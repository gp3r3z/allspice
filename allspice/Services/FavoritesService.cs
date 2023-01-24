namespace allspice.Services;

public class FavoritesService
{
    private readonly FavoritesRepository _repo;

    private readonly RecipesService _recipesService;


    public FavoritesService(FavoritesRepository repo, RecipesService recipesService)
    {
        _repo = repo;
        _recipesService = recipesService;

    }

    internal Favorite Create(Favorite favoriteData)
    {
        Recipe recipe = _recipesService.GetOne(favoriteData.RecipeId);

        Favorite favorite = _repo.Create(favoriteData);

        return favorite;
    }

    internal List<MyFavorites> GetMyFavorites(string accountId)
    {
        List<MyFavorites> myFavorites = _repo.GetMyFavorites(accountId);
        return myFavorites;
    }

    internal string Remove(int id, string userId)
    {

        // NOTE regular delete
        _repo.Remove(id);
        return $"{id} has been removed";
    }
}
