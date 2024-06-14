import { createContext } from "react";

export const RootContext = createContext();

function RootContextProvider({ children }) {
  const baseUrl = "https://localhost:7000";

  return (
    <RootContext.Provider value={{ baseUrl }}>{children}</RootContext.Provider>
  );
}

export default RootContextProvider;
