namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RecipesController : ControllerBase
{
    private readonly RecipesService _recipesService;
    private readonly Auth0Provider _auth0provider;

    private readonly IngredientsService _ingredientsService;


    public RecipesController(RecipesService recipesService, Auth0Provider auth0provider, IngredientsService ingredientService)
    {
        _recipesService = recipesService;
        _auth0provider = auth0provider;
        _ingredientsService = ingredientService;

    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Recipe>> Create([FromBody] Recipe recipeData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            recipeData.CreatorId = userInfo.Id;

            Recipe recipe = _recipesService.Create(recipeData);
            recipe.Creator = userInfo;

            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<Recipe>>> Get()
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            List<Recipe> recipe = _recipesService.Get(userInfo?.Id);
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Recipe>> GetOne(int id)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);

            Recipe recipe = _recipesService.GetOne(id);
            return Ok(recipe);


        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<Recipe> Update([FromBody] Recipe recipeData, int id)
    {
        try
        {
            Recipe recipe = _recipesService.Update(recipeData, id);
            return Ok(recipe);

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
            string message = _recipesService.Remove(id, userInfo.Id);
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("{id}/ingredients")]
    public async Task<ActionResult<List<Ingredient>>> GetIngredientsByRecipe(int id)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            List<Ingredient> pictures = _ingredientsService.GetIngredientsByRecipe(id, userInfo?.Id);
            return Ok(pictures);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}