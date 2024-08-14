namespace PLM.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryIdeaController(GetAllCategoryIdeaController categoryIdeaController)
    : ControllerBase
{
    private readonly GetAllCategoryIdeaController _categoryIdeaController = categoryIdeaController;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        try
        {
            var response = await _categoryIdeaController.GetAll(new Filter());

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