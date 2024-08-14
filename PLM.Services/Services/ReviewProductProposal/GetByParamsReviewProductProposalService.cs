namespace PLM.BusinessLogic.Services.ReviewProductProposal;
/// <summary>
/// Service for retrieving review product proposals based on specific parameters.
/// Implements the IGetByParamsInputPort interface to handle retrieval with a boolean parameter.
/// <param name="reviewProductProposalRepository">Review product proposal repository</param>
/// <param name="outputPort">Output port to handle responses</param>
/// </summary>
internal class GetByParamsReviewProductProposalService(IReviewProductProposalRepository reviewProductProposalRepository,
                                      IOutputPort outputPort) : IGetByParamsInputPort<bool>
{
    private readonly IReviewProductProposalRepository _reviewProductProposalRepository = reviewProductProposalRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to retrieve review product proposals based on the specified boolean parameter.
    /// </summary>
    /// <param name="param">Boolean parameter used for retrieval</param>
    public async Task GetByParams(bool param)
    {
        try
        {
            var response = await _reviewProductProposalRepository.GetAllForDesign();
            await _outputPort.Handle((OperationResponse)response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            await _outputPort.Handle(new OperationResponse
            {
                Code = -3,
                Message = "Se ha producido un problema, por favor inténtelo de nuevo. " +
                          "Si el problema persiste, póngase en contacto con el servicio técnico",
                Content = []
            });
        }
    }
}