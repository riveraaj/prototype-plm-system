namespace PLM.BusinessObjects.POCOs;
public class Idea
{
    public int IdeaId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly DateCreation { get; set; }
    public char Status { get; set; }
    public int UserEmployeeId { get; set; }
    public int CategoryIdeaId { get; set; }
}