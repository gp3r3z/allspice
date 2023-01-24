namespace allspice.Models;

public class Favorite
{
    public int Id { get; set; }
    public string AccountId { get; set; }
    public int RecipeId { get; set; }
}
public class MyFavorites : Favorite
{
    public int FavoriteId { get; set; }
}