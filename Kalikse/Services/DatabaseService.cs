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

                // Keto Bulletproof Coffee
                command.Parameters.AddWithValue("@Name", "Keto Bulletproof Coffee");
                command.Parameters.AddWithValue("@Description", "A creamy, energizing coffee drink that helps you stay in ketosis.");
                command.Parameters.AddWithValue("@ImageUrl", "bulletproof_coffee.jpg");
                command.Parameters.AddWithValue("@MinPrice", 100.00m);
                command.Parameters.AddWithValue("@MaxPrice", 150.00m);
                command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                command.Parameters.AddWithValue("@Allergens", "Dairy");
                command.Parameters.AddWithValue("@Ingredients", "Coffee, Butter, MCT Oil, Heavy Cream, Cinnamon");
                command.Parameters.AddWithValue("@Instructions", "1. Brew strong coffee\n2. Add 1 tbsp butter\n3. Add 1 tbsp MCT oil\n4. Add 2 tbsp heavy cream\n5. Blend until frothy\n6. Sprinkle with cinnamon");
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                command.ExecuteNonQuery();

                // Keto Avocado Egg Salad
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Name", "Keto Avocado Egg Salad");
                command.Parameters.AddWithValue("@Description", "A creamy, protein-rich salad perfect for keto lunches.");
                command.Parameters.AddWithValue("@ImageUrl", "avocado_egg_salad.jpg");
                command.Parameters.AddWithValue("@MinPrice", 150.00m);
                command.Parameters.AddWithValue("@MaxPrice", 200.00m);
                command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                command.Parameters.AddWithValue("@Allergens", "Eggs");
                command.Parameters.AddWithValue("@Ingredients", "Hard-boiled Eggs, Avocado, Mayonnaise, Dijon Mustard, Celery, Green Onions, Salt, Pepper");
                command.Parameters.AddWithValue("@Instructions", "1. Chop hard-boiled eggs\n2. Mash avocado\n3. Mix all ingredients\n4. Season to taste\n5. Serve on lettuce leaves");
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                command.ExecuteNonQuery();

                // Keto Cauliflower Rice Stir-Fry
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Name", "Keto Cauliflower Rice Stir-Fry");
                command.Parameters.AddWithValue("@Description", "A low-carb alternative to traditional fried rice.");
                command.Parameters.AddWithValue("@ImageUrl", "cauliflower_stirfry.jpg");
                command.Parameters.AddWithValue("@MinPrice", 200.00m);
                command.Parameters.AddWithValue("@MaxPrice", 300.00m);
                command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                command.Parameters.AddWithValue("@Allergens", "Eggs, Soy");
                command.Parameters.AddWithValue("@Ingredients", "Cauliflower Rice, Eggs, Bacon, Green Onions, Soy Sauce, Garlic, Ginger, Sesame Oil");
                command.Parameters.AddWithValue("@Instructions", "1. Cook bacon until crispy\n2. Scramble eggs\n3. Stir-fry cauliflower rice\n4. Add all ingredients\n5. Season with soy sauce and sesame oil");
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                command.ExecuteNonQuery();

                // Keto Fat Bombs
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Name", "Keto Fat Bombs");
                command.Parameters.AddWithValue("@Description", "Delicious energy-boosting snacks for keto dieters.");
                command.Parameters.AddWithValue("@ImageUrl", "fat_bombs.jpg");
                command.Parameters.AddWithValue("@MinPrice", 100.00m);
                command.Parameters.AddWithValue("@MaxPrice", 150.00m);
                command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                command.Parameters.AddWithValue("@Allergens", "Dairy, Nuts");
                command.Parameters.AddWithValue("@Ingredients", "Coconut Oil, Cream Cheese, Cocoa Powder, Stevia, Vanilla Extract, Almonds");
                command.Parameters.AddWithValue("@Instructions", "1. Mix softened cream cheese with coconut oil\n2. Add cocoa powder and sweetener\n3. Form into balls\n4. Roll in chopped almonds\n5. Freeze for 30 minutes");
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                command.ExecuteNonQuery();

                // Keto Zucchini Lasagna
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Name", "Keto Zucchini Lasagna");
                command.Parameters.AddWithValue("@Description", "A low-carb version of the classic Italian dish.");
                command.Parameters.AddWithValue("@ImageUrl", "zucchini_lasagna.jpg");
                command.Parameters.AddWithValue("@MinPrice", 300.00m);
                command.Parameters.AddWithValue("@MaxPrice", 400.00m);
                command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                command.Parameters.AddWithValue("@Allergens", "Dairy");
                command.Parameters.AddWithValue("@Ingredients", "Zucchini, Ground Beef, Marinara Sauce, Ricotta Cheese, Mozzarella, Parmesan, Italian Seasoning");
                command.Parameters.AddWithValue("@Instructions", "1. Slice zucchini lengthwise\n2. Layer with meat sauce and cheeses\n3. Bake at 375°F for 45 minutes\n4. Let rest 10 minutes\n5. Serve hot");
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                command.ExecuteNonQuery();

                // Keto Chicken Alfredo
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Name", "Keto Chicken Alfredo");
                command.Parameters.AddWithValue("@Description", "Creamy pasta alternative using zucchini noodles.");
                command.Parameters.AddWithValue("@ImageUrl", "chicken_alfredo.jpg");
                command.Parameters.AddWithValue("@MinPrice", 250.00m);
                command.Parameters.AddWithValue("@MaxPrice", 350.00m);
                command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                command.Parameters.AddWithValue("@Allergens", "Dairy");
                command.Parameters.AddWithValue("@Ingredients", "Chicken Breast, Zucchini Noodles, Heavy Cream, Parmesan, Garlic, Butter, Italian Seasoning");
                command.Parameters.AddWithValue("@Instructions", "1. Cook chicken until golden\n2. Prepare zucchini noodles\n3. Make alfredo sauce\n4. Combine all ingredients\n5. Garnish with extra parmesan");
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                command.ExecuteNonQuery();

                // Keto Taco Bowl
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Name", "Keto Taco Bowl");
                command.Parameters.AddWithValue("@Description", "A Mexican-inspired bowl without the carbs.");
                command.Parameters.AddWithValue("@ImageUrl", "taco_bowl.jpg");
                command.Parameters.AddWithValue("@MinPrice", 200.00m);
                command.Parameters.AddWithValue("@MaxPrice", 300.00m);
                command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                command.Parameters.AddWithValue("@Allergens", "Dairy");
                command.Parameters.AddWithValue("@Ingredients", "Ground Beef, Lettuce, Avocado, Cheese, Sour Cream, Tomatoes, Taco Seasoning");
                command.Parameters.AddWithValue("@Instructions", "1. Cook seasoned ground beef\n2. Prepare lettuce base\n3. Add toppings\n4. Garnish with cheese and sour cream\n5. Serve immediately");
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                command.ExecuteNonQuery();

                // Keto Chocolate Mousse
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Name", "Keto Chocolate Mousse");
                command.Parameters.AddWithValue("@Description", "A rich, creamy dessert that's keto-friendly.");
                command.Parameters.AddWithValue("@ImageUrl", "chocolate_mousse.jpg");
                command.Parameters.AddWithValue("@MinPrice", 150.00m);
                command.Parameters.AddWithValue("@MaxPrice", 200.00m);
                command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                command.Parameters.AddWithValue("@Allergens", "Dairy");
                command.Parameters.AddWithValue("@Ingredients", "Heavy Cream, Dark Chocolate, Erythritol, Vanilla Extract, Salt");
                command.Parameters.AddWithValue("@Instructions", "1. Melt chocolate\n2. Whip cream with sweetener\n3. Fold in chocolate\n4. Add vanilla and salt\n5. Chill for 2 hours");
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                command.ExecuteNonQuery();

                // Keto Salmon with Avocado Salsa
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Name", "Keto Salmon with Avocado Salsa");
                command.Parameters.AddWithValue("@Description", "A healthy, omega-3 rich meal perfect for keto.");
                command.Parameters.AddWithValue("@ImageUrl", "salmon_salsa.jpg");
                command.Parameters.AddWithValue("@MinPrice", 300.00m);
                command.Parameters.AddWithValue("@MaxPrice", 400.00m);
                command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                command.Parameters.AddWithValue("@Allergens", "");
                command.Parameters.AddWithValue("@Ingredients", "Salmon Fillet, Avocado, Lime, Cilantro, Red Onion, Olive Oil, Salt, Pepper");
                command.Parameters.AddWithValue("@Instructions", "1. Season and bake salmon\n2. Prepare avocado salsa\n3. Combine lime juice and olive oil\n4. Top salmon with salsa\n5. Garnish with cilantro");
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                command.ExecuteNonQuery();

                // Keto Breakfast Casserole
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Name", "Keto Breakfast Casserole");
                command.Parameters.AddWithValue("@Description", "A hearty breakfast that keeps you full until lunch.");
                command.Parameters.AddWithValue("@ImageUrl", "breakfast_casserole.jpg");
                command.Parameters.AddWithValue("@MinPrice", 250.00m);
                command.Parameters.AddWithValue("@MaxPrice", 350.00m);
                command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                command.Parameters.AddWithValue("@Allergens", "Eggs, Dairy");
                command.Parameters.AddWithValue("@Ingredients", "Eggs, Sausage, Cheese, Bell Peppers, Onions, Heavy Cream, Salt, Pepper");
                command.Parameters.AddWithValue("@Instructions", "1. Cook sausage and vegetables\n2. Beat eggs with cream\n3. Layer ingredients in casserole dish\n4. Bake at 350°F for 45 minutes\n5. Let rest 10 minutes before serving");
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                command.ExecuteNonQuery();
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