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
// User Management Pages
import Users from "../pages/users/Users";
import CreateUser from "../pages/create-user/CreateUser";
// Course Management Pages
import Courses from "../pages/courses/Courses";
import CreateCourse from "../pages/create-course/CreateCourse";
import EditCourse from "../pages/edit-course/EditCourse";
// Faculty Management Pages
import Faculties from "../pages/faculties/Faculties";
import CreateFaculty from "../pages/create-faculty/CreateFaculty";
import EditFaculty from "../pages/edit-faculty/EditFaculty";
// TeacherCourse Page
import TeacherCourses from "../pages/teacher-Courses/TeacherCourses";
// Enrollments Page 
import Enrollments from "../pages/enrollments/Enrollments";

const PrivateRoute = ({ element: Element, ...rest }) => {
    return sessionStorage.getItem("login") === "true" ? <Element {...rest} /> : <Navigate to="/auth" />;
};

export const router = createBrowserRouter(
    createRoutesFromElements(
        <>
            <Route path="/" element={<Navigate to="/auth" />} />
            <Route path="/" element={<RootLayout />}>
                <Route path="/auth" element={<Login />} />
                {/*User Management*/}
                <Route path="/users" element={<PrivateRoute element={Users} />} />
                <Route path="/create-user" element={<PrivateRoute element={CreateUser} />} />
                {/*Course Management*/}
                <Route path="/courses" element={<PrivateRoute element={Courses} />} />
                <Route path="/create-course" element={<PrivateRoute element={CreateCourse} />} />
                <Route path="/edit-course/:id" element={<PrivateRoute element={EditCourse} />} />
                {/*Faculty Management*/}
                <Route path="/faculties" element={<PrivateRoute element={Faculties} />} />
                <Route path="/create-faculty" element={<PrivateRoute element={CreateFaculty} />} />
                <Route path="/edit-faculty/:id" element={<PrivateRoute element={EditFaculty} />} />
                {/*Teacher Course Management*/}
                <Route path="/teacher-courses" element={<PrivateRoute element={TeacherCourses} />} />
                {/*Enrollments Management*/}
                <Route path="/enrollments" element={<PrivateRoute element={Enrollments} />} />

                <Route path="*" element={<NotFound />} />
            </Route>
        </>
    )
);
