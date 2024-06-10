// src/components/Home.jsx

import React from 'react';
import useAuth from '../hooks/useAuth';

const Home = () => {
    const {logout } = useAuth();
    const auth = localStorage.getItem('user');
    console.log(JSON.parse(auth));
    return (
        <div>
            <h1>Welcome, {auth}</h1>
            <button onClick={logout}>Logout</button>
        </div>
    );
};

export default Home;
