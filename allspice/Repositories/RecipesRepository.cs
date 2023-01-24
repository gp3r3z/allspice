namespace allspice.Repositories;

public class RecipesRepository
{
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Recipe Create(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO recipes
        (title, instructions, img, category, creatorId)
        VALUES
        (@title, @instructions, @img, @category, @creatorId);
        SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, recipeData);
        recipeData.Id = id;
        return recipeData;
    }
    internal List<Recipe> Get()
    {
        string sql = @"
    SELECT
    rec.*,
    ac.*
    FROM recipes rec
    JOIN accounts ac ON ac.id = rec.creatorId;
    ";
        //                      <1st select, 2nd select, return type> |(data from rows)
        List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
        {
            // attach account data from the matching row to album
            recipe.Creator = account;
            // return joined data
            return recipe;
        }).ToList();

        return recipes;
    }


    internal Recipe GetOne(int id)
    {
        string sql = @"
        SELECT
        rec.*,
        ac.*
        FROM recipes rec
        JOIN accounts ac ON ac.id = rec.creatorId
        WHERE rec.id = @id;
        ";

        return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
        {
            recipe.Creator = account;
            return recipe;
        }, new { id }).FirstOrDefault();
    }

    internal bool Update(Recipe original)
    {
        string sql = @"
    UPDATE recipes
        SET
       
        instructions = @instructions
    WHERE id = @id;
    ";
        int rows = _db.Execute(sql, original);
        return rows > 0;
    }

    internal bool Remove(int id)
    {
        string sql = @"
    DELETE FROM recipes
    WHERE id = @id;
    ";
        int rows = _db.Execute(sql, new { id });
        // NOTE returns to the service a bool if it deleted anything from the table or not
        return rows > 0;
    }
}