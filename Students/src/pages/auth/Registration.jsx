import "./auth.css";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
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

function Registration() {
  return (
    <div className="registration">
      <h1>registration</h1>
    </div>
  );
}

export default Registration;
