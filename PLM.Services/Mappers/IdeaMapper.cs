namespace PLM.BusinessLogic.Mappers;
internal static class IdeaMapper
{
    public static Idea MapIdea(CreateIdeaDTO oCreateIdeaDTO)
        => new()
        {
            Name = oCreateIdeaDTO.Name,
            Description = oCreateIdeaDTO.Description,
            UserEmployeeId = oCreateIdeaDTO.UserEmployeeId,
            CategoryIdeaId = oCreateIdeaDTO.CategoryIdeaId
        };

    public static Idea MapIdea(UpdateIdeaDTO oUpdateIdeaDTO)
        => new()
        {
            IdeaId = oUpdateIdeaDTO.Id,
            Status = oUpdateIdeaDTO.Status,
            UserEmployeeId = oUpdateIdeaDTO.UserEmployeeId
        };
}