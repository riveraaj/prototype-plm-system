import { URL_REVIEW_PROPOSAL } from "../middleware/const";
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

export default function ReportProposal() {
	return (
		<SearchLayout
			title="Reporte de Propuestas"
			initialSearch={INITIAL_SEARCH}
			url={URL_REVIEW_PROPOSAL}
			columns={COLUMNS_TABLE}
			searchConfig={SEARCH_CONFIG}
			primaryKey="review_product_proposal_id"
			searchParamsMapper={{
				Empleado: "username",
				Estado: "status",
			}}
			urlReporter={"http://localhost:3000/api/proposalReport"}
			report={true}
			reportName={"propuesta"}
		/>
	);
}
