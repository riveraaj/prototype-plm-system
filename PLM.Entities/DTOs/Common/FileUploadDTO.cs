namespace PLM.Entities.DTOs.Common;
public class FileUploadDTO(string contentType, byte[] content)
{
    public string Name { get; set; } = "";

    [Required]
    public string ContentType { get; } = contentType;

    [Required]
    public byte[] Content { get; } = content;
}