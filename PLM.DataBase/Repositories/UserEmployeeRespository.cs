namespace PLM.DataBase.Repositories;
internal class UserEmployeeRespository(IConnection context)
    : InvokeStoredProcedure(context), IUserEmployeeRepository
{
    public async Task<OperationResponse> CreateAsync(UserEmployee oUserEmployee)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@person_id", oUserEmployee.PersonId}, {"@name", oUserEmployee.Name},
                {"@last_name", oUserEmployee.LastName}, {"@second_lastname", oUserEmployee.SecondLastName},
                {"@address ", oUserEmployee.Address}, {"@birthday", oUserEmployee.Birthday},
                {"@phone_number", oUserEmployee.PhoneNumber}, {"@password", oUserEmployee.Password},
                {"@email", oUserEmployee.Email}, {"@role_id", oUserEmployee.RoleId}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("create_user_employee", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }

    public async Task<OperationResponse> DeleteAsync(int id)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@user_employee_id", id}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("delete_user_employee", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }

    public async Task<OperationResponse> GetAllAsync(string name, string id)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@personName", name},
                {"@identification", id}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("get_user_employee", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }

    public async Task<OperationResponse> GetByIdAsync(int id)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@user_employee_id", id}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("get_by_id_user_employee", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }

    public async Task<OperationResponse> UpdateAsync(UserEmployee oUserEmployee)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@user_employee_id", oUserEmployee.UserEmployeeId},
                {"@address", oUserEmployee.Address},
                {"@phone_number", oUserEmployee.PhoneNumber},
                {"@email", oUserEmployee.Email},
                {"@role_id", oUserEmployee.RoleId}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("update_user_employee", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }
}