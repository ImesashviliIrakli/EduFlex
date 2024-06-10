import React, { createContext, useState } from 'react';
import jwt_decode from 'jwt-decode';
import axios from 'axios';

const AuthContext = createContext();
const baseUrl = 'https://localhost:7000';
export const AuthProvider = ({ children }) => {
    const [auth, setAuth] = useState(null);

    const login = async (email, password) => {
        const response = await axios.post(baseUrl+'/api/auth/login', { email, password });
        const token = response.data.token;
        localStorage.setItem('token', token);
        setAuth({ token, user: jwt_decode(token) });
    };

    const logout = () => {
        localStorage.removeItem('token');
        setAuth(null);
    };

    const register = async (email, userName, password, roleName) => {
        const response = await axios.post(baseUrl + '/api/auth/register', { email, userName, password, roleName });
        return response.data;
    };

    return (
        <AuthContext.Provider value={{ auth, login, logout, register }}>
            {children}
        </AuthContext.Provider>
    );
};

export default AuthContext;
