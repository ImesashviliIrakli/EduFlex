import "./create-course.css";
import { useState } from "react";
import { useGet } from "../../hooks/useGet";
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

function CreateCourse() {
    const { data: faculties, isLoading } = useGet(["Faculties"], "/api/faculty/get");

    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [price, setPrice] = useState(0);
    const [imageUrl, setImageUrl] = useState("");
    const [facultyId, setFacultyId] = useState("");

    const navigate = useNavigate();

    const { baseUrl } = useRootContext();

    const {
        snackbarStatus,
        setSnackbarStat,
        setSnackbarMessage,
        handleSnackbarOpen,
    } = useAlertBarContext();

    const mutation = useMutation({
        mutationKey: ["Create Course"],
        mutationFn: (newCourse) =>
            fetchData({
                url: baseUrl + "/api/Course",
                method: "post",
                data: newCourse,
            }),
        onSuccess: () => {
            handleSnackbarOpen();
            setSnackbarStat(snackbarStatus.success);
            setSnackbarMessage("Course Created Successfully!");
            setTimeout(() => {
                navigate("/courses");
            }, 1000);
        },
        onError: () => {
            handleSnackbarOpen();
            console.log(snackbarStatus);
            setSnackbarStat(snackbarStatus.error);
            setSnackbarMessage("Course Creation Failed!");
        },
    });

    const handleSubmit = (e) => {
        e.preventDefault();
        mutation.mutate({ title, description, price, imageUrl, facultyId });
    };

    return (
        <div className="create-course">
            <Typography variant="h4" component="h1" gutterBottom>
                Create Course
            </Typography>
            <Box component="form" className="p-3" onSubmit={handleSubmit} sx={{ backgroundColor: "#fff", padding: "20px", borderRadius: "8px", boxShadow: "0 2px 4px rgba(0, 0, 0, 0.1)" }}>
                <TextField
                    required
                    label="Title"
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                    fullWidth
                    margin="normal"
                    variant="outlined"
                />
                <TextField
                    required
                    label="Description"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    fullWidth
                    margin="normal"
                    variant="outlined"
                />
                <TextField
                    required
                    label="Price"
                    type="number"
                    value={price}
                    onChange={(e) => setPrice(Number(e.target.value))}
                    fullWidth
                    margin="normal"
                    variant="outlined"
                />
                <TextField
                    required
                    label="Image URL"
                    value={imageUrl}
                    onChange={(e) => setImageUrl(e.target.value)}
                    fullWidth
                    margin="normal"
                    variant="outlined"
                />
                <FormControl fullWidth margin="normal" variant="outlined">
                    <InputLabel>Faculty</InputLabel>
                    <Select
                        value={facultyId}
                        onChange={(e) => setFacultyId(e.target.value)}
                        label="Faculty"
                        required
                    >
                        {faculties?.result?.map((faculty) => (
                            <MenuItem key={faculty.id} value={faculty.id}>
                                {faculty.name}
                            </MenuItem>
                        ))}
                    </Select>
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
                        onClick={() => navigate("/courses")}
                        sx={{ borderColor: '#007bff', color: '#007bff', '&:hover': { borderColor: '#0056b3', color: '#0056b3' } }}
                    >
                        Go Back
                    </Button>
                </Box>
            </Box>
        </div>
    );
}

export default CreateCourse;
