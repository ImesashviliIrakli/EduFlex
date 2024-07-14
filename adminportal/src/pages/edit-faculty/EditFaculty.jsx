import "./edit-faculty.css"
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
    Typography,
} from "@mui/material";
import EditIcon from '@mui/icons-material/Edit';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';

function EditFaculty() {
    const facultyId = window.location.pathname.split("/")[2];

    const [id, setId] = useState("");
    const [name, setName] = useState("");

    const navigate = useNavigate();

    const { baseUrl } = useRootContext();
    const {
        snackbarStatus,
        setSnackbarStat,
        setSnackbarMessage,
        handleSnackbarOpen,
    } = useAlertBarContext();

    const { data: facultyById } = useGet(["faculty", facultyId], `/api/faculty/${facultyId}`);

    useEffect(() => {
        if (facultyById?.result !== null) {
            setName(facultyById?.result.name);
            setId(facultyById?.result.id);
        }
    }, [facultyById?.result])

    const mutation = useMutation({
        mutationKey: ["Edit Faculty", facultyId],
        mutationFn: (editFaculty) =>
            fetchData({
                url: baseUrl + `/api/faculty`,
                method: "put",
                data: editFaculty,
                params: {
                    id: parseInt(facultyId),
                },
            }),
        onSuccess: () => {
            handleSnackbarOpen();
            setSnackbarStat(snackbarStatus.success);
            setSnackbarMessage("Faculty Updated Successfully!");
            setTimeout(() => {
                navigate("/faculties");
            }, 1000);
        },
        onError: () => {
            handleSnackbarOpen();
            setSnackbarStat(snackbarStatus.error);
            setSnackbarMessage("Faculty Update Failed!");
        },
    });

    const handleSubmit = (e) => {
        e.preventDefault();
        mutation.mutate({ name });
    };

    return (
        <div className="create-faculty">
            <Typography variant="h4" component="h1" gutterBottom>
                Edit Faculty
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
                        startIcon={<EditIcon />}
                        type="submit"
                    >
                        Edit
                    </Button>
                    <Button
                        variant="outlined"
                        startIcon={<ArrowBackIcon />}
                        onClick={() => navigate("/faculties")}
                        sx={{ borderColor: '#007bff', color: '#007bff', '&:hover': { borderColor: '#0056b3', color: '#0056b3' } }}
                    >
                        Go Back
                    </Button>
                </Box>
            </Box>
        </div>
    );
}

export default EditFaculty
