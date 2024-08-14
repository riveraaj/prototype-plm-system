import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { NextUIProvider } from "@nextui-org/react";
import "./assets/styles/index.css";
import App from "./pages/App";

createRoot(document.getElementById("root")).render(
	<StrictMode>
		<NextUIProvider>
			<App />
		</NextUIProvider>
	</StrictMode>
);
