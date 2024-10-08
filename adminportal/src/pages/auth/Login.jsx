import React, { useState } from "react";
import "./auth.css";
import { useAuth } from "../../hooks/useAuth";
import {
    Box,
    IconButton,
    OutlinedInput,
    InputLabel,
    InputAdornment,
    FormControl,
    TextField,
    Button,
    Typography,
} from "@mui/material";
import { VisibilityOff, Visibility } from "@mui/icons-material";
import LoginIcon from "@mui/icons-material/Login";

function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [showPassword, setShowPassword] = useState(false);

    const { mutate } = useAuth(["login"], "/api/auth/login", "login");

    const handleShowPassword = () => setShowPassword((show) => !show);

    const handleMouseDownPassword = (event) => {
        event.preventDefault();
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        mutate({ email, password });
    };

    return (
        <div className="login-container">
            <div className="login">
                <Typography variant="h4" component="h2">Admin Login</Typography>
                <Box component="form" className="p-3" onSubmit={handleSubmit}>
                    <TextField
                        required
                        label="Email"
                        value={email}
                        onChange={(e) => {
                            setEmail(e.target.value);
                        }}
                    />
                    <FormControl
                        required
                        variant="outlined"
                        value={password}
                        onChange={(e) => {
                            setPassword(e.target.value);
                        }}
                    >
                        <InputLabel htmlFor="password">Password</InputLabel>
                        <OutlinedInput
                            name="password"
                            type={showPassword ? "text" : "password"}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton
                                        aria-label="toggle password visibility"
                                        onClick={handleShowPassword}
                                        onMouseDown={handleMouseDownPassword}
                                        edge="end"
                                    >
                                        {showPassword ? <VisibilityOff /> : <Visibility />}
                                    </IconButton>
                                </InputAdornment>
                            }
                            label="Password"
                        />
                    </FormControl>
                    <Box mt={2}>
                        <Button variant="contained" endIcon={<LoginIcon />} type="submit">
                            Login
                        </Button>
                    </Box>
                </Box>
            </div>
        </div>

    );
}

export default Login;
