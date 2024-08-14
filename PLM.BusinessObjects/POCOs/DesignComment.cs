namespace PLM.BusinessObjects.POCOs;
public class DesignComment
{
    public int DesignCommentId { get; set; }
    public string Description { get; set; }
    public int UserEmployeeId { get; set; }
    public int DesignId { get; set; }
}