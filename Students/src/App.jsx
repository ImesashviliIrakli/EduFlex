import "bootstrap/dist/css/bootstrap.min.css";
import "normalize.css";
import "./index.css";
import { RouterProvider } from "react-router-dom";
import { router } from "./router/Router";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import RootContextProvider from "./context/RootContextProvider";

const queryClient = new QueryClient();

function App() {
  return (
    <>
      <RootContextProvider>
        <QueryClientProvider client={queryClient}>
          <RouterProvider router={router} />
          <ReactQueryDevtools
            initialIsOpen={false}
            buttonPosition="top-right"
          />
        </QueryClientProvider>
      </RootContextProvider>
    </>
  );
}

export default App;
