namespace PLM.BusinessLogic.Services.UserEmployee;
/// <summary>
/// Service for creating a new user employee.
/// Implements the ICreateInputPort interface to handle creation with DTO.
/// <param name="userEmployeeRepository">User employee repository</param>
/// <param name="outputPort">Output port to handle responses</param>
/// </summary>
internal class CreateUserEmployeeService(IUserEmployeeRepository userEmployeeRepository,
                                         IOutputPort outputPort)
    : ICreateInputPort<CreateUserEmployeeDTO>
{
    private readonly IUserEmployeeRepository _userEmployeeRepository = userEmployeeRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to create a new user employee with the provided DTO.
    /// </summary>
    /// <param name="oCreateUserEmployeeDTO">DTO with user employee creation data</param>
    public async Task Create(CreateUserEmployeeDTO oCreateUserEmployeeDTO)
    {
        try
        {
            // Validate the primary and secondary data models
            var validationResult = oCreateUserEmployeeDTO.ValidateModel();
            var secondValidationResult = oCreateUserEmployeeDTO.CreatePersonDTO.ValidateModel();

            object response;

            if (validationResult.IsValid && secondValidationResult.IsValid)
            {
                // Encrypt the password before storing
                oCreateUserEmployeeDTO.Password = EncryptorHelper.Encrypt(oCreateUserEmployeeDTO.Password);

                var user = UserEmployeeMapper.MapUserEmployee(oCreateUserEmployeeDTO);
                response = await _userEmployeeRepository.CreateAsync(user);
            }
            else
            {
                response = new OperationResponse()
                {
                    Code = -4,
                    Message = "Errores de validación en los datos enviados.",
                    Content = validationResult.ErrorMessages.Select(msg => (object)msg!)
                                .Concat(secondValidationResult.ErrorMessages.Select(msg => (object)msg!))
                                .ToList()
                };
            }

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