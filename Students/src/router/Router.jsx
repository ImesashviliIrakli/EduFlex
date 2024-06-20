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
import Registration from "../pages/auth/Registration";
import Courses from "../pages/courses/Courses";
import CourseDetails from "../pages/course-details/CourseDetails";

export const router = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route path="/" element={<Navigate to="/auth" />} />
      <Route path="/" element={<RootLayout />}>
        <Route path="/auth" element={<Login />} />
        <Route path="/auth/registration" element={<Registration />} />
        <Route path="/courses" element={<Courses />} />
        <Route path="/course/:id" element={<CourseDetails />} />
        <Route path="*" element={<NotFound />} />
      </Route>
    </>
  )
);
