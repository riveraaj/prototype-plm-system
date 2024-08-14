namespace PLM.Entities.DTOs.Idea;
public class CreateIdeaDTO(string name, string description,
                           int userEmployeeId,
                           int categoryIdeaId)
{
    [Required(ErrorMessage = "El campo nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El campo nombre no puede tener más de 100 caracteres.")]
    public string Name { get; } = name;

    [Required(ErrorMessage = "El campo descripción es obligatorio.")]
    [StringLength(300, ErrorMessage = "El campo descripción no puede tener más de 300 caracteres.")]
    public string Description { get; } = description;

    [Range(1, int.MaxValue, ErrorMessage = "El campo usuario es obligatorio.")]
    public int UserEmployeeId { get; } = userEmployeeId;

    [Range(1, int.MaxValue, ErrorMessage = "El campo categoría es obligatorio.")]
    public int CategoryIdeaId { get; } = categoryIdeaId;
}