namespace allspice.Repositories;
public class IngredientsRepository
{
    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Ingredient Create(Ingredient ingredientData)
    {
        string sql = @"
    INSERT INTO ingredients
    (name, quantity, recipeId)
    VALUES
    (@name, @quantity, @recipeId);
    SELECT LAST_INSERT_ID();
    ";
        int id = _db.ExecuteScalar<int>(sql, ingredientData);
        ingredientData.Id = id;
        return ingredientData;
    }

    internal List<Ingredient> GetIngredientsByRecipe(int recipeId)
    {
        string sql = @"
    SELECT
    ingred.*
    FROM ingredients ingred
    WHERE recipeId = @recipeId;
    ";
        List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
        return ingredients;
    }

    internal bool Remove(int id)
    {
        string sql = @"
    DELETE FROM ingredients
    WHERE id = @id;
    ";
        int rows = _db.Execute(sql, new { id });
        // NOTE returns to the service a bool if it deleted anything from the table or not
        return rows > 0;
    }
}
