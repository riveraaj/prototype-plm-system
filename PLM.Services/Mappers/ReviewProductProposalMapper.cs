namespace PLM.BusinessLogic.Mappers;
internal static class ReviewProductProposalMapper
{
    public static ReviewProductProposal MapReviewProductProposal(
        CreateReviewProductProposalDTO oCreateReviewProductProposalDTO)
        => new()
        {
            Justification = oCreateReviewProductProposalDTO.Justification,
            ProductProposalId = oCreateReviewProductProposalDTO.ProductProposalId,
            UserEmployeeId = oCreateReviewProductProposalDTO.UserEmployeeId,
        };
}