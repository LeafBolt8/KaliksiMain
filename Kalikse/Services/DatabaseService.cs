using Microsoft.Data.Sqlite;
using Kalikse.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace Kalikse.Services
{
    public class DatabaseService
    {
        private readonly string _dbPath;
        private readonly string _connectionString;

        public DatabaseService()
        {
            _dbPath = Path.Combine(FileSystem.AppDataDirectory, "recipes.db");
            _connectionString = $"Data Source={_dbPath}";
            InitializeDatabase();
            SeedTestRecipe();
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(_dbPath))
            {
                using var connection = new SqliteConnection(_connectionString);
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Recipes (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Description TEXT,
                        ImageUrl TEXT,
                        MinPrice REAL,
                        MaxPrice REAL,
                        DietaryPreference TEXT,
                        Allergens TEXT,
                        Ingredients TEXT,
                        Instructions TEXT,
                        CreatedAt TEXT,
                        UpdatedAt TEXT
                    );";
                command.ExecuteNonQuery();
            }
        }

        private void SeedTestRecipe()
        {
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                connection.Open();

                // Check if we already have recipes
                var checkCommand = connection.CreateCommand();
                checkCommand.CommandText = "SELECT COUNT(*) FROM Recipes";
                var count = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (count == 0)
                {
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        INSERT INTO Recipes (
                            Name, Description, ImageUrl, MinPrice, MaxPrice, 
                            DietaryPreference, Allergens, Ingredients, Instructions, 
                            CreatedAt, UpdatedAt
                        ) VALUES (
                            @Name, @Description, @ImageUrl, @MinPrice, @MaxPrice,
                            @DietaryPreference, @Allergens, @Ingredients, @Instructions,
                            @CreatedAt, @UpdatedAt
                        );";

                    // Add Chicken Adobo (None dietary preference, contains soy)
                    command.Parameters.AddWithValue("@Name", "Chicken Adobo");
                    command.Parameters.AddWithValue("@Description", "A classic Filipino dish featuring chicken marinated in vinegar, soy sauce, and garlic, then braised until tender.");
                    command.Parameters.AddWithValue("@ImageUrl", "adobo.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 250.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 350.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "None");
                    command.Parameters.AddWithValue("@Allergens", "Soy");
                    command.Parameters.AddWithValue("@Ingredients", "Chicken, Soy Sauce, Vinegar, Garlic, Bay Leaves, Black Peppercorns");
                    command.Parameters.AddWithValue("@Instructions", "1. Combine chicken, soy sauce, vinegar, garlic, bay leaves, and peppercorns in a bowl.\n2. Marinate for at least 30 minutes.\n3. Transfer to a pot and bring to a boil.\n4. Simmer for 30-40 minutes until chicken is tender.\n5. Serve hot with rice.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Add Vegetarian Sinigang (Vegetarian, contains peanuts)
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Vegetarian Sinigang");
                    command.Parameters.AddWithValue("@Description", "A sour and savory Filipino soup made with vegetables and tamarind broth.");
                    command.Parameters.AddWithValue("@ImageUrl", "sinigang.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 200.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 300.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Vegetarian");
                    command.Parameters.AddWithValue("@Allergens", "Peanuts");
                    command.Parameters.AddWithValue("@Ingredients", "Tamarind Broth, Kangkong, Radish, Tomatoes, Onions, Peanuts");
                    command.Parameters.AddWithValue("@Instructions", "1. Prepare tamarind broth.\n2. Add vegetables in order of cooking time.\n3. Season with salt and pepper.\n4. Serve hot with rice.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Add Vegan Lumpia (Vegan, no allergens)
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Vegan Lumpia");
                    command.Parameters.AddWithValue("@Description", "Crispy spring rolls filled with vegetables and tofu.");
                    command.Parameters.AddWithValue("@ImageUrl", "lumpia.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 150.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 250.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Vegan");
                    command.Parameters.AddWithValue("@Allergens", "");
                    command.Parameters.AddWithValue("@Ingredients", "Lumpia Wrapper, Tofu, Carrots, Cabbage, Green Beans, Garlic");
                    command.Parameters.AddWithValue("@Instructions", "1. Prepare vegetable filling.\n2. Wrap in lumpia wrapper.\n3. Deep fry until golden brown.\n4. Serve with sweet chili sauce.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Add Keto Sisig (Keto, contains dairy)
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Sisig");
                    command.Parameters.AddWithValue("@Description", "A low-carb version of the popular Filipino sisig dish.");
                    command.Parameters.AddWithValue("@ImageUrl", "sisig.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 300.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 400.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "Dairy");
                    command.Parameters.AddWithValue("@Ingredients", "Pork Belly, Eggs, Mayonnaise, Onions, Chili, Lime");
                    command.Parameters.AddWithValue("@Instructions", "1. Grill pork belly until crispy.\n2. Chop into small pieces.\n3. Mix with mayonnaise and seasonings.\n4. Top with egg and serve hot.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Log the error but don't throw - we don't want to break the app if seeding fails
                System.Diagnostics.Debug.WriteLine($"Error seeding test recipes: {ex.Message}");
            }
        }

        public async Task<List<Recipe>> GetFilteredRecipesAsync(decimal maxBudget, string dietaryPreference, List<string> allergens)
        {
            var recipes = new List<Recipe>();
            
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            var allergenConditions = new List<string>();
            
            // Build the base query
            var query = @"
                SELECT * FROM Recipes 
                WHERE MaxPrice <= @MaxBudget 
                AND (DietaryPreference = @DietaryPreference OR DietaryPreference = 'None')";

            command.Parameters.AddWithValue("@MaxBudget", maxBudget);
            command.Parameters.AddWithValue("@DietaryPreference", dietaryPreference);

            // Add allergen conditions if any allergens are specified
            if (allergens != null && allergens.Any())
            {
                for (int i = 0; i < allergens.Count; i++)
                {
                    var paramName = $"@Allergen{i}";
                    query += $" AND Allergens NOT LIKE {paramName}";
                    command.Parameters.AddWithValue(paramName, $"%{allergens[i]}%");
                }
            }

            command.CommandText = query;

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                recipes.Add(new Recipe
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    ImageUrl = reader.GetString(3),
                    MinPrice = reader.GetDecimal(4),
                    MaxPrice = reader.GetDecimal(5),
                    DietaryPreference = reader.GetString(6),
                    Allergens = reader.GetString(7).Split(',').ToList(),
                    Ingredients = reader.GetString(8).Split(',').ToList(),
                    Instructions = reader.GetString(9),
                    CreatedAt = DateTime.Parse(reader.GetString(10)),
                    UpdatedAt = DateTime.Parse(reader.GetString(11))
                });
            }

            return recipes;
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Recipes WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Recipe
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    ImageUrl = reader.GetString(3),
                    MinPrice = reader.GetDecimal(4),
                    MaxPrice = reader.GetDecimal(5),
                    DietaryPreference = reader.GetString(6),
                    Allergens = reader.GetString(7).Split(',').ToList(),
                    Ingredients = reader.GetString(8).Split(',').ToList(),
                    Instructions = reader.GetString(9),
                    CreatedAt = DateTime.Parse(reader.GetString(10)),
                    UpdatedAt = DateTime.Parse(reader.GetString(11))
                };
            }

            return null;
        }
    }
} 