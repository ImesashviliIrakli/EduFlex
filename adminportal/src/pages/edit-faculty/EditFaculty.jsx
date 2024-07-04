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
} from "@mui/material";
import AddIcon from '@mui/icons-material/Add';

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

  const {data: facultyById} = useGet(["faculty", facultyId], `/api/faculty/${facultyId}`);

  useEffect(() => {
    if(facultyById?.result !== null) {
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
    <div className="edit-faculty">
      <h1>Edit Faculty</h1>
      <Box component="form" className="p-3" onSubmit={handleSubmit}>
        {/* <TextField
          required
          label="Id"
          value={id}
          onChange={(e) => {
            setId(e.target.value);
          }}
        /> */}
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
            Update
          </Button>
          <Button variant="outlined" onClick={() => navigate("/faculties")}>
            Go Back
          </Button>
        </Box>
      </Box>
    </div>
  );
}

export default EditFaculty
