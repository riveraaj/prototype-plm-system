namespace PLM.BusinessLogic.Services.ProductProposal;
/// <summary>
/// Service for retrieving all product proposals.
/// Implements the IGetAllInputPort interface to handle retrieval with filter parameters.
/// <param name="productProposalRepository">Product proposal repository</param>
/// <param name="outputPort">Output port to handle responses</param>
/// </summary>
internal class GetAllProductProposalService(IProductProposalRepository productProposalRepository,
                               IOutputPort outputPort) : IGetAllInputPort
{
    private readonly IProductProposalRepository _productProposalRepository = productProposalRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to retrieve all product proposals based on the provided filter parameters.
    /// </summary>
    /// <param name="oFilter">Filter parameters for retrieving product proposals</param>
    public async Task GetAll(Filter oFilter)
    {
        try
        {
            var response = await _productProposalRepository.GetAllAsync(oFilter.ParamOne, oFilter.ParamTwo);
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