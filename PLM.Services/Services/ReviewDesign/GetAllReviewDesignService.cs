namespace PLM.BusinessLogic.Services.ReviewDesign;
/// <summary>
/// Service for retrieving all review designs with filtering.
/// Implements the IGetAllInputPort interface to handle review design retrieval with filters.
/// <param name="reviewDesignRepository">Review design repository</param>
/// <param name="outputPort">Output port to handle responses</param>
/// </summary>
internal class GetAllReviewDesignService(IReviewDesignRepository reviewDesignRepository,
                               IOutputPort outputPort) : IGetAllInputPort
{
    private readonly IReviewDesignRepository _reviewDesignRepository = reviewDesignRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to retrieve all review designs based on the provided filters.
    /// </summary>
    /// <param name="oFilter">Filter object containing parameters for retrieval</param>
    public async Task GetAll(Filter oFilter)
    {
        try
        {
            var response = await _reviewDesignRepository.GetAllAsync(oFilter.ParamOne, oFilter.ParamTwo);
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