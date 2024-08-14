/* eslint-disable react/prop-types */
import {
	Button,
	DateInput,
	Input,
	Select,
	SelectItem,
	Textarea,
} from "@nextui-org/react";
import { useEffect, useState } from "react";
import { EyeFilledIcon } from "../assets/Icon/EyeFilledIcon";
import { EyeSlashFilledIcon } from "../assets/Icon/EyeSlashFilledIcon";
import { httpHelper } from "../middleware/httpHelper";

export default function FormDynamic({
	formConfig,
	onSubmit,
	inError,
	children,
	action,
}) {
	const [formData, setFormData] = useState({});
	const [selectOptions, setSelectOptions] = useState({});
	const [isVisible, setIsVisible] = useState(false);

	const toggleVisibility = () => setIsVisible(!isVisible);

	const handleChange = (e) => {
		const { name, value } = e.target;
		setFormData({ ...formData, [name]: value });
	};

	const handleSelectChange = (name, value) => {
		setFormData((prevData) => ({ ...prevData, [name]: value }));
	};

	const handleSubmit = (e) => {
		e.preventDefault();
		onSubmit(formData);
	};

	useEffect(() => {
		if (action !== "Crear") {
			const initialFormData = {};
			formConfig.forEach((field) => {
				initialFormData[field.name] = field.value || "";
			});
			setFormData(initialFormData);
		}

		const newSelectOptions = {};
		formConfig.forEach((field) => {
			if (field.type === "select" && field.dataSource) {
				httpHelper()
					.get(field.dataSource)
					.then((res) => {
						if (res.code >= 0) {
							const options = res.content.map((item) => ({
								value: item[field.optionValueField],
								label: item[field.optionLabelField],
							}));
							newSelectOptions[field.name] = options;
							setSelectOptions(newSelectOptions); // Actualizar el estado con el nuevo objeto completo
						}
					});
			} else if (field.type === "select" && field.options) {
				// Manejar opciones proporcionadas directamente
				newSelectOptions[field.name] = field.options;
				setSelectOptions(newSelectOptions); // Actualizar el estado con el nuevo objeto completo
			}
		});
	}, [formConfig, action]);

	return (
		<form
			onSubmit={handleSubmit}
			className="flex flex-col w-[50%] gap-2 justify-center items-center mx-auto"
		>
			{formConfig.map((field) => {
				switch (field.type) {
					case "text":
						return (
							<Input
								key={field.name}
								color="secondary"
								isRequired={field.required}
								required={field.required}
								type={field.type}
								name={field.name}
								placeholder={field.placeholder}
								value={formData[field.name] || ""}
								onChange={handleChange}
								label={field.label}
								className={field.inVisible ? "invisible" : ""}
							/>
						);
					case "email":
						return (
							<Input
								key={field.name}
								isClearable
								color="secondary"
								isRequired={field.required}
								type={field.type}
								name={field.name}
								placeholder={field.placeholder}
								value={formData[field.name] || ""}
								onChange={handleChange}
								label={field.label}
								onClear={() => console.log("input cleared")}
							/>
						);
					case "password":
						return (
							<Input
								key={field.name}
								color="secondary"
								isRequired={field.required}
								type={isVisible ? "text" : field.type}
								name={field.name}
								placeholder={field.placeholder}
								value={formData[field.name] || ""}
								onChange={handleChange}
								label={field.label}
								endContent={
									<button
										className="focus:outline-none"
										type="button"
										onClick={toggleVisibility}
										aria-label="toggle password visibility"
									>
										{isVisible ? (
											<EyeSlashFilledIcon className="text-2xl text-default-400 pointer-events-none" />
										) : (
											<EyeFilledIcon className="text-2xl text-default-400 pointer-events-none" />
										)}
									</button>
								}
							/>
						);
					case "select":
						return (
							<Select
								key={field.name}
								isRequired={field.required}
								label={field.label}
								color="secondary"
								placeholder={field.placeholder}
								value={formData[field.name] || ""}
								onChange={(item) =>
									handleSelectChange(field.name, item.target.value)
								}
							>
								{selectOptions[field.name]?.map((option) => (
									<SelectItem key={option.value} value={option.value}>
										{option.label}
									</SelectItem>
								))}
							</Select>
						);
					case "textArea":
						return (
							<Textarea
								key={field.name}
								color="secondary"
								isRequired={field.required}
								label={field.label}
								placeholder={field.placeholder}
								name={field.name}
								onChange={handleChange}
								value={formData[field.name] || ""}
							/>
						);
					case "date":
						return (
							<DateInput
								key={field.name}
								color="secondary"
								label={field.label}
								name={field.name}
								onChange={(item) => handleSelectChange(field.name, item)}
								value={formData[field.name]}
							/>
						);
					case "file":
						return (
							<Input
								key={field.name}
								color="secondary"
								isRequired={field.required}
								required={field.required}
								type={field.type}
								name={field.name}
								placeholder={field.placeholder}
								onChange={(item) =>
									handleSelectChange(field.name, item.target.files[0])
								}
								label={field.label}
								accept=".docx, .txt, .pdf"
							/>
						);
					default:
						return null;
				}
			})}
			{inError && children}
			<Button color="secondary" variant="flat" type="submit">
				Enviar
			</Button>
		</form>
	);
}
