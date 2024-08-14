namespace PLM.BusinessLogic.Mappers;
internal static class UserEmployeeMapper
{
    public static UserEmployee MapUserEmployee(CreateUserEmployeeDTO oCreateUserEmployeeDTO)
        => new()
        {
            PersonId = oCreateUserEmployeeDTO.CreatePersonDTO.Id,
            Name = oCreateUserEmployeeDTO.CreatePersonDTO.Name,
            LastName = oCreateUserEmployeeDTO.CreatePersonDTO.LastName,
            SecondLastName = oCreateUserEmployeeDTO.CreatePersonDTO.SecondLastName,
            Address = oCreateUserEmployeeDTO.CreatePersonDTO.Address,
            Birthday = oCreateUserEmployeeDTO.CreatePersonDTO.Birthday,
            PhoneNumber = oCreateUserEmployeeDTO.CreatePersonDTO.PhoneNumber,
            Password = oCreateUserEmployeeDTO.Password,
            Email = oCreateUserEmployeeDTO.Email,
            RoleId = oCreateUserEmployeeDTO.RoleId,
        };

    public static UserEmployee MapUserEmployee(UpdateUserEmployeeDTO oUpdateUserEmployeeDTO)
        => new()
        {
            UserEmployeeId = oUpdateUserEmployeeDTO.Id,
            Address = oUpdateUserEmployeeDTO.UpdatePersonDTO.Address,
            PhoneNumber = oUpdateUserEmployeeDTO.UpdatePersonDTO.PhoneNumber,
            Email = oUpdateUserEmployeeDTO.Email,
            RoleId = oUpdateUserEmployeeDTO.RoleId
        };
}