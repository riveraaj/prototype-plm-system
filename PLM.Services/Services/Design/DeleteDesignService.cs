namespace PLM.BusinessLogic.Services.Design;
/// <summary>
/// Service for deleting a design based on the provided design ID.
/// Implements the IDeleteInputPort interface to handle delete operations.
/// <param name="designRepository">Repository for design data operations</param>
/// <param name="outputPort">Output port to manage responses</param>
/// </summary>
internal class DeleteDesignService(IDesignRepository designRepository,
                                 IOutputPort outputPort) : IDeleteInputPort
{
    private readonly IDesignRepository _designRepository = designRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to delete a design based on the provided design ID.
    /// </summary>
    /// <param name="id">ID of the design to be deleted</param>
    public async Task Delete(int id)
    {
        try
        {
            object response;

            if (id > 0) response = await _designRepository.DeleteAsync(id);
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