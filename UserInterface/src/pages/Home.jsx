import LinkCard from "../components/LinkCard";

export default function Home() {
	return (
		<section className="flex justify-center items-center flex-wrap gap-20 px-[20%] pt-[10%]">
			<LinkCard
				title="Empleados"
				src="/images/employees.png"
				path="/empleados"
				width="w-[150px]"
			/>
			<LinkCard
				title="Ideas"
				src="/images/idea.png"
				path="/ideas"
				width="w-[150px]"
			/>
			<LinkCard
				title="Propuestas"
				src="/images/proposal.png"
				path="/propuestas"
				width="w-[150px]"
			/>
			<LinkCard
				title="Diseños"
				src="/images/designs.png"
				path="/disenos"
				width="w-[150px]"
			/>
			<LinkCard
				title="Reporte de Propuestas"
				src="/images/reportProposal.png"
				path="/reporte/propuestas"
				width="w-[150px]"
			/>
			<LinkCard
				title="Reporte de Diseños"
				src="/images/reportDesign.png"
				path="/reporte/disenos"
				width="w-[150px]"
			/>
		</section>
	);
}
