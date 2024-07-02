import "./auth.css";
import { useState } from "react";
import { Link } from "react-router-dom";
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
import { useAuth } from "../../hooks/useAuth";

function Registration() {
  const [email, setEmail] = useState("");
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);

  const { mutate } = useAuth(
    ["registration"],
    "/api/auth/register",
    "register"
  );

  const handleShowPassword = () => setShowPassword((show) => !show);

  const handleMouseDownPassword = (event) => {
    event.preventDefault();
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    mutate({ email, userName, password, roleName: "Admin" });
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
