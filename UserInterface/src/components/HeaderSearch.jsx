/* eslint-disable react/prop-types */
import { Button, DatePicker, Input } from "@nextui-org/react";

export default function HeaderSearch({
	title,
	searchConfig,
	handleSearchButton,
	onOpen,
	search,
	handlerSearch,
	setAction,
	handlerDownload,
	report = false,
}) {
	return (
		<header className="w-[100%] container mx-auto py-10">
			<h3 className="font-bold text-2xl text-center">{title}</h3>

			<Button
				variant="ghost"
				color="secondary"
				className="mt-8 font-medium"
				onClick={() => {
					setAction("Crear");
					onOpen();
				}}
			>
				Nuevo
			</Button>

			<section className="mt-10 flex flex-row items-center">
				<article className="flex flex-col gap-5">
					{Object.keys(searchConfig[1]).map((key) => {
						const item = searchConfig[1][key];
						const paramType = searchConfig[0][key]; // Accede al paramType basado en la clave

						return paramType === "date" ? (
							<DatePicker
								key={item}
								size="lg"
								label={item}
								color="secondary"
								labelPlacement="outside-left"
								className="max-w-[284px] font-semibold"
								name={item}
								id={item}
								value={search[item] || null}
								variant="bordered"
								onChange={(date) => handlerSearch({ name: item, value: date })}
							/>
						) : (
							<Input
								variant="bordered"
								key={item}
								className="font-semibold"
								type="search"
								label={item}
								labelPlacement="outside-left"
								placeholder="Escriba para buscar..."
								size="lg"
								name={item}
								color="secondary"
								id={item}
								value={search[item]}
								onChange={handlerSearch}
							/>
						);
					})}
				</article>

				<Button
					variant="ghost"
					onClick={handleSearchButton}
					color="secondary"
					className="ml-24 font-medium"
				>
					Buscar
				</Button>

				{report && (
					<Button
						variant="ghost"
						color="secondary"
						className="ml-5 font-medium"
						onClick={handlerDownload}
					>
						Descargar
					</Button>
				)}
			</section>
		</header>
	);
}
