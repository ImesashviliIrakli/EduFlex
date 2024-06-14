import { useContext } from "react";
import { AlertBarContext } from "../context/AlertBarContextProvider";

export const useAlertBarContext = () => {
  return useContext(AlertBarContext);
};
