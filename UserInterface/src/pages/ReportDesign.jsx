import { URL_REVIEW_DESIGN } from "../middleware/const";
import SearchLayout from "../layouts/SearchLayout";

const COLUMNS_TABLE = [
	{
		key: "name",
		label: "Idea",
	},
	{
		key: "justification",
		label: "Justificacion",
	},
	{
		key: "status",
		label: "Estado",
	},
	{
		key: "evaluation_date",
		label: "Evaluacion",
	},
	{
		key: "full_name",
		label: "Evaluador",
	},
];

const INITIAL_SEARCH = {
	Empleado: "",
	Estado: "",
};

const SEARCH_CONFIG = [
	{
		ParamOne: "search",
		ParamTwo: "search",
	},
	{
		ParamOne: "Empleado",
		ParamTwo: "Estado",
	},
];

export default function ReportDesign() {
	return (
		<SearchLayout
			title="Reporte de Diseños"
			initialSearch={INITIAL_SEARCH}
			url={URL_REVIEW_DESIGN}
			columns={COLUMNS_TABLE}
			searchConfig={SEARCH_CONFIG}
			primaryKey="review_design_id"
			searchParamsMapper={{
				Empleado: "username",
				Estado: "designname",
			}}
			urlReporter={"http://localhost:3000/api/designReport"}
			report={true}
			reportName={"propuesta"}
		/>
	);
}
