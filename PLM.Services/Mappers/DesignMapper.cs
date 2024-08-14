namespace PLM.BusinessLogic.Mappers;
internal static class DesignMapper
{
    public static Design MapDesign(CreateDesignDTO oCreateDesignDTO)
        => new()
        {
            CurrentDesignPath = oCreateDesignDTO.CurrentDesignPath,
            ReviewProductProposalId = oCreateDesignDTO.ReviewProductProposalId,
            UserEmployeeId = oCreateDesignDTO.UserEmployeeId,
            Name = oCreateDesignDTO.Name,
        };

    public static Design MapDesign(UpdateDesignDTO oUpdateDesignDTO)
        => new()
        {
            CurrentDesignPath = oUpdateDesignDTO.CurrentDesignPath,
            DesignId = oUpdateDesignDTO.Id,
            UserEmployeeId = oUpdateDesignDTO.UserEmployeeId,
        };
}