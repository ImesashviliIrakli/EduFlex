import "./edit-course.css";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useMutation } from "@tanstack/react-query";
import { useRootContext } from "../../hooks/useRootContext";
import { useAlertBarContext } from "../../hooks/useAlertBarContext";
import { useGet } from "../../hooks/useGet";
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

function EditCourse() {
    const courseId = window.location.pathname.split("/")[2];

    const [id, setId] = useState("");
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

    const { data: courseById } = useGet(["course", courseId], `/api/course/${courseId}`);
    const { data: faculties } = useGet(["Faculties"], "/api/faculty/get");

    useEffect(() => {
        if (courseById?.result !== null) {
            setTitle(courseById?.result.title);
            setDescription(courseById?.result.description);
            setPrice(courseById?.result.price);
            setImageUrl(courseById?.result.imageUrl);
            setFacultyId(courseById?.result.faculty.id);
            setId(courseById?.result.id);
        }
    }, [courseById?.result]);

    const mutation = useMutation({
        mutationKey: ["Edit Course", courseId],
        mutationFn: (editCourse) =>
            fetchData({
                url: baseUrl + `/api/course`,
                method: "put",
                data: editCourse,
                params: {
                    id: parseInt(courseId),
                },
            }),
        onSuccess: () => {
            handleSnackbarOpen();
            setSnackbarStat(snackbarStatus.success);
            setSnackbarMessage("Course Updated Successfully!");
            setTimeout(() => {
                navigate("/courses");
            }, 1000);
        },
        onError: () => {
            handleSnackbarOpen();
            setSnackbarStat(snackbarStatus.error);
            setSnackbarMessage("Course Update Failed!");
        },
    });

    const handleSubmit = (e) => {
        e.preventDefault();
        mutation.mutate({ id, title, description, price, imageUrl, facultyId });
    };

    return (
        <div className="edit-course">
            <h1>Edit Course</h1>
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
                        Update
                    </Button>
                    <Button variant="outlined" onClick={() => navigate("/courses")}>
                        Go Back
                    </Button>
                </Box>
            </Box>
        </div>
    );
}

export default EditCourse;
