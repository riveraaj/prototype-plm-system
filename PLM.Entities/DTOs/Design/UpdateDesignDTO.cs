namespace PLM.Entities.DTOs.Design;
public class UpdateDesignDTO(int id, int userEmployeeId,
                             FileUploadDTO fileUploadDTO)
{
    [Range(1, int.MaxValue, ErrorMessage = "El campo id es obligatorio.")]
    public int Id { get; } = id;

    [Range(1, int.MaxValue, ErrorMessage = "El campo usuario es obligatorio.")]
    public int UserEmployeeId { get; } = userEmployeeId;

    public string CurrentDesignPath { get; set; } = "";

    [Required(ErrorMessage = "El archivo es obligatorio.")]
    public FileUploadDTO? FileUploadDTO { get; } = fileUploadDTO;
}