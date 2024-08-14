namespace PLM.Entities.DTOs.Person;
public class CreatePersonDTO(int id, string name, string lastName,
                             string secondLastName, string address,
                             DateOnly birthday, int phoneNumber)
{
    [Range(1, int.MaxValue, ErrorMessage = "El campo identificación es obligatorio.")]
    public int Id { get; } = id;

    [Required(ErrorMessage = "El campo nombre es obligatorio.")]
    [StringLength(30, ErrorMessage = "El campo nombre no puede tener más de 30 caracteres.")]
    public string Name { get; } = name;

    [Required(ErrorMessage = "El campo apellido es obligatorio.")]
    [StringLength(30, ErrorMessage = "El campo apellido no puede tener más de 30 caracteres.")]
    public string LastName { get; } = lastName;

    [Required(ErrorMessage = "El campo segundo apellido es obligatorio.")]
    [StringLength(30, ErrorMessage = "El campo segundo apellido no puede tener más de 30 caracteres.")]
    public string SecondLastName { get; } = secondLastName;

    [Required(ErrorMessage = "El campo dirección es obligatorio.")]
    [StringLength(300, ErrorMessage = "El campo dirección no puede tener más de 300 caracteres.")]
    public string Address { get; } = address;

    [DataType(DataType.Date, ErrorMessage = "El campo fecha de nacimiento  debe ser una fecha válida.")]
    public DateOnly Birthday { get; } = birthday;

    [Range(1, int.MaxValue, ErrorMessage = "El campo número de celular es obligatorio.")]
    public int PhoneNumber { get; } = phoneNumber;
}