namespace allspice.Controllers;



[ApiController]
[Route("api/ingredients")]
public class IngredientsController : ControllerBase
{
    private IngredientsService _ingredientsService;

    private readonly Auth0Provider _auth0provider;

    public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider)
    {
        _ingredientsService = ingredientsService;
        _auth0provider = auth0Provider;
    }


    [HttpPost]
    [Authorize]
    public ActionResult<Ingredient> Create([FromBody] Ingredient ingredientData)
    {
        try
        {

            Ingredient ingredient = _ingredientsService.Create(ingredientData);

            return Ok(ingredient);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Remove(int id)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _ingredientsService.Remove(id, userInfo.Id);
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}
