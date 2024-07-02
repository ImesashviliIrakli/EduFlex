import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [react()],
    server: {
        port: 3001,
    },
    // esbuild: {
    //   loader: { ".js": "jsx" },
    //   include: ["src/**/*.js", "src/**/*.jsx"],
    //   exclude: [],
    // },
    // resolve: {
    //   extensions: [".js", ".jsx"],
    // },
});
