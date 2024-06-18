import { createContext, useState } from "react";

export const RootContext = createContext();

function RootContextProvider({ children }) {
  const baseUrl = "https://localhost:7000";

  const [loggedIn, setLoggedIn] = useState(false);

  return (
    <RootContext.Provider value={{ baseUrl, loggedIn, setLoggedIn }}>
      {children}
    </RootContext.Provider>
  );
}

export default RootContextProvider;
