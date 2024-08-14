namespace PLM.Entities.DTOs.ProductProposal;
public class CreateReviewProductProposalDTO(char status, int userEmployeeId,
                                            int productProposalId,
                                            string justification)
{
    [Range(1, int.MaxValue, ErrorMessage = "El campo propuesta de producto es obligatorio.")]
    public int ProductProposalId { get; } = productProposalId;

    [Range(1, int.MaxValue, ErrorMessage = "El campo usuario es obligatorio.")]
    public int UserEmployeeId { get; } = userEmployeeId;

    [Required(ErrorMessage = "El campo estado es obligatorio.")]
    [RegularExpression("^[AR]$", ErrorMessage = "El valor debe ser 'A' o 'R'.")]
    public char Status { get; } = status;

    [Required(ErrorMessage = "El campo justificación es obligatorio.")]
    [StringLength(300, ErrorMessage = "El campo justificación no puede tener más de 300 caracteres.")]
    public string Justification { get; } = justification;
}