import { URL_EMPLOYEE, URL_REVIEW_PROPOSAL } from "../middleware/const";

export const createDesignFormConfig = [
    {
        name: "name",
        label: "Nombre",
        type: "text",
        placeholder: "Ingrese el nombre del diseno",
        required: true
    },
    {
        name: "reviewProductProposalId",
        label: "Propuestas",
        type: "select",
        placeholder: "Seleccione una propuesta",
        required: true,
        options: [],
        dataSource: URL_REVIEW_PROPOSAL.concat("/get"),
        optionValueField: "review_product_proposal_id", // Campo a usar como value
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
];

export const updateDesignFormConfig = [
    {
        name: "design_id",
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

export const evaluateDesignFormConfig = [
    {
        name: "design_id",
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