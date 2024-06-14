import "./auth.css";
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useRootContext } from "../../hooks/useRootContext";
import { useAlertBarContext } from "../../hooks/useAlertBarContext";
import { fetchData } from "../../functions/fetchData";
import { useMutation } from "@tanstack/react-query";
import {
  Box,
  IconButton,
  OutlinedInput,
  InputLabel,
  InputAdornment,
  FormControl,
  TextField,
  Button,
} from "@mui/material";
import { VisibilityOff, Visibility } from "@mui/icons-material";
import LoginIcon from "@mui/icons-material/Login";

function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);

  const navigate = useNavigate();

  const { baseUrl } = useRootContext();
  const {
    snackbarStatus,
    setSnackbarStat,
    setSnackbarMessage,
    handleSnackbarOpen,
  } = useAlertBarContext();

  const mutation = useMutation({
    mutationKey: "login",
    mutationFn: (loginData) =>
      fetchData({
        url: baseUrl + "/api/auth/login",
        method: "post",
        data: JSON.stringify(loginData),
      }),
    onSuccess: (data) => {
      sessionStorage.setItem("userData", JSON.stringify(data.result));
      sessionStorage.setItem("token", JSON.stringify(data.result.token));
      handleSnackbarOpen();
      setSnackbarStat(snackbarStatus.success);
      setSnackbarMessage("Successfully logged in");
      setTimeout(() => navigate("/home"), 2500);
    },
    onError: (error) => {
      sessionStorage.setItem("userData", undefined);
      sessionStorage.setItem("token", undefined);
      handleSnackbarOpen();
      setSnackbarStat(snackbarStatus.error);
      setSnackbarMessage(error.data?.Result?.Message);
    },
  });

  const handleShowPassword = () => setShowPassword((show) => !show);

  const handleMouseDownPassword = (event) => {
    event.preventDefault();
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    mutation.mutate({ email: email, password: password });
  };

  return (
    <>
      <div className="login">
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
            <span>or</span>
            <Link variant="outlined" to="/auth/registration">
              <Button variant="outlined">Register</Button>
            </Link>
          </Box>
        </Box>
      </div>
    </>
  );
}

export default Login;
