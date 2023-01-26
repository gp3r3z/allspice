namespace allspice.Services;
public class IngredientsService
{
    private readonly IngredientsRepository _repo;

    private readonly RecipesService _recipesService;

    public IngredientsService(IngredientsRepository repo, RecipesService recipesService)
    {
        _repo = repo;
        _recipesService = recipesService;
    }

    internal Ingredient Create(Ingredient ingredientData)
    {
        Ingredient ingredient = _repo.Create(ingredientData);
        return ingredient;
    }

    internal List<Ingredient> GetIngredientsByRecipe(int recipeId, string userId)
    {
        Recipe recipe = _recipesService.GetOne(recipeId);

        List<Ingredient> ingredients = _repo.GetIngredientsByRecipe(recipeId);
        return ingredients;
    }
    internal string Remove(int id, string userId)
    {

        // if (original.CreatorId != userId)
        // {
        //     throw new Exception("Not your Ingredient to remove !.");
        // }

        // NOTE regular delete
        _repo.Remove(id);
        return "Ingredient has been removed";
    }
}
