import { URL_ROLE } from "../middleware/const";

export const createEmployeeFormConfig = [
    {
        name: "id",
        label: "Cedula",
        type: "text",
        placeholder: "Ingrese su cedula",
        required: true
    },
    {
        name: "name",
        label: "Nombre",
        type: "text",
        placeholder: "Ingrese su nombre",
        required: true
    },
    {
        name: "lastName",
        label: "Primer Apellido",
        type: "text",
        placeholder: "Ingrese su primer apellido",
        required: true
    },
    {
        name: "secondLastName",
        label: "Segundo Apellido",
        type: "text",
        placeholder: "Ingrese su segundo apellido",
        required: true
    },
    {
        name: "address",
        label: "Direccion",
        type: "textArea",
        placeholder: "Ingrese su direccion",
        required: true
    },
    {
        name: "birthday",
        label: "Edad",
        type: "date",
        required: true
    },
    {
        name: "phoneNumber",
        label: "Celular",
        type: "text",
        placeholder: "Ingrese su numero celular",
        required: true
    },
    {
        name: "password",
        label: "Contraseña",
        type: "password",
        placeholder: "Ingrese su numero contraseña",
        required: true
    },
    {
        name: "email",
        label: "Correo",
        type: "email",
        placeholder: "Ingrese su correo",
        required: true
    },
    {
        name: "roleId",
        label: "Rol",
        type: "select",
        placeholder: "Seleccione un rol",
        required: true,
        options: [],
        dataSource: URL_ROLE,
        optionValueField: "role_id", // Campo a usar como value
        optionLabelField: "description", // Campo a usar como label
    },
]

export const updateEmployeeFormConfig = [
    {
        name: "user_employee_id",
        label: "Cedula",
        type: "text",
        placeholder: "",
        required: false,
        inVisible: true
    },
    {
        name: "address",
        label: "Direccion",
        type: "textArea",
        placeholder: "Ingrese su direccion",
        required: true
    },
    {
        name: "phone_number",
        label: "Celular",
        type: "text",
        placeholder: "Ingrese su numero celular",
        required: true
    },
    {
        name: "email",
        label: "Correo",
        type: "email",
        placeholder: "Ingrese su correo",
        required: true
    },
    {
        name: "role",
        label: "Rol",
        type: "select",
        placeholder: "Seleccione un rol",
        required: true,
        options: [],
        dataSource: URL_ROLE,
        optionValueField: "role_id", // Campo a usar como value
        optionLabelField: "description", // Campo a usar como label
    },
]