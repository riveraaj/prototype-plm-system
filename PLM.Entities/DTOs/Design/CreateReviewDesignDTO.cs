namespace PLM.Entities.DTOs.Design;
public class CreateReviewDesignDTO(char status, int userEmployeeId,
                                   int designId,
                                   string justification)
{
    [Range(1, int.MaxValue, ErrorMessage = "El campo diseño es obligatorio.")]
    public int DesignId { get; } = designId;

    [Range(1, int.MaxValue, ErrorMessage = "El campo usuario es obligatorio.")]
    public int UserEmployeeId { get; } = userEmployeeId;

    [Required(ErrorMessage = "El campo estado es obligatorio.")]
    [RegularExpression("^[AR]$", ErrorMessage = "El valor debe ser 'A' o 'R'.")]
    public char Status { get; } = status;

    [Required(ErrorMessage = "El campo justificación es obligatorio.")]
    [StringLength(300, ErrorMessage = "El campo justificación no puede tener más de 300 caracteres.")]
    public string Justification { get; } = justification;
}