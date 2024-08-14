namespace PLM.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewDesignController(CreateReviewDesignController createReviewDesignController,
                            GetAllReviewDesignController getAllReviewDesignController)
    : ControllerBase
{
    private readonly CreateReviewDesignController _createReviewDesignController
                                                         = createReviewDesignController;
    private readonly GetAllReviewDesignController _getAllReviewDesignController
                                                        = getAllReviewDesignController;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] string userName = "",
                                         [FromQuery] string designName = "")
    {
        try
        {
            var filter = new Filter(userName, designName);

            var response = await _getAllReviewDesignController.GetAll(filter);

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
    public async Task<IActionResult> Create(CreateReviewDesignDTO oCreateReviewDesignDTO)
    {
        try
        {
            var response = await _createReviewDesignController.Create(oCreateReviewDesignDTO);

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
