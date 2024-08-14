namespace PLM.Entities.DTOs.ProductProposal;
public class CreateProductProposalDTO(int ideaId, int userEmployeeId,
                                      FileUploadDTO fileUploadDTO)
{
    [Range(1, int.MaxValue, ErrorMessage = "El campo idea es obligatorio.")]
    public int IdeaId { get; } = ideaId;

    [Range(1, int.MaxValue, ErrorMessage = "El campo usuario es obligatorio.")]
    public int UserEmployeeId { get; } = userEmployeeId;
    public string FilePath { get; set; } = "";

    [Required(ErrorMessage = "El archivo es obligatorio.")]
    public FileUploadDTO? FileUploadDTO { get; } = fileUploadDTO;
}