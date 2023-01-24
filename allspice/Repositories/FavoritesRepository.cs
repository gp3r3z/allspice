namespace allspice.Repositories;

public class FavoritesRepository
{
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Favorite Create(Favorite favoriteData)
    {
        string sql = @"
        INSERT INTO favorites
        (recipeId, accountId)
        VALUES
        (@recipeId, @accountId);
        SELECT LAST_INSERT_ID();
        ";

        int id = _db.ExecuteScalar<int>(sql, favoriteData);
        favoriteData.Id = id;
        return favoriteData;
    }

    internal List<MyFavorites> GetMyFavorites(string accountId)
    {
        string sql = @"
    SELECT
    rec.*,
    fav.*,
    cr.*
    FROM favorites fav
    JOIN recipes rec ON rec.id = fav.recipeId
    JOIN accounts cr ON rec.creatorId = cr.id
    WHERE fav.accountId = @accountId; 
    ";
        List<MyFavorites> myFavorites = _db.Query<MyFavorites, Favorite, Account, MyFavorites>(sql, (rec, fav, cr) =>
        {
            rec.RecipeId = fav.Id;
            rec.AccountId = cr.Id;
            rec.FavoriteId = fav.Id;
            return rec;
        }, new { accountId }).ToList();

        return myFavorites;
    }


    internal bool Remove(int id)
    {
        string sql = @"
    DELETE FROM favorites
    WHERE id = @id;
    ";
        int rows = _db.Execute(sql, new { id });
        // NOTE returns to the service a bool if it deleted anything from the table or not
        return rows > 0;
    }
}
