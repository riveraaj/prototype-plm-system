namespace PLM.Entities.DTOs.Design;
public class CreateDesignDTO(string name,
                             FileUploadDTO fileUploadDTO,
                             int reviewProductProposalId,
                             int userEmployeeId)
{
    [Required(ErrorMessage = "El campo nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El campo nombre no puede tener más de 100 caracteres.")]
    public string Name { get; } = name;

    public string CurrentDesignPath { get; set; } = "";

    [Required(ErrorMessage = "El archivo es obligatorio.")]
    public FileUploadDTO? FileUploadDTO { get; } = fileUploadDTO;

    [Range(1, int.MaxValue, ErrorMessage = "El campo propuestas evaluadas es obligatorio.")]
    public int ReviewProductProposalId { get; } = reviewProductProposalId;

    [Range(1, int.MaxValue, ErrorMessage = "El campo usuario es obligatorio.")]
    public int UserEmployeeId { get; } = userEmployeeId;
}