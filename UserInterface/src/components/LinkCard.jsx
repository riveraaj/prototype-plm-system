import { Card, CardFooter, Image } from "@nextui-org/react";
import { NavLink } from "react-router-dom";

export default function LinkCard({ title, src, path, width }) {
	return (
		<NavLink to={path} className={width || "auto"}>
			<Card
				isFooterBlurred
				radius="lg"
				className="border-none bg-transparent cursor-pointer"
			>
				<Image
					alt={`Navigate to ${title}`}
					className="object-contain bg-transparent"
					height={150}
					src={src}
					width={150}
				/>
				<CardFooter className="justify-center before:bg-purple-600 border-purple-600/20 border-1 overflow-hidden py-1 absolute before:rounded-xl rounded-large bottom-1 shadow-small ml-1 z-10">
					<p className="text-tiny font-semibold text-dark/80">{title}</p>
				</CardFooter>
			</Card>
		</NavLink>
	);
}
