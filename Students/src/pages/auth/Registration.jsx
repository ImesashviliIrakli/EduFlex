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
import AccountCircleIcon from "@mui/icons-material/AccountCircle";

function Registration() {
  const [email, setEmail] = useState("");
  const [userName, setUserName] = useState("");
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
    mutationKey: "register",
    mutationFn: (registerData) =>
      fetchData({
        url: baseUrl + "/api/auth/register",
        method: "post",
        data: JSON.stringify(registerData),
      }),
    onSuccess: () => {
      handleSnackbarOpen();
      setSnackbarStat(snackbarStatus.success);
      setSnackbarMessage("Registered Successfully!");
      setTimeout(() => navigate("/"), 2500);
    },
    onError: (error) => {
      handleSnackbarOpen();
      setSnackbarStat(snackbarStatus.error);
      setSnackbarMessage(error.data?.Result?.Message || "An Error Occured");
    },
  });

  const handleShowPassword = () => setShowPassword((show) => !show);

  const handleMouseDownPassword = (event) => {
    event.preventDefault();
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    mutation.mutate({ email, userName, password, roleName: "Student" });
  };

  return (
    <>
      <div className="registration">
        <Box component="form" className="p-3" onSubmit={handleSubmit}>
          <TextField
            required
            label="Email"
            value={email}
            onChange={(e) => {
              setEmail(e.target.value);
            }}
          />

          <TextField
            required
            label="User Name"
            value={userName}
            onChange={(e) => {
              setUserName(e.target.value);
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
            <Button
              variant="contained"
              endIcon={<AccountCircleIcon />}
              type="submit"
            >
              Register
            </Button>
            <span>or</span>
            <Link to="/auth">
              <Button variant="outlined" endIcon={<LoginIcon />}>
                Login
              </Button>
            </Link>
          </Box>
        </Box>
      </div>
    </>
  );
}

export default Registration;
