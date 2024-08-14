namespace PLM.BusinessLogic.Services.ReviewProductProposal;
/// <summary>
/// Service for retrieving all review product proposals with filters.
/// Implements the IGetAllInputPort interface to handle retrieval with filtering.
/// <param name="reviewProductProposalRepository">Review product proposal repository</param>
/// <param name="outputPort">Output port to handle responses</param>
/// </summary>
internal class GetAllReviewProductProposalService(IReviewProductProposalRepository reviewProductProposalRepository,
                                                  IOutputPort outputPort) : IGetAllInputPort
{
    private readonly IReviewProductProposalRepository _reviewProductProposalRepository = reviewProductProposalRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to retrieve all review product proposals with the specified filters.
    /// </summary>
    /// <param name="oFilter">Filter object containing the parameters for retrieval</param>
    public async Task GetAll(Filter oFilter)
    {
        try
        {
            var response = await _reviewProductProposalRepository.GetAllAsync(oFilter.ParamOne, oFilter.ParamTwo);

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