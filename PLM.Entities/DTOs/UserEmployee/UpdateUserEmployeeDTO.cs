namespace PLM.Entities.DTOs.UserEmployee;
public class UpdateUserEmployeeDTO(UpdatePersonDTO UpdatePersonDTO,
                                   int id, string email, int roleId)
{
    [Range(1, int.MaxValue, ErrorMessage = "El campo identificador es obligatorio.")]
    public int Id { get; } = id;

    public UpdatePersonDTO UpdatePersonDTO { get; } = UpdatePersonDTO;

    [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$",
            ErrorMessage = "El formato del correo no es válido.")]
    [MaxLength(150, ErrorMessage = "El campo correo tiene que ser menor a 150 caracteres")]
    public string Email { get; } = email;

    [Range(1, 4, ErrorMessage = "El valor de rol debe estar entre 1 y 4.")]
    public int RoleId { get; } = roleId;
}