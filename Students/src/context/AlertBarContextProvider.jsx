import { createContext, useState } from "react";

export const AlertBarContext = createContext();

function AlertBarContextProvider({ children }) {
  const [snackbar, setSnackbar] = useState(false);
  const [snackbarStat, setSnackbarStat] = useState("");
  const [snackbarMessage, setSnackbarMessage] = useState("");

  const snackbarStatus = {
    error: "error",
    info: "info",
    warning: "warning",
    success: "success",
  };

  const handleSnackbarOpen = () => {
    setSnackbar(true);
  };

  const handleSnackbarClose = (event, reason) => {
    if (reason === "clickaway") {
      return;
    }
    setSnackbar(false);
  };

  return (
    <AlertBarContext.Provider
      value={{
        snackbarStatus,
        snackbar,
        snackbarStat,
        setSnackbarStat,
        snackbarMessage,
        setSnackbarMessage,
        handleSnackbarOpen,
        handleSnackbarClose,
      }}
    >
      {children}
    </AlertBarContext.Provider>
  );
}

export default AlertBarContextProvider;
