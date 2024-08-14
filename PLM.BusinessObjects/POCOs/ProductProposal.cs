namespace PLM.BusinessObjects.POCOs;
public class ProductProposal
{
    public int ProductProposalId { get; set; }
    public DateOnly DateCreation { get; set; }
    public char Status { get; set; }
    public string FilePath { get; set; }
    public int IdeaId { get; set; }
    public int UserEmployeeId { get; set; }
}