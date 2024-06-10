import React, { useState } from 'react';
import useAuth from '../hooks/useAuth';

const Register = () => {
    const [email, setEmail] = useState('');
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const [roleName, setRoleName] = useState('');
    const { register } = useAuth();

    const handleSubmit = async (e) => {
        e.preventDefault();
        await register(email, userName, password, roleName);
    };

    return (
        <form onSubmit={handleSubmit}>
            <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
            <input type="text" value={userName} onChange={(e) => setUserName(e.target.value)} />
            <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
            <input type="text" value={roleName} onChange={(e) => setRoleName(e.target.value)} />
            <button type="submit">Register</button>
        </form>
    );
};

export default Register;
