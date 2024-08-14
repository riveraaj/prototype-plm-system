namespace PLM.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewProductProposalController(CreateReviewProductProposalController createReviewProductProposalController,
                            GetForDesignController getForDesignController,
                            GetAllReviewProductProposalController getAllReviewProductProposalController)
    : ControllerBase
{
    private readonly CreateReviewProductProposalController _createReviewProductProposalController
                                                         = createReviewProductProposalController;
    private readonly GetForDesignController _getForDesignController
                                          = getForDesignController;
    private readonly GetAllReviewProductProposalController _getAllReviewProductProposalController
                                                        = getAllReviewProductProposalController;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] string userName = "",
                                         [FromQuery] string ideaName = "")
    {
        try
        {
            var filter = new Filter(userName, ideaName);

            var response = await _getAllReviewProductProposalController.GetAll(filter);

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
    public async Task<IActionResult> GetForDesign()
    {
        try
        {
            var response = await _getForDesignController.Get();

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
    public async Task<IActionResult> Create(CreateReviewProductProposalDTO oCreateReviewProductProposalDTO)
    {
        try
        {
            var response = await _createReviewProductProposalController.Create(oCreateReviewProductProposalDTO);

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