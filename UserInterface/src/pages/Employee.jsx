import { URL_EMPLOYEE } from "../middleware/const";
import SearchLayout from "../layouts/SearchLayout";
import { useEffect, useState } from "react";
import { httpHelper } from "../middleware/httpHelper";
import FormDynamic from "../components/FormDynamic";
import {
	createEmployeeFormConfig,
	updateEmployeeFormConfig,
} from "../config/employeeFormConfig";
import Swal from "sweetalert2";
import { formatPickerDateComplete } from "../services/converterHelper";

const COLUMNS_TABLE = [
	{
		key: "email",
		label: "Correo",
	},
	{
		key: "role",
		label: "Rol",
	},
	{
		key: "full_name",
		label: "Nombre",
	},
	{
		key: "address",
		label: "Dirección",
	},
	{
		key: "phone_number",
		label: "Número celular",
	},
];

const INITIAL_SEARCH = {
	Nombre: "",
	Cédula: "",
};

const SEARCH_CONFIG = [
	{
		ParamOne: "search",
		ParamTwo: "search",
	},
	{
		ParamOne: "Nombre",
		ParamTwo: "Cédula",
	},
];

export default function Employee() {
	const [submit, setSubmit] = useState(false);
	const [inError, setInError] = useState(false);
	const [validations, setValidations] = useState([]);
	const [action, setAction] = useState("Crear");
	const [updateData, setUpdateData] = useState({});
	const [newUpdateConfig, setNewUpdateConfig] = useState([]);

	const handlerDelete = (data) => {
		Swal.fire({
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
					.del(URL_EMPLOYEE.concat(`/${data.user_employee_id}`), {
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
			id: data.user_employee_id,
			email: data.email,
			roleId: data.role,
			updatePersonDTO: {
				address: data.address,
				phoneNumber: data.phone_number,
			},
		};

		httpHelper()
			.put(URL_EMPLOYEE, {
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
		const dataToSend = {
			createPersonDTO: {
				id: data.id,
				name: data.name,
				lastName: data.lastName,
				secondLastName: data.secondLastName,
				address: data.address,
				birthday: formatPickerDateComplete(data.birthday),
				phoneNumber: data.phoneNumber,
			},
			password: data.password,
			email: data.email,
			roleId: data.roleId,
		};

		httpHelper()
			.post(URL_EMPLOYEE, {
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
			const updatedConfig = updateEmployeeFormConfig.map((field) => ({
				...field,
				value: updateData[field.name] || "",
			}));
			setNewUpdateConfig(updatedConfig);
		}
	}, [updateData]);

	return (
		<SearchLayout
			title="Empleados"
			initialSearch={INITIAL_SEARCH}
			url={URL_EMPLOYEE}
			columns={COLUMNS_TABLE}
			primaryKey="user_employee_id"
			searchConfig={SEARCH_CONFIG}
			searchParamsMapper={{
				Nombre: "name",
				Cédula: "identification",
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
				formConfig={
					action === "Crear" ? createEmployeeFormConfig : newUpdateConfig
				}
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
