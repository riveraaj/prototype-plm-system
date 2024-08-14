namespace PLM.BusinessObjects.POCOs;
public class Design()
{
    public int DesignId { get; set; }
    public string Name { get; set; }
    public char Status { get; set; }
    public string CurrentDesignPath { get; set; }
    public DateOnly LastModification { get; set; }
    public int ReviewProductProposalId { get; set; }
    public int UserEmployeeId { get; set; }
}