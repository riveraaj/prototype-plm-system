namespace PLM.Entities.DTOs.Person;
public class UpdatePersonDTO(string address, int phoneNumber)
{
    [Required(ErrorMessage = "El campo dirección es obligatorio.")]
    [StringLength(300, ErrorMessage = "El campo dirección no puede tener más de 300 caracteres.")]
    public string Address { get; } = address;

    [Range(1, int.MaxValue, ErrorMessage = "El campo número de celular es obligatorio.")]
    public int PhoneNumber { get; } = phoneNumber;
}