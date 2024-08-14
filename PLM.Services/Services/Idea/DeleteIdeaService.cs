namespace PLM.BusinessLogic.Services.Idea;
/// <summary>
/// Service for deleting an idea.
/// Implements the IDeleteInputPort interface to handle delete operations.
/// <param name="ideaRepository">Repository for idea data operations</param>
/// <param name="outputPort">Output port to manage responses</param>
/// </summary>
internal class DeleteIdeaService(IIdeaRepository ideaRepository,
                                 IOutputPort outputPort) : IDeleteInputPort
{
    private readonly IIdeaRepository _ideaRepository = ideaRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to delete an idea based on the provided ID.
    /// </summary>
    /// <param name="id">ID of the idea to be deleted</param>
    public async Task Delete(int id)
    {
        try
        {
            object response;

            if (id > 0) response = await _ideaRepository.DeleteAsync(id);
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