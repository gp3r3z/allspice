namespace allspice.Services;

public class RecipesService
{
    private readonly RecipesRepository _repo;

    public RecipesService(RecipesRepository repo)
    {
        _repo = repo;
    }

    internal Recipe Create(Recipe recipeData)
    {
        Recipe recipe = _repo.Create(recipeData);

        return recipe;
    }

    internal List<Recipe> Get(string userId)
    {
        List<Recipe> recipes = _repo.Get();
        // TODO add filter option
        List<Recipe> filtered = recipes.FindAll(r => r.CreatorId == userId);

        return recipes;
    }

    internal Recipe GetOne(int id)
    {
        Recipe recipe = _repo.GetOne(id);
        if (recipe == null)
        {
            throw new Exception($"No Recipe Found at ID: {id}");
        }
        return recipe;
    }

    internal Recipe Update(Recipe recipeUpdate, int id)
    {
        // NOTE null coalescence
        Recipe original = GetOne(id);

        original.Instructions = recipeUpdate.Instructions ?? original.Instructions;


        bool edited = _repo.Update(original);
        if (edited == false)
        {
            throw new Exception("Car was not edited");
        }
        return original;
    }

    internal string Remove(int id, string userId)
    {
        Recipe original = GetOne(id);
        if (original.CreatorId != userId)
        {
            throw new Exception("Not your Recipe to remove !.");
        }

        // NOTE regular delete
        _repo.Remove(id);
        return $"{original.Title} has been exterminated";
    }
}