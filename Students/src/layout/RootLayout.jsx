import { Outlet } from "react-router-dom";

function RootLayout() {
  return (
    <div className="root-layout">
      <h1>hello world</h1>
      <Outlet />
    </div>
  );
}

export default RootLayout;
