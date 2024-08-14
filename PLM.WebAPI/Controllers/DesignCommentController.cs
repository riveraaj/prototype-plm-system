namespace PLM.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DesignCommentController(CreateDesignCommentController createDesignCommentController,
                            GetByDesignIdDesingCommentController getByDesignIdDesingCommentController)
    : ControllerBase
{
    private readonly CreateDesignCommentController _createDesignCommentController
                                                    = createDesignCommentController;
    private readonly GetByDesignIdDesingCommentController _getByDesignIdDesingCommentController
                                                        = getByDesignIdDesingCommentController;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetByDesignId([FromQuery] int id)
    {
        try
        {
            var response = await _getByDesignIdDesingCommentController.GetByDesignId(id);

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
    public async Task<IActionResult> Create(CreateDesignCommentDTO oCreateDesignCommentDTO)
    {
        try
        {
            var response = await _createDesignCommentController.Create(oCreateDesignCommentDTO);

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
}