namespace PLM.Entities.DTOs.Idea;
public class UpdateIdeaDTO(int id, char status, int userEmployeeId)
{
    [Range(1, int.MaxValue, ErrorMessage = "El campo id es obligatorio.")]
    public int Id { get; } = id;

    [Range(1, int.MaxValue, ErrorMessage = "El campo usuario es obligatorio.")]
    public int UserEmployeeId { get; } = userEmployeeId;

    [Required(ErrorMessage = "El campo estado es obligatorio.")]
    [RegularExpression("^[AR]$", ErrorMessage = "El valor debe ser 'A' o 'R'.")]
    public char Status { get; } = status;
}