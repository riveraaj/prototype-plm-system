import { BrowserRouter, Route, Routes } from "react-router-dom";
import Menu from "../components/Menu";
import Home from "../pages/Home";
import Employee from "../pages/Employee";
import Idea from "../pages/Idea";
import Proposal from "../pages/Proposal";
import Design from "../pages/Design";
import ReportProposal from "../pages/ReportProposal";
import ReportDesign from "../pages/ReportDesign";

export default function Router() {
	return (
		<BrowserRouter>
			<Menu />
			<Routes>
				<Route path="/" element={<Home />} />
				<Route path="/empleados" element={<Employee />} />
				<Route path="/ideas" element={<Idea />} />
				<Route path="/propuestas" element={<Proposal />} />
				<Route path="/disenos" element={<Design />} />
				<Route path="/reporte/propuestas" element={<ReportProposal />} />
				<Route path="/reporte/disenos" element={<ReportDesign />} />
			</Routes>
		</BrowserRouter>
	);
}
