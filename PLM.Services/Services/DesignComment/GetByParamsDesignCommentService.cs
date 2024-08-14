namespace PLM.BusinessLogic.Services.DesignComment;
/// <summary>
/// Service for retrieving design comments based on provided parameters.
/// Implements the IGetByParamsInputPort interface to handle retrieval operations.
/// <param name="designcommentRepository">Repository for design comment data operations</param>
/// <param name="outputPort">Output port to manage responses</param>
/// </summary>
internal class GetByParamsDesignCommentService(IDesignCommentRepository designcommentRepository,
                                      IOutputPort outputPort) : IGetByParamsInputPort<uint>
{
    private readonly IDesignCommentRepository _designcommentRepository = designcommentRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to retrieve design comments based on the provided design ID.
    /// </summary>
    /// <param name="id">The ID of the design for which comments are to be retrieved</param>
    public async Task GetByParams(uint id)
    {
        try
        {
            var response = await _designcommentRepository.GetByDesignIdAsync((int)id);
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