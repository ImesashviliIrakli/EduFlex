import "bootstrap/dist/css/bootstrap.min.css";
import "normalize.css";
import "./index.css";
import { RouterProvider } from "react-router-dom";
import { router } from "./router/Router";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import RootContextProvider from "./context/RootContextProvider";
import AlertBarContextProvider from "./context/AlertBarContextProvider";
import AlertBar from "./components/alertbar/AlertBar";

const queryClient = new QueryClient();

function App() {
  return (
    <>
      <QueryClientProvider client={queryClient}>
        <RootContextProvider>
          <AlertBarContextProvider>
            <RouterProvider router={router} />
            <AlertBar />
          </AlertBarContextProvider>
        </RootContextProvider>
        <ReactQueryDevtools
          initialIsOpen={false}
          buttonPosition="bottom-right"
        />
      </QueryClientProvider>
    </>
  );
}

export default App;
