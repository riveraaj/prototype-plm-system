namespace PLM.Entities.DTOs.Design;
public class CreateDesignCommentDTO(string description, int userEmployeeId,
                                    int designId)
{
    [Required(ErrorMessage = "El campo descipción es obligatorio.")]
    [StringLength(300, ErrorMessage = "El campo descipción no puede tener más de 300 caracteres.")]
    public string Description { get; } = description;

    [Range(1, int.MaxValue, ErrorMessage = "El campo usuario es obligatorio.")]
    public int UserEmployeeId { get; } = userEmployeeId;

    [Range(1, int.MaxValue, ErrorMessage = "El campo diseño es obligatorio.")]
    public int DesignId { get; } = designId;
}