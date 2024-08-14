namespace PLM.Entities.DTOs.UserEmployee;
public class CreateUserEmployeeDTO(CreatePersonDTO CreatePersonDTO,
                                   string password, string email, int roleId)
{
    [Required(ErrorMessage = "La entidad persona es necesaria para la creación del usuario.")]
    public CreatePersonDTO? CreatePersonDTO { get; } = CreatePersonDTO;

    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
           ErrorMessage = "La contraseña debe tener al menos una mayúscula, una minúscula, un " +
                          "número y un mínimo de 8 caracteres.")]
    public string Password { get; set; } = password;

    [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$",
            ErrorMessage = "El formato del correo no es válido.")]
    [MaxLength(150, ErrorMessage = "El campo correo tiene que ser menor a 150 caracteres")]
    public string Email { get; } = email;

    [Range(1, 4, ErrorMessage = "El valor de rol debe estar entre 1 y 4.")]
    public int RoleId { get; } = roleId;
}