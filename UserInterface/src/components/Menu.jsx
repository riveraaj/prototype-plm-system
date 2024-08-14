import {
	Button,
	Dropdown,
	DropdownItem,
	DropdownMenu,
	DropdownTrigger,
	Image,
	Navbar,
	NavbarBrand,
	NavbarContent,
	NavbarItem,
} from "@nextui-org/react";
import { NavLink } from "react-router-dom";
import { ChevronDown } from "./ChevronDown";

export default function Menu() {
	return (
		<Navbar>
			<NavbarBrand>
				<NavbarItem>
					<NavLink to="/" className="flex justify-center items-center">
						<Image src="/logo.png" width={40} />
						<p className="font-bold text-inherit ml-3">PLM</p>
					</NavLink>
				</NavbarItem>
			</NavbarBrand>
			<NavbarContent className="hidden sm:flex gap-4" justify="center">
				<NavbarItem>
					<NavLink
						className={({ isActive }) =>
							isActive ? "text-purple-600 font-semibold" : "font-semibold"
						}
						to="/empleados"
					>
						Empleados
					</NavLink>
				</NavbarItem>
				<NavbarItem isActive>
					<NavLink
						className={({ isActive }) =>
							isActive ? "text-purple-600  font-semibold" : "font-semibold"
						}
						to="/ideas"
					>
						Ideas
					</NavLink>
				</NavbarItem>
				<NavbarItem>
					<NavLink
						className={({ isActive }) =>
							isActive ? "text-purple-600  font-semibold" : "font-semibold"
						}
						to="/propuestas"
					>
						Propuestas
					</NavLink>
				</NavbarItem>
				<NavbarItem>
					<NavLink
						className={({ isActive }) =>
							isActive ? "text-purple-600  font-semibold" : "font-semibold"
						}
						to="/disenos"
					>
						Diseños
					</NavLink>
				</NavbarItem>
				<Dropdown>
					<NavbarItem>
						<DropdownTrigger>
							<Button
								disableRipple
								className="p-0 bg-transparent data-[hover=true]:bg-transparent font-semibold text-base"
								endContent={<ChevronDown fill="currentColor" size={16} />}
							>
								Reportes
							</Button>
						</DropdownTrigger>
					</NavbarItem>
					<DropdownMenu
						aria-label="PLM reports"
						className="w-[340px]"
						itemClasses={{
							base: "gap-4",
						}}
					>
						<DropdownItem key="proposals">
							<NavLink
								className={({ isActive }) =>
									isActive ? "text-purple-600  font-semibold" : "font-semibold"
								}
								to="/reporte/propuestas"
							>
								Reporte de Propuestas
							</NavLink>
						</DropdownItem>

						<DropdownItem key="designs">
							<NavLink
								className={({ isActive }) =>
									isActive ? "text-purple-600  font-semibold" : "font-semibold"
								}
								to="/reporte/disenos"
							>
								Reporte de Diseños
							</NavLink>
						</DropdownItem>
					</DropdownMenu>
				</Dropdown>
			</NavbarContent>
			<NavbarContent justify="end"></NavbarContent>
		</Navbar>
	);
}
