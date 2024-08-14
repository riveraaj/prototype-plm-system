namespace PLM.BusinessObjects.POCOs;
public class ReviewDesign
{
    public int ReviewDesignId { get; set; }
    public DateOnly EvaluationDate { get; set; }
    public string Justification { get; set; }
    public int DesignId { get; set; }
    public int UserEmployeeId { get; set; }
}