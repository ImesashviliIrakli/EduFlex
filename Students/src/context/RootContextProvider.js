/* eslint-disable react/prop-types */
import { createContext, useState } from "react";

export const RootContext = createContext();

function RootContextProvider({ children }) {
  const baseUrl = "https://localhost:7000";

  const [token, setToken] = useState(null);

  return (
    <RootContext.Provider value={{ baseUrl, token, setToken }}>
      {children}
    </RootContext.Provider>
  );
}

export default RootContextProvider;
