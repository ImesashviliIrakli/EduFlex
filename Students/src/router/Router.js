import "bootstrap/dist/css/bootstrap.min.css";
import "normalize.css";
import {
  Route,
  createBrowserRouter,
  createRoutesFromElements,
  Navigate,
} from "react-router-dom";
// Layouts
import AuthLayout from "../layout/auth-layout/AuthLayout";
// Pages
import NotFound from "../pages/not-found/NotFound";
import Login from "../pages/auth/Login";
import Registration from "../pages/auth/Registration";

export const router = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route path="/" element={<Navigate to="/auth" />} />
      <Route path="/auth" element={<AuthLayout />}>
        <Route index element={<Login />} />
        <Route path="/auth/registration" element={<Registration />} />
      </Route>
      <Route path="*" element={<NotFound />} />
    </>
  )
);
