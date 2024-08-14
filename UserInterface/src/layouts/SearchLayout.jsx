/* eslint-disable react/prop-types */
import { useEffect, useState } from "react";
import { httpHelper } from "../middleware/httpHelper";
import { Spinner, useDisclosure } from "@nextui-org/react";
import HeaderSearch from "../components/HeaderSearch";
import TableDynamic from "../components/TableDynamic";
import {
	formatDatesInArray,
	formatPickerDate,
} from "../services/converterHelper";
import ModalForm from "../components/ModalForm";

export default function DynamicPageLayout({
	title,
	initialSearch,
	url,
	columns,
	primaryKey,
	searchParamsMapper,
	searchConfig,
	edit,
	del,
	view,
	evaluate,
	file,
	children,
	submit,
	setEvaluateData,
	setSubmit,
	setUpdateData,
	setAction,
	action,
	handlerDelete,
	urlReporter,
	reportName,
	report,
}) {
	const [data, setData] = useState([]);
	const [search, setSearch] = useState(initialSearch);
	const [isLoading, setIsLoading] = useState(false);
	const { isOpen, onOpen, onOpenChange } = useDisclosure();

	const buildSearchURL = (URL) => {
		const params = Object.keys(search)
			.map((key) => {
				if (key === "Fecha") {
					const date = formatPickerDate(search[key]);
					return `${searchParamsMapper[key] || key}=${date || ""}`;
				} else return `${searchParamsMapper[key] || key}=${search[key].trim()}`;
			})
			.join("&");

		return `${URL}?${params}`;
	};

	const handleSearchButton = () => {
		//resetSearch();
		setIsLoading(true);

		httpHelper()
			.get(buildSearchURL(url))
			.then((res) => {
				if (res.code >= 0) {
					const formattedData = formatDatesInArray(res.content);
					setData(formattedData);
				}
				setIsLoading(false);
			});
	};

	const handlerDownload = () => {
		const today = new Date();
		const day = today.getDate();
		const month = today.getMonth() + 1;
		const year = today.getFullYear();

		const formattedDate = `${day}-${month}-${year}`;

		httpHelper()
			.get(buildSearchURL(urlReporter))
			.then((res) => {
				if (res.Code >= 0) {
					let tempLink = document.createElement("a");
					tempLink.href = `data:application/pdf;base64,${res.Content}`;
					tempLink.setAttribute(
						"download",
						`reporte-de-${reportName}-${formattedDate}.pdf`
					);
					tempLink.click();
				}
			});
	};

	const handlerSearch = (e) => {
		let name, value;

		if (e.target) ({ name, value } = e.target);
		else {
			name = e.name;
			value = e.value;
		}

		setSearch({ ...search, [name]: value });
	};

	//const resetSearch = () => setSearch(initialSearch);

	const handlerUpdate = (data) => {
		setAction("Actualizar");
		setUpdateData(data);
		onOpen();
	};

	// const handlerView = (data) => console.log("Viendo");

	const handlerEvaluate = (data) => {
		setAction("Evaluar");
		setEvaluateData(data);
		onOpen();
	};

	useEffect(() => {
		if (submit) {
			onOpenChange();
			setSubmit(false);
		}

		setIsLoading(true);
		httpHelper()
			.get(url)
			.then((res) => {
				if (res.code >= 0) {
					const formattedData = formatDatesInArray(res.content);
					setData(formattedData);
				}
				setIsLoading(false);
			});
	}, [url, submit, onOpenChange, setSubmit, action]);

	return (
		<>
			<HeaderSearch
				title={title}
				searchConfig={searchConfig}
				handleSearchButton={handleSearchButton}
				onOpen={onOpen}
				handlerSearch={handlerSearch}
				search={search}
				setAction={setAction}
				handlerDownload={handlerDownload}
				report={report}
			/>
			<hr className="pb-10" />
			{isLoading ? (
				<Spinner
					label="Loading..."
					color="secondary"
					className="flex mx-auto"
				/>
			) : (
				<TableDynamic
					rows={data}
					columns={columns}
					label={`Table for ${title}`}
					primaryKey={primaryKey}
					edit={edit}
					del={del}
					view={view}
					file={file}
					evaluate={evaluate}
					handlerDelete={handlerDelete}
					handlerEdit={handlerUpdate}
					handlerEvaluate={handlerEvaluate}
				/>
			)}

			{isOpen ? (
				<ModalForm
					isOpen={isOpen}
					title={`${action} ${title.slice(0, -1)}`}
					setSubmit={setSubmit}
				>
					{children}
				</ModalForm>
			) : (
				""
			)}
		</>
	);
}
