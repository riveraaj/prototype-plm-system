namespace PLM.BusinessObjects.POCOs;
public class UserEmployee : Person
{
    public int UserEmployeeId { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public bool Active { get; set; }
    public int RoleId { get; set; }
}