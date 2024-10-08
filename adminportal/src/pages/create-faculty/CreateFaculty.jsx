import "./create-faculty.css"
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useMutation, useQueryClient } from "@tanstack/react-query";
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
function CreateFaculty() {
    const [name, setName] = useState("");

    const navigate = useNavigate();

    const { baseUrl } = useRootContext();
    const {
        snackbarStatus,
        setSnackbarStat,
        setSnackbarMessage,
        handleSnackbarOpen,
    } = useAlertBarContext();

    const mutation = useMutation({
        mutationKey: ["Create Faculty"],
        mutationFn: (newFaculty) =>
            fetchData({
                url: baseUrl + "/api/faculty",
                method: "post",
                data: newFaculty,
            }),
        onSuccess: () => {
            handleSnackbarOpen();
            setSnackbarStat(snackbarStatus.success);
            setSnackbarMessage("Faculty Created Successfully!");
            setTimeout(() => {
                navigate("/faculties");
            },1000)
        },
        onError: () => {
            handleSnackbarOpen();
            setSnackbarStat(snackbarStatus.error);
            setSnackbarMessage("Faculty Creation Failed!");
        },
    });

    const handleSubmit = (e) => {
        e.preventDefault();
        mutation.mutate({ name });
    };

    return (
        <div className="create-faculty">
            <Typography variant="h4" component="h1" gutterBottom>
                Create Faculty
            </Typography>
            <Box component="form" className="p-3" onSubmit={handleSubmit} sx={{ backgroundColor: "#fff", padding: "20px", borderRadius: "8px", boxShadow: "0 2px 4px rgba(0, 0, 0, 0.1)" }}>
                

                <FormControl fullWidth margin="normal" variant="outlined">
                    <TextField
                        required
                        label="Name"
                        value={name}
                        onChange={(e) => {
                            setName(e.target.value);
                        }}
                    />
                </FormControl>

                <Box mt={2} display="flex" justifyContent="space-between">
                    <Button
                        variant="contained"
                        startIcon={<AddIcon />}
                        type="submit"
                        color="success"
                    >
                        Create
                    </Button>
                    <Button
                        variant="outlined"
                        startIcon={<ArrowBackIcon />}
                        onClick={() => navigate("/faculties")}
                    >
                        Go Back
                    </Button>
                </Box>
            </Box>
        </div>
    );
}

export default CreateFaculty
