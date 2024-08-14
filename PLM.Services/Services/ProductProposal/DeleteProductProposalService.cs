namespace PLM.BusinessLogic.Services.ProductProposal;
/// <summary>
/// Service for deleting a product proposal.
/// Implements the IDeleteInputPort interface to handle deletion operations.
/// <param name="productProposalRepository">Product proposal repository</param>
/// <param name="outputPort">Output port to handle responses</param>
/// </summary>
internal class DeleteProductProposalService(IProductProposalRepository productProposalRepository,
                                            IOutputPort outputPort) : IDeleteInputPort
{
    private readonly IProductProposalRepository _productProposalRepository = productProposalRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to delete a product proposal based on the provided ID.
    /// </summary>
    /// <param name="id">ID of the product proposal to be deleted</param>
    public async Task Delete(int id)
    {
        try
        {
            object response;

            if (id > 0) response = await _productProposalRepository.DeleteAsync(id);
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