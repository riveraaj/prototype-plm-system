namespace PLM.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductProposalController(CreateProductProposalController createProductProposalController,
                            UpdateProductProposalController updateProductProposalController,
                            DeleteProductProposalController deleteProductProposalController,
                            GetAllProductProposalController getAllProductProposalController)
    : ControllerBase
{
    private readonly CreateProductProposalController _createProductProposalController = createProductProposalController;
    private readonly UpdateProductProposalController _updateProductProposalController = updateProductProposalController;
    private readonly DeleteProductProposalController _deleteProductProposalController = deleteProductProposalController;
    private readonly GetAllProductProposalController _getAllProductProposalController = getAllProductProposalController;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] string name = "",
                                         [FromQuery] string date = "")
    {
        try
        {
            var filter = new Filter(name, date);

            var response = await _getAllProductProposalController.GetAll(filter);

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
    public async Task<IActionResult> Create(CreateProductProposalDTO oCreateProductProposalDTO)
    {
        try
        {
            var response = await _createProductProposalController.Create(oCreateProductProposalDTO);

            if (response.Code == -3) return StatusCode(StatusCodes.Status502BadGateway, response);

            if (response.Code == -1 || response.Code == -2
                || response.Code == -4) return BadRequest(response);

            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Update(UpdateProductProposalDTO oUpdateProductProposalDTO)
    {
        try
        {
            var response = await _updateProductProposalController.Update(oUpdateProductProposalDTO);

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
            var response = await _deleteProductProposalController.Delete(id);

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