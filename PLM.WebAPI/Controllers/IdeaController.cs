namespace PLM.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdeaController(CreateIdeaController createIdeaController,
                            UpdateIdeaController updateIdeaController,
                            GetForProductProposalController getForProductProposalController,
                            DeleteIdeaController deleteIdeaController,
                            GetAllIdeaController getAllIdeaController)
    : ControllerBase
{
    private readonly CreateIdeaController _createIdeaController = createIdeaController;
    private readonly UpdateIdeaController _updateIdeaController = updateIdeaController;
    private readonly GetForProductProposalController _getForProductProposalController
                                                     = getForProductProposalController;
    private readonly DeleteIdeaController _deleteIdeaController = deleteIdeaController;
    private readonly GetAllIdeaController _getAllIdeaController = getAllIdeaController;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] string name = "",
                                         [FromQuery] string date = "")
    {
        try
        {
            var filter = new Filter(name, date);

            var response = await _getAllIdeaController.GetAll(filter);

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
    public async Task<IActionResult> GetForProductProposal()
    {
        try
        {
            var response = await _getForProductProposalController.Get();

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
    public async Task<IActionResult> Create(CreateIdeaDTO oCreateIdeaDTO)
    {
        try
        {
            var response = await _createIdeaController.Create(oCreateIdeaDTO);

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
    public async Task<IActionResult> Update(UpdateIdeaDTO oUpdateIdeaDTO)
    {
        try
        {
            var response = await _updateIdeaController.Update(oUpdateIdeaDTO);

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
            var response = await _deleteIdeaController.Delete(id);

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