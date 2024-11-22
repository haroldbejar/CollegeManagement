import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App";
import { UserProvider } from "./context/user.context.jsx";
import { FetchProvider } from "./context/fecth.context.jsx";
import "./index.css";

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <FetchProvider>
      <UserProvider>
        <App />
      </UserProvider>
    </FetchProvider>
  </StrictMode>
);
