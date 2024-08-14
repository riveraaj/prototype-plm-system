namespace PLM.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserEmployeeController(CreateUserEmployeeController createUserEmployeeController,
                                    UpdateUserEmployeeController updateUserEmployeeController,
                                    GetByIdUserEmployeeController getByIdUserEmployeeController,
                                    DeleteUserEmployeeController deleteUserEmployeeController,
                                    GetAllUserEmployeeController getAllUserEmployeeController)
    : ControllerBase
{
    private readonly CreateUserEmployeeController _createUserEmployeeController
                                                = createUserEmployeeController;
    private readonly UpdateUserEmployeeController _updateUserEmployeeController
                                                = updateUserEmployeeController;
    private readonly GetByIdUserEmployeeController _getForProductProposalController
                                                    = getByIdUserEmployeeController;
    private readonly DeleteUserEmployeeController _deleteUserEmployeeController
                                                = deleteUserEmployeeController;
    private readonly GetAllUserEmployeeController _getAllUserEmployeeController
                                                = getAllUserEmployeeController;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] string name = "",
                                         [FromQuery] string identification = "")
    {
        try
        {
            var filter = new Filter(name, identification);

            var response = await _getAllUserEmployeeController.GetAll(filter);

            if (response.Code == -3) return StatusCode(StatusCodes.Status502BadGateway, response);

            if (response.Code == -1 || response.Code == -2
                || response.Code == -4) return BadRequest(response);

            return Ok(response);
        }
        catch (JsonException ex)
        {
            Console.WriteLine(ex);
            return BadRequest(ex);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex);
            return BadRequest(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }

    [HttpGet]
    [Route("get")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        try
        {
            var response = await _getForProductProposalController.Get(id);

            if (response.Code == -3) return StatusCode(StatusCodes.Status502BadGateway, response);

            if (response.Code == -1 || response.Code == -2
                || response.Code == -4) return BadRequest(response);

            return Ok(response);
        }
        catch (JsonException ex)
        {
            Console.WriteLine(ex);
            return BadRequest(ex);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex);
            return BadRequest(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create(CreateUserEmployeeDTO oCreateUserEmployeeDTO)
    {
        try
        {
            var response = await _createUserEmployeeController.Create(oCreateUserEmployeeDTO);

            if (response.Code == -3) return StatusCode(StatusCodes.Status502BadGateway, response);

            if (response.Code == -1 || response.Code == -2
                || response.Code == -4) return BadRequest(response);

            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (JsonException ex)
        {
            Console.WriteLine(ex);
            return BadRequest(ex);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex);
            return BadRequest(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Update(UpdateUserEmployeeDTO oUpdateUserEmployeeDTO)
    {
        try
        {
            var response = await _updateUserEmployeeController.Update(oUpdateUserEmployeeDTO);

            if (response.Code == -3) return StatusCode(StatusCodes.Status502BadGateway, response);

            if (response.Code == -1 || response.Code == -2
                || response.Code == -4) return BadRequest(response);

            return Ok(response);
        }
        catch (JsonException ex)
        {
            Console.WriteLine(ex);
            return BadRequest(ex);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex);
            return BadRequest(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var response = await _deleteUserEmployeeController.Delete(id);

            if (response.Code == -3) return StatusCode(StatusCodes.Status502BadGateway, response);

            if (response.Code == -1 || response.Code == -2
                || response.Code == -4) return BadRequest(response);

            return Ok(response);
        }
        catch (JsonException ex)
        {
            Console.WriteLine(ex);
            return BadRequest(ex);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex);
            return BadRequest(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }
}