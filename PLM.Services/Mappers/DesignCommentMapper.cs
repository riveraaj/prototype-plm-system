namespace PLM.BusinessLogic.Mappers;
internal static class DesignCommentMapper
{
    public static DesignComment MapDesignComment(CreateDesignCommentDTO oCreateDesignCommentDTO)
        => new()
        {
            Description = oCreateDesignCommentDTO.Description,
            DesignId = oCreateDesignCommentDTO.DesignId,
            UserEmployeeId = oCreateDesignCommentDTO.UserEmployeeId
        };
}