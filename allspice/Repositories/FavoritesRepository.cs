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
   
    fav.*,
    rec.*,
    cr.*
    FROM recipes rec
    JOIN favorites fav ON fav.recipeId = rec.id
    JOIN accounts cr ON rec.creatorId = cr.id
    WHERE fav.accountId = @accountId; 
    ";
        List<MyFavorites> myFavorites = _db.Query<MyFavorites, Favorite, Account, MyFavorites>(sql, (fav, rec, cr) =>
        {

            fav.FavoriteId = fav.Id;
            return fav;
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
