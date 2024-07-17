import {
  Route,
  createBrowserRouter,
  createRoutesFromElements,
  Navigate,
} from "react-router-dom";
// Layouts
import RootLayout from "../layout/root-layout/RootLayout";
// Pages
import NotFound from "../pages/not-found/NotFound";
import Login from "../pages/auth/Login";

const PrivateRoute = ({ element: Element, ...rest }) => {
  return sessionStorage.getItem("login") === "true" ? (
    <Element {...rest} />
  ) : (
    <Navigate to="/auth" />
  );
};

export const router = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route path="/" element={<Navigate to="/auth" />} />
      <Route path="/" element={<RootLayout />}>
        <Route path="/auth" element={<Login />} />
        <Route path="*" element={<NotFound />} />
      </Route>
    </>
  )
);
