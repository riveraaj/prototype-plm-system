/* eslint-disable react/prop-types */
import {
	Button,
	Dropdown,
	DropdownItem,
	DropdownMenu,
	DropdownTrigger,
	Pagination,
	Table,
	TableBody,
	TableCell,
	TableColumn,
	TableHeader,
	TableRow,
	getKeyValue,
} from "@nextui-org/react";
import { useEffect, useMemo, useState } from "react";
import { VerticalDotsIcon } from "../assets/Icon/VerticalDotsIcon";

export default function TableDynamic({
	columns,
	rows,
	label,
	handlerView,
	handlerEdit,
	handlerDelete,
	handlerEvaluate,
	primaryKey,
	edit = false,
	del = false,
	evaluate = false,
	view = false,
	file,
}) {
	const extendedColumns = [...columns, { key: "actions", label: "Acciones" }];
	const rowsPerPage = 5;
	const [page, setPage] = useState(1);
	const [pages, setPages] = useState(1);

	const items = useMemo(() => {
		const start = (page - 1) * rowsPerPage;
		const end = start + rowsPerPage;

		return rows.slice(start, end);
	}, [page, rows]);

	const handleClick = (path) => {
		window.open(path, "_blank");
	};

	useEffect(() => {
		setPages(Math.ceil(rows.length / rowsPerPage));
	}, [rows]);

	return (
		<Table
			aria-label={label}
			className="max-w-[100%] mx-auto mb-8"
			bottomContent={
				<div className="flex w-full justify-center">
					<Pagination
						isCompact
						showControls
						showShadow
						color="secondary"
						page={page}
						total={pages}
						onChange={(page) => setPage(page)}
					/>
				</div>
			}
		>
			<TableHeader columns={edit || del || view ? extendedColumns : columns}>
				{(column) => (
					<TableColumn key={column.key} className="text-sm">
						{column.label}
					</TableColumn>
				)}
			</TableHeader>
			<TableBody items={items} emptyContent={"No hay nada que mostrar."}>
				{(item) => (
					<TableRow key={item[primaryKey]}>
						{(columnKey) =>
							columnKey === "actions" ? (
								<TableCell>
									<div className="relative flex justify-normal items-center gap-2">
										<Dropdown>
											<DropdownTrigger>
												<Button
													isIconOnly
													size="sm"
													variant="light"
													className="ml-[20%]"
												>
													<VerticalDotsIcon className="text-default-400" />
												</Button>
											</DropdownTrigger>

											<DropdownMenu>
												{view && (
													<DropdownItem
														onClick={() => handlerView(item)}
														className="text-warning-600"
													>
														Detalles
													</DropdownItem>
												)}
												{edit && (
													<DropdownItem
														onClick={() => handlerEdit(item)}
														className="text-blue-600"
													>
														Editar
													</DropdownItem>
												)}
												{del && (
													<DropdownItem
														onClick={() => handlerDelete(item)}
														className="text-red-700"
													>
														Eliminar
													</DropdownItem>
												)}
												{evaluate && (
													<DropdownItem
														onClick={() => handlerEvaluate(item)}
														className="text-purple-600"
													>
														Evaluar
													</DropdownItem>
												)}
											</DropdownMenu>
										</Dropdown>
									</div>
								</TableCell>
							) : columnKey === file ? (
								<TableCell>
									<Button
										color="secondary"
										variant="flat"
										onClick={() => handleClick(getKeyValue(item, columnKey))}
									>
										Abrir
									</Button>
								</TableCell>
							) : (
								<TableCell>{getKeyValue(item, columnKey)}</TableCell>
							)
						}
					</TableRow>
				)}
			</TableBody>
		</Table>
	);
}
