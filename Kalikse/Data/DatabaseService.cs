using System.Data;
using Microsoft.Data.Sqlite;
using Kalikse.Models;

namespace Kalikse.Data;

public class DatabaseService
{
    private readonly string _dbPath;

    public DatabaseService()
    {
        _dbPath = Path.Combine(FileSystem.AppDataDirectory, "recipes.db");
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        if (!File.Exists(_dbPath))
        {
            using var connection = new SqliteConnection($"Data Source={_dbPath}");
            connection.Open();

            // Execute the SQL script to create tables
            var sql = File.ReadAllText("Data/RecipeDatabase.sql");
            using var command = new SqliteCommand(sql, connection);
            command.ExecuteNonQuery();
        }
    }

    public async Task<List<Recipe>> GetFilteredRecipesAsync(decimal budget, string dietaryPreference, List<string> allergies)
    {
        var recipes = new List<Recipe>();

        using var connection = new SqliteConnection($"Data Source={_dbPath}");
        await connection.OpenAsync();

        var query = @"
            SELECT DISTINCT r.* 
            FROM Recipes r
            LEFT JOIN RecipeDietaryPreferences rdp ON r.RecipeId = rdp.RecipeId
            LEFT JOIN DietaryPreferences dp ON rdp.PreferenceId = dp.PreferenceId
            WHERE r.PriceRangeMax <= @Budget
            AND (dp.Name = @DietaryPreference OR @DietaryPreference = 'None')
            AND NOT EXISTS (
                SELECT 1 FROM RecipeIngredients ri
                JOIN Ingredients i ON ri.IngredientId = i.IngredientId
                WHERE ri.RecipeId = r.RecipeId
                AND i.IsAllergen = 1
                AND i.Name IN @Allergies
            )";

        using var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@Budget", budget);
        command.Parameters.AddWithValue("@DietaryPreference", dietaryPreference);
        command.Parameters.AddWithValue("@Allergies", string.Join(",", allergies));

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var recipe = new Recipe
            {
                RecipeId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                ImagePath = reader.GetString(3),
                PriceRangeMin = reader.GetDecimal(4),
                PriceRangeMax = reader.GetDecimal(5)
            };

            // Load ingredients
            recipe.Ingredients = await GetRecipeIngredientsAsync(recipe.RecipeId);

            // Load dietary preferences
            recipe.DietaryPreferences = await GetRecipeDietaryPreferencesAsync(recipe.RecipeId);

            recipes.Add(recipe);
        }

        return recipes;
    }

    private async Task<List<RecipeIngredient>> GetRecipeIngredientsAsync(int recipeId)
    {
        var ingredients = new List<RecipeIngredient>();

        using var connection = new SqliteConnection($"Data Source={_dbPath}");
        await connection.OpenAsync();

        var query = @"
            SELECT i.Name, ri.Quantity, ri.Unit
            FROM RecipeIngredients ri
            JOIN Ingredients i ON ri.IngredientId = i.IngredientId
            WHERE ri.RecipeId = @RecipeId";

        using var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@RecipeId", recipeId);

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            ingredients.Add(new RecipeIngredient
            {
                Name = reader.GetString(0),
                Quantity = reader.GetString(1),
                Unit = reader.GetString(2)
            });
        }

        return ingredients;
    }

    private async Task<List<string>> GetRecipeDietaryPreferencesAsync(int recipeId)
    {
        var preferences = new List<string>();

        using var connection = new SqliteConnection($"Data Source={_dbPath}");
        await connection.OpenAsync();

        var query = @"
            SELECT dp.Name
            FROM RecipeDietaryPreferences rdp
            JOIN DietaryPreferences dp ON rdp.PreferenceId = dp.PreferenceId
            WHERE rdp.RecipeId = @RecipeId";

        using var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@RecipeId", recipeId);

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            preferences.Add(reader.GetString(0));
        }

        return preferences;
    }
}