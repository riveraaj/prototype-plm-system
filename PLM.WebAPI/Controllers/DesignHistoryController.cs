namespace PLM.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DesignHistoryController(DeleteDesignHistoryController deleteDesignHistoryController,
                                     GetAllDesignHistoryController getAllDesignHistoryController)
    : ControllerBase
{
    private readonly DeleteDesignHistoryController _deleteDesignHistoryController
                                                    = deleteDesignHistoryController;
    private readonly GetAllDesignHistoryController _getAllDesignHistoryController
                                                    = getAllDesignHistoryController;

    [HttpGet]
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] string name = "",
                                         [FromQuery] string date = "",
                                         [FromQuery] string designId = "")
    {
        try
        {
            var filter = new Filter(name, date, designId);

            var response = await _getAllDesignHistoryController.GetAll(filter);

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
            var response = await _deleteDesignHistoryController.Delete(id);

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