namespace PLM.BusinessLogic.Services.ReviewDesign;
/// <summary>
/// Service for creating a new review design.
/// Implements the ICreateInputPort interface to handle creation with DTO.
/// <param name="reviewDesignRepository">Review design repository</param>
/// <param name="outputPort">Output port to handle responses</param>
/// </summary>
internal class CreateReviewDesignService(IReviewDesignRepository reviewDesignRepository,
                                 IOutputPort outputPort) : ICreateInputPort<CreateReviewDesignDTO>
{
    private readonly IReviewDesignRepository _reviewDesignRepository = reviewDesignRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to create a new review design with the provided DTO.
    /// </summary>
    /// <param name="oCreatereviewDesignDTO">DTO with review design creation data</param>
    public async Task Create(CreateReviewDesignDTO oCreatereviewDesignDTO)
    {
        try
        {
            // Validate the DTO model
            var validationResult = oCreatereviewDesignDTO.ValidateModel();
            object response;

            if (validationResult.IsValid)
            {
                var reviewDesign = ReviewDesignMapper.MapReviewDesign(oCreatereviewDesignDTO);
                response = await _reviewDesignRepository.CreateAsync(reviewDesign, oCreatereviewDesignDTO.Status);
            }
            else
                response = new OperationResponse()
                {
                    Code = -4,
                    Message = "Errores de validación en los datos enviados.",
                    Content = validationResult.ErrorMessages.Select(msg => (object)msg!).ToList()
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