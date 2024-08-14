namespace PLM.BusinessObjects.POCOs;
public class ReviewProductProposal
{
    public int ReviewProductProposalId { get; set; }
    public DateOnly EvaluationDate { get; set; }
    public string Justification { get; set; }
    public int ProductProposalId { get; set; }
    public int UserEmployeeId { get; set; }
}