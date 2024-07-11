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
import Users from "../pages/users/Users";
import Courses from "../pages/courses/Courses";
import CreateCourse from "../pages/create-course/CreateCourse";
import Faculties from "../pages/faculties/Faculties";
import CreateFaculty from "../pages/create-faculty/CreateFaculty";
import EditFaculty from "../pages/edit-faculty/EditFaculty";

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

        <Route path="/users" element={<PrivateRoute element={Users} />} />

        <Route path="/courses" element={<PrivateRoute element={Courses} />} />
        <Route
          path="/create-course"
          element={<PrivateRoute element={CreateCourse} />}
        />
        <Route
          path="/edit-course/:id"
          element={<PrivateRoute element={EditFaculty} />}
        />

        <Route
          path="/faculties"
          element={<PrivateRoute element={Faculties} />}
        />
        <Route
          path="/create-faculty"
          element={<PrivateRoute element={CreateFaculty} />}
        />
        <Route
          path="/edit-faculty/:id"
          element={<PrivateRoute element={EditFaculty} />}
        />

        <Route path="*" element={<NotFound />} />
      </Route>
    </>
  )
);
