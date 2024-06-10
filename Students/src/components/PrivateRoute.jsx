// src/components/PrivateRoute.jsx

import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import useAuth from '../hooks/useAuth';

const PrivateRoute = ({ element: Component, ...rest }) => {
    const { auth } = useAuth();

    return auth ? <Outlet /> : <Navigate to="/login" />;
};

export default PrivateRoute;
