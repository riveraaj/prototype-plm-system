import { URL_IDEA } from "../middleware/const";
import SearchLayout from "../layouts/SearchLayout";
import FormDynamic from "../components/FormDynamic";
import {
	createIdeaFormConfig,
	updateIdeaFormConfig,
} from "../config/ideaFormConfig";
import { useEffect, useState } from "react";
import { httpHelper } from "../middleware/httpHelper";
import Swal from "sweetalert2";

const COLUMNS_TABLE = [
	{
		key: "name",
		label: "Nombre",
	},
	{
		key: "description",
		label: "Descripción",
	},
	{
		key: "date_creation",
		label: "Fecha de creación",
	},
	{
		key: "status",
		label: "Estado",
	},
	{
		key: "category",
		label: "Categoria",
	},
	{
		key: "full_name",
		label: "Empleado",
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

export default function Idea() {
	const [submit, setSubmit] = useState(false);
	const [inError, setInError] = useState(false);
	const [validations, setValidations] = useState([]);
	const [action, setAction] = useState("Crear");
	const [updateData, setUpdateData] = useState({});
	const [newUpdateConfig, setNewUpdateConfig] = useState([]);

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
					.del(URL_IDEA.concat(`/${data.idea_id}`), {
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

	const handlerUpdate = (data) => {
		const dataToSend = {
			id: data.idea_id,
			status: data.status,
			userEmployeeId: data.userEmployeeId,
		};

		httpHelper()
			.put(URL_IDEA, {
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

	const handlerSubmit = (data) => {
		httpHelper()
			.post(URL_IDEA, {
				body: data,
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
			const updatedConfig = updateIdeaFormConfig.map((field) => ({
				...field,
				value: updateData[field.name] || "",
			}));
			setNewUpdateConfig(updatedConfig);
		}
	}, [updateData]);

	return (
		<SearchLayout
			title="Ideas"
			initialSearch={INITIAL_SEARCH}
			url={URL_IDEA}
			columns={COLUMNS_TABLE}
			searchConfig={SEARCH_CONFIG}
			primaryKey="idea_id"
			searchParamsMapper={{
				Nombre: "name",
				Fecha: "date",
			}}
			edit={true}
			del={true}
			submit={submit}
			setSubmit={setSubmit}
			action={action}
			setAction={setAction}
			setUpdateData={setUpdateData}
			handlerDelete={handlerDelete}
		>
			<FormDynamic
				formConfig={action === "Crear" ? createIdeaFormConfig : newUpdateConfig}
				onSubmit={action === "Crear" ? handlerSubmit : handlerUpdate}
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
