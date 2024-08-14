namespace PLM.BusinessLogic.Mappers;
internal static class ReviewDesignMapper
{
    public static ReviewDesign MapReviewDesign(CreateReviewDesignDTO oCreateReviewDesignDTO)
        => new()
        {
            DesignId = oCreateReviewDesignDTO.DesignId,
            Justification = oCreateReviewDesignDTO.Justification,
            UserEmployeeId = oCreateReviewDesignDTO.UserEmployeeId
        };
}