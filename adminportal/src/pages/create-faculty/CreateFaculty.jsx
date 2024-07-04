import "./create-faculty.css"
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
} from "@mui/material";
import AddIcon from '@mui/icons-material/Add';

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
        }, 1000);
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
        <h1>Create Faculty</h1>
        <Box component="form" className="p-3" onSubmit={handleSubmit}>
          <TextField
            required
            label="Name"
            value={name}
            onChange={(e) => {
              setName(e.target.value);
            }}
          />

          <Box mt={2}>
            <Button variant="contained" startIcon={<AddIcon />} type="submit">
              Create
            </Button>
            <Button variant="outlined" onClick={() => navigate("/faculties")}>
              Go Back
            </Button>
          </Box>
        </Box>
      </div>
    );
}

export default CreateFaculty
