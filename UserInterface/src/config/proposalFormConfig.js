import { URL_EMPLOYEE, URL_IDEA } from "../middleware/const";

export const createProposalFormConfig = [
    {
        name: "ideaId",
        label: "Idea",
        type: "select",
        placeholder: "Seleccione una idea",
        required: true,
        options: [],
        dataSource: URL_IDEA.concat("/get"),
        optionValueField: "idea_id", // Campo a usar como value
        optionLabelField: "name", // Campo a usar como label
    },
    {
        name: "userEmployeeId",
        label: "Empleado",
        type: "select",
        placeholder: "Seleccione un empleado",
        required: true,
        options: [],
        dataSource: URL_EMPLOYEE,
        optionValueField: "user_employee_id", // Campo a usar como value
        optionLabelField: "full_name", // Campo a usar como label
    },
    {
        name: "file",
        label: "Archivo",
        type: "file",
        placeholder: "Cargue el archivo",
        required: true
    },
]

export const updateProposalFormConfig = [
    {
        name: "product_proposal_id",
        label: "id",
        type: "text",
        placeholder: "",
        required: false,
        inVisible: true
    },
    {
        name: "userEmployeeId",
        label: "Empleado",
        type: "select",
        placeholder: "Seleccione un empleado",
        required: true,
        options: [],
        dataSource: URL_EMPLOYEE,
        optionValueField: "user_employee_id", // Campo a usar como value
        optionLabelField: "full_name", // Campo a usar como label
    },
    {
        name: "file",
        label: "Archivo",
        type: "file",
        placeholder: "Cargue el archivo",
        required: true
    },
]

export const evaluateProposalFormConfig = [
    {
        name: "product_proposal_id",
        label: "id",
        type: "text",
        placeholder: "",
        required: false,
        inVisible: true
    },
    {
        name: "userEmployeeId",
        label: "Empleado",
        type: "select",
        placeholder: "Seleccione un empleado",
        required: true,
        options: [],
        dataSource: URL_EMPLOYEE,
        optionValueField: "user_employee_id", // Campo a usar como value
        optionLabelField: "full_name", // Campo a usar como label
    },
    {
        name: "status",
        label: "Evaluacion",
        type: "select",
        placeholder: "Seleccione una opcion",
        required: true,
        options: [{
            value: "A",
            label: "Aprobar"
        },
        {
            value: "R",
            label: "Rechazar"
        },
        ]
    },
    {
        name: "justification",
        label: "Justificacion",
        type: "textArea",
        placeholder: "Ingrese la justificacion",
        required: true
    },
]