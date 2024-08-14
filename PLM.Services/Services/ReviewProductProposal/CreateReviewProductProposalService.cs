namespace PLM.BusinessLogic.Services.ReviewProductProposal;
/// <summary>
/// Service for creating a new review product proposal.
/// Implements the ICreateInputPort interface to handle creation with DTO.
/// <param name="reviewProductProposalRepository">Review product proposal repository</param>
/// <param name="outputPort">Output port to handle responses</param>
/// </summary>
internal class CreateReviewProductProposalService(IReviewProductProposalRepository reviewProductProposalRepository,
                                                  IOutputPort outputPort) : ICreateInputPort<CreateReviewProductProposalDTO>
{
    private readonly IReviewProductProposalRepository _reviewProductProposalRepository
        = reviewProductProposalRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to create a new review product proposal with the provided DTO.
    /// </summary>
    /// <param name="oCreatereviewProductProposalDTO">DTO with review product proposal creation data</param>
    public async Task Create(CreateReviewProductProposalDTO oCreatereviewProductProposalDTO)
    {
        try
        {
            // Validate the DTO model
            var validationResult = oCreatereviewProductProposalDTO.ValidateModel();
            object response;

            // If validation is successful, proceed to map and create the review product proposal
            if (validationResult.IsValid)
            {
                var reviewProductProposal = ReviewProductProposalMapper.MapReviewProductProposal(oCreatereviewProductProposalDTO);
                response = await _reviewProductProposalRepository.CreateAsync(reviewProductProposal, oCreatereviewProductProposalDTO.Status);
            }
            else
                // If there are validation errors, prepare an error response
                response = new OperationResponse()
                {
                    Code = -4,
                    Message = "Errores de validación en los datos enviados.",
                    Content = validationResult.ErrorMessages.Select(msg => (object)msg!).ToList()
                };

            // Handle the response using the output port
            await _outputPort.Handle((OperationResponse)response);
        }
        catch (Exception ex)
        {
            // Handle exceptions and prepare an error response
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