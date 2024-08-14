namespace PLM.BusinessLogic.Services.UserEmployee;
/// <summary>
/// Service for updating user employee information.
/// Implements the IUpdateInputPort interface to handle update input.
/// <param name="userEmployeeRepository">User employee repository</param>
/// <param name="outputPort">Output port to handle responses</param>
/// </summary>
internal class UpdateUserEmployeeService(IUserEmployeeRepository userEmployeeRepository,
                                         IOutputPort outputPort)
    : IUpdateInputPort<UpdateUserEmployeeDTO>
{
    private readonly IUserEmployeeRepository _userEmployeeRepository = userEmployeeRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to update user employee information.
    /// </summary>
    /// <param name="oUpdateUserEmployeeDTO">DTO with update data</param>
    public async Task Update(UpdateUserEmployeeDTO oUpdateUserEmployeeDTO)
    {
        try
        {
            // Validate the primary and secondary data models
            var validationResult = oUpdateUserEmployeeDTO.ValidateModel();
            var secondValidationResult = oUpdateUserEmployeeDTO.UpdatePersonDTO.ValidateModel();

            object response;

            if (validationResult.IsValid && secondValidationResult.IsValid)
            {
                var userEmployee = UserEmployeeMapper.MapUserEmployee(oUpdateUserEmployeeDTO);
                response = await _userEmployeeRepository.UpdateAsync(userEmployee);
            }
            else
                response = new OperationResponse()
                {
                    Code = -4,
                    Message = "Errores de validación en los datos enviados.",
                    Content = validationResult.ErrorMessages.Select(msg => (object)msg!)
                                .Concat(secondValidationResult.ErrorMessages.Select(msg => (object)msg!))
                                .ToList()
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