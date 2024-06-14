import "./alertbar.css";
import { useAlertBarContext } from "../../hooks/useAlertBarContext";
import { Snackbar, Alert } from "@mui/material";

function AlertBar() {
  const { snackbar, snackbarStat, snackbarMessage, handleSnackbarClose } =
    useAlertBarContext();

  return (
    <Snackbar
      open={snackbar}
      autoHideDuration={2000}
      onClose={handleSnackbarClose}
    >
      <Alert
        onClose={handleSnackbarClose}
        severity={snackbarStat}
        variant="filled"
        sx={{ width: "100%" }}
      >
        {snackbarMessage}
      </Alert>
    </Snackbar>
  );
}

export default AlertBar;
