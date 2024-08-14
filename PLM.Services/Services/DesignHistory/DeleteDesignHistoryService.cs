namespace PLM.BusinessLogic.Services.DesignHistory;
/// <summary>
/// Service for deleting a design history record.
/// Implements the IDeleteInputPort interface to handle deletion operations.
/// <param name="designHistoryRepository">Repository for design history data operations</param>
/// <param name="outputPort">Output port to manage responses</param>
/// </summary>
internal class DeleteDesignHistoryService(IDesignHistoryRepository designHistoryRepository,
                                 IOutputPort outputPort) : IDeleteInputPort
{
    private readonly IDesignHistoryRepository _designHistoryRepository = designHistoryRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to delete a design history record based on the provided ID.
    /// </summary>
    /// <param name="id">ID of the design history record to be deleted</param>
    public async Task Delete(int id)
    {
        try
        {
            object response;

            if (id > 0) response = await _designHistoryRepository.DeleteAsync(id);
            else response = new OperationResponse
            {
                Code = -4,
                Message = "Errores de validación en los datos enviados.",
                Content = ["El id es un campo obligatorio"]
            };

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