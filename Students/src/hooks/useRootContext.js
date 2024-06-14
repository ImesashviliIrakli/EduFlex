import { useContext } from "react";
import { RootContext } from "../context/RootContextProvider";

export const useRootContext = () => {
  return useContext(RootContext);
};
