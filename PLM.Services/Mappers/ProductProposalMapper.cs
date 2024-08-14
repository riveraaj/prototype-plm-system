namespace PLM.BusinessLogic.Mappers;
internal static class ProductProposalMapper
{
    public static ProductProposal MapProductProposal(CreateProductProposalDTO oCreateProductProposalDTO)
        => new()
        {
            FilePath = oCreateProductProposalDTO.FilePath,
            IdeaId = oCreateProductProposalDTO.IdeaId,
            UserEmployeeId = oCreateProductProposalDTO.UserEmployeeId
        };

    public static ProductProposal MapProductProposal(UpdateProductProposalDTO oUpdateProductProposalDTO)
        => new()
        {
            FilePath = oUpdateProductProposalDTO.FilePath,
            ProductProposalId = oUpdateProductProposalDTO.Id,
            UserEmployeeId = oUpdateProductProposalDTO.UserEmployeeId
        };
}