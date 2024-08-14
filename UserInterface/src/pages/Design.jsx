import { useEffect, useState } from "react";
import FormDynamic from "../components/FormDynamic";
import {
	createDesignFormConfig,
	evaluateDesignFormConfig,
	updateDesignFormConfig,
} from "../config/designFormConfig";
import SearchLayout from "../layouts/SearchLayout";
import { URL_DESIGN, URL_REVIEW_DESIGN } from "../middleware/const";
import Swal from "sweetalert2";
import { httpHelper } from "../middleware/httpHelper";
import { convertFileToBase64 } from "../services/converterHelper";

const COLUMNS_TABLE = [
	{
		key: "name",
		label: "Nombre",
	},
	{
		key: "last_modification",
		label: "Fecha de creación",
	},
	{
		key: "status",
		label: "Estado",
	},
	{
		key: "full_name",
		label: "Empleado",
	},
	{
		key: "current_desing_path",
		label: "Archivo",
	},
];

const INITIAL_SEARCH = {
	Nombre: "",
	Fecha: "",
};

const SEARCH_CONFIG = [
	{
		ParamOne: "search",
		ParamTwo: "date",
	},
	{
		ParamOne: "Nombre",
		ParamTwo: "Fecha",
	},
];

export default function Design() {
	const [submit, setSubmit] = useState(false);
	const [inError, setInError] = useState(false);
	const [validations, setValidations] = useState([]);
	const [action, setAction] = useState("Crear");
	const [updateData, setUpdateData] = useState({});
	const [newUpdateConfig, setNewUpdateConfig] = useState([]);
	const [evaluateData, setEvaluateData] = useState({});
	const [newEvaluateConfig, setNewEvaluateConfig] = useState([]);

	const handlerDelete = async (data) => {
		await Swal.fire({
			title: "¿Está seguro?",
			text: "¡No podrás revertirlo!",
			icon: "warning",
			showCancelButton: true,
			confirmButtonColor: "#3085d6",
			cancelButtonColor: "#d33",
			confirmButtonText: "¡Sí, elimínalo!",
		}).then((result) => {
			if (result.isConfirmed) {
				httpHelper()
					.del(URL_DESIGN.concat(`/${data.design_id}`), {
						headers: {
							"Content-Type": "application/json",
						},
					})
					.then((res) => {
						if (res.code >= 0) {
							Swal.fire({
								position: "center",
								icon: "success",
								title: res.message,
								showConfirmButton: false,
								timer: 4000,
							});
							setAction("Delete");
							return;
						}
						if (res.code <= 0) {
							Swal.fire({
								position: "center",
								icon: "error",
								title: res.message,
								showConfirmButton: false,
								timer: 4000,
							});
							return;
						}
					});
			}
		});
	};

	const handlerEvaluate = (data) => {
		const dataToSend = {
			designId: data.design_id,
			userEmployeeId: data.userEmployeeId,
			status: data.status,
			justification: data.justification,
		};

		httpHelper()
			.post(URL_REVIEW_DESIGN, {
				body: dataToSend,
				headers: {
					"Content-Type": "application/json",
				},
			})
			.then((res) => {
				if (res.code >= 0) {
					Swal.fire({
						position: "center",
						icon: "success",
						title: res.message,
						showConfirmButton: false,
						timer: 4000,
					});
					setSubmit(true);
					return;
				}
				if (res.code === -4) {
					setValidations(res.content);
					setInError(true);
					return;
				}
				if (res.code <= 0) {
					Swal.fire({
						position: "center",
						icon: "error",
						title: res.message,
						showConfirmButton: false,
						timer: 4000,
					});
					setSubmit(true);
					return;
				}
			});
	};

	const handlerUpdate = async (data) => {
		const file = await convertFileToBase64(data.file);
		const content = file.split(",")[1];

		const dataToSend = {
			id: data.design_id,
			userEmployeeId: data.userEmployeeId,
			fileUploadDTO: {
				contentType: data.file.type,
				content: content,
			},
		};

		httpHelper()
			.put(URL_DESIGN, {
				body: dataToSend,
				headers: {
					"Content-Type": "application/json",
				},
			})
			.then((res) => {
				if (res.code >= 0) {
					Swal.fire({
						position: "center",
						icon: "success",
						title: res.message,
						showConfirmButton: false,
						timer: 4000,
					});
					setSubmit(true);
					return;
				}
				if (res.code === -4) {
					setValidations(res.content);
					setInError(true);
					return;
				}
				if (res.code <= 0) {
					Swal.fire({
						position: "center",
						icon: "error",
						title: res.message,
						showConfirmButton: false,
						timer: 4000,
					});
					setSubmit(true);
					return;
				}
			});
	};

	const handlerSubmit = async (data) => {
		const file = await convertFileToBase64(data.file);
		const content = file.split(",")[1];

		const dataToSend = {
			name: data.name,
			reviewProductProposalId: data.reviewProductProposalId,
			userEmployeeId: data.userEmployeeId,
			fileUploadDTO: {
				contentType: data.file.type,
				content: content,
			},
		};

		httpHelper()
			.post(URL_DESIGN, {
				body: dataToSend,
				headers: {
					"Content-Type": "application/json",
				},
			})
			.then((res) => {
				if (res.code >= 0) {
					Swal.fire({
						position: "center",
						icon: "success",
						title: res.message,
						showConfirmButton: false,
						timer: 4000,
					});
					setSubmit(true);
					return;
				}
				if (res.code === -4) {
					setValidations(res.content);
					setInError(true);
					return;
				}
				if (res.code <= 0) {
					Swal.fire({
						position: "center",
						icon: "error",
						title: res.message,
						showConfirmButton: false,
						timer: 4000,
					});
					setSubmit(true);
					return;
				}
			});
	};

	useEffect(() => setInError(false), [submit]);

	useEffect(() => {
		if (Object.keys(updateData).length > 0) {
			const updatedConfig = updateDesignFormConfig.map((field) => ({
				...field,
				value: updateData[field.name] || "",
			}));
			setNewUpdateConfig(updatedConfig);
		}

		if (Object.keys(evaluateData).length > 0) {
			const evaluateConfig = evaluateDesignFormConfig.map((field) => ({
				...field,
				value: evaluateData[field.name] || "",
			}));
			setNewEvaluateConfig(evaluateConfig);
		}
	}, [updateData, evaluateData]);

	return (
		<SearchLayout
			title="Propuestas de Diseños"
			initialSearch={INITIAL_SEARCH}
			url={URL_DESIGN}
			columns={COLUMNS_TABLE}
			searchConfig={SEARCH_CONFIG}
			primaryKey="design_id"
			searchParamsMapper={{
				Nombre: "name",
				Fecha: "lastModification",
			}}
			edit={true}
			del={true}
			file="current_desing_path"
			submit={submit}
			setSubmit={setSubmit}
			action={action}
			setAction={setAction}
			setUpdateData={setUpdateData}
			evaluate={true}
			setEvaluateData={setEvaluateData}
			handlerDelete={handlerDelete}
		>
			<FormDynamic
				formConfig={
					action === "Crear"
						? createDesignFormConfig
						: action === "Actualizar"
						? newUpdateConfig
						: newEvaluateConfig
				}
				onSubmit={
					action === "Crear"
						? handlerSubmit
						: action === "Actualizar"
						? handlerUpdate
						: handlerEvaluate
				}
				inError={inError}
				action={action}
			>
				<section className="flex flex-col text-red-600">
					{validations.map((validation, index) => (
						<span key={index}>{validation}</span>
					))}
				</section>
			</FormDynamic>
		</SearchLayout>
	);
}
