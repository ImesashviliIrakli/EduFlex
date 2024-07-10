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
} from "@mui/material";
import AddIcon from '@mui/icons-material/Add';

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
            <h1>Create Course</h1>
            <Box component="form" className="p-3" onSubmit={handleSubmit}>
                <TextField
                    required
                    label="Title"
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                    fullWidth
                    margin="normal"
                />
                <TextField
                    required
                    label="Description"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    fullWidth
                    margin="normal"
                />
                <TextField
                    required
                    label="Price"
                    type="number"
                    value={price}
                    onChange={(e) => setPrice(Number(e.target.value))}
                    fullWidth
                    margin="normal"
                />
                <TextField
                    required
                    label="Image URL"
                    value={imageUrl}
                    onChange={(e) => setImageUrl(e.target.value)}
                    fullWidth
                    margin="normal"
                />
                <FormControl fullWidth margin="normal">
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
                <Box mt={2}>
                    <Button variant="contained" startIcon={<AddIcon />} type="submit">
                        Create
                    </Button>
                    <Button variant="outlined" onClick={() => navigate("/courses")}>
                        Go Back
                    </Button>
                </Box>
            </Box>
        </div>
    );
}

export default CreateCourse;
