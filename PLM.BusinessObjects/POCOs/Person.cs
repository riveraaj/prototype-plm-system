namespace PLM.BusinessObjects.POCOs;
public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string SecondLastName { get; set; }
    public string Address { get; set; }
    public DateOnly Birthday { get; set; }
    public int PhoneNumber { get; set; }
}