import { URL_CATEGORY, URL_EMPLOYEE } from "../middleware/const";

export const createIdeaFormConfig = [
    {
        name: "name",
        label: "Nombre",
        type: "text",
        placeholder: "Ingrese el nombre de la idea",
        required: true
    },
    {
        name: "description",
        label: "Descripcion",
        type: "textArea",
        placeholder: "Ingrese la justificacion de la idea",
        required: true
    },
    {
        name: "categoryIdeaId",
        label: "Categoria",
        type: "select",
        placeholder: "Seleccione una categoria",
        required: true,
        options: [],
        dataSource: URL_CATEGORY,
        optionValueField: "category_idea_id", // Campo a usar como value
        optionLabelField: "description", // Campo a usar como label
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
]

export const updateIdeaFormConfig = [
    {
        name: "idea_id",
        label: "Nombre",
        type: "text",
        placeholder: "Ingrese el nombre de la idea",
        required: true,
        inVisible: true
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

]