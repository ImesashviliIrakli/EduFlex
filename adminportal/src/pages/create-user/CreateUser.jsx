import "./create-user.css"
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useMutation } from "@tanstack/react-query";
import { useRootContext } from "../../hooks/useRootContext";
import { useAlertBarContext } from "../../hooks/useAlertBarContext";
import { fetchData } from "../../functions/fetchData";
import {
    Box,
    TextField,
    Button,
    Select,
    MenuItem,
    FormControl,
    InputLabel,
    Typography,
} from "@mui/material";
import AddIcon from '@mui/icons-material/Add';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';

function CreateUser() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [username, setUsername] = useState("");
    const [roleName, setRoleName] = useState("Teacher");

    const navigate = useNavigate();

    const { baseUrl } = useRootContext();
    const {
        snackbarStatus,
        setSnackbarStat,
        setSnackbarMessage,
        handleSnackbarOpen,
    } = useAlertBarContext();

    const mutation = useMutation({
        mutationKey: ["Create User"],
        mutationFn: (newUser) =>
            fetchData({
                url: baseUrl + "/api/auth/addUserByAdmin",
                method: "post",
                data: newUser,
            }),
        onSuccess: () => {
            handleSnackbarOpen();
            setSnackbarStat(snackbarStatus.success);
            setSnackbarMessage("User Created Successfully!");
            setTimeout(() => {
                navigate("/users");
            }, 1000);
        },
        onError: () => {
            handleSnackbarOpen();
            setSnackbarStat(snackbarStatus.error);
            setSnackbarMessage("User Creation Failed!");
        },
    });

    const handleSubmit = (e) => {
        e.preventDefault();
        mutation.mutate({ email, password, username, roleName });
    };

    return (
         <div className="create-user">
            <Typography variant="h4" component="h1" gutterBottom>
                Create Course
            </Typography>
            <Box component="form" className="p-3" onSubmit={handleSubmit} sx={{ backgroundColor: "#fff", padding: "20px", borderRadius: "8px", boxShadow: "0 2px 4px rgba(0, 0, 0, 0.1)" }}>
                <TextField
                    required
                    label="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    fullWidth
                    margin="normal"
                />
                <TextField
                    required
                    label="Password"
                    type="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    fullWidth
                    margin="normal"
                />
                <TextField
                    required
                    label="Username"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                    fullWidth
                    margin="normal"
                />
                <FormControl fullWidth margin="normal">
                    <InputLabel>Role Name</InputLabel>
                    <Select
                        value={roleName}
                        onChange={(e) => setRoleName(e.target.value)}
                        label="Role Name"
                    >
                        <MenuItem value="Admin">Admin</MenuItem>
                        <MenuItem value="Teacher">Teacher</MenuItem>
                    </Select>
                </FormControl>
                <Box mt={2} display="flex" justifyContent="space-between">
                    <Button variant="contained" color="success" startIcon={<AddIcon />} type="submit">
                        Create
                    </Button>
                    <Button variant="outlined" startIcon={<ArrowBackIcon />} onClick={() => navigate("/users")}>
                        Go Back
                    </Button>
                </Box>
            </Box>
        </div>
    );
}

export default CreateUser;
