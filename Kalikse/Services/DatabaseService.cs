using Microsoft.Data.Sqlite;
using Kalikse.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Text.Json;

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

                    // Keto Chicken Adobo
                    command.Parameters.AddWithValue("@Name", "Keto Chicken Adobo");
                    command.Parameters.AddWithValue("@Description", "A Filipino classic reimagined for keto, featuring tender chicken thighs braised in vinegar, coconut aminos (instead of soy sauce), and garlic. This low-carb version maintains all the savory, tangy flavors of traditional adobo while fitting perfectly into a ketogenic diet.");
                    command.Parameters.AddWithValue("@ImageUrl", "chicken_adobo.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 250.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 350.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Chicken Thighs", MinPrice = 150, MaxPrice = 200 },
                        new Ingredient { Name = "Coconut Aminos", MinPrice = 50, MaxPrice = 70 },
                        new Ingredient { Name = "White Vinegar", MinPrice = 20, MaxPrice = 30 },
                        new Ingredient { Name = "Garlic", MinPrice = 10, MaxPrice = 15 },
                        new Ingredient { Name = "Bay Leaves", MinPrice = 10, MaxPrice = 15 },
                        new Ingredient { Name = "Black Peppercorns", MinPrice = 10, MaxPrice = 15 },
                        new Ingredient { Name = "Olive Oil", MinPrice = 30, MaxPrice = 40 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Season chicken thighs with salt and pepper. In a large skillet over medium heat, heat 2 tablespoons olive oil. Add chicken, skin-side down, and cook until golden and cooked through (about 6–7 minutes per side). Remove and set aside.\n\n2. In the same skillet, add 2 minced garlic cloves and cook until fragrant. Add 1/4 cup coconut aminos, 1/4 cup white vinegar, 1 bay leaf, and 1 teaspoon black peppercorns. Simmer for 5 minutes.\n\n3. Return chicken to the skillet, skin-side up, and simmer for another 5 minutes. Serve hot, garnished with extra garlic and bay leaves.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Sinigang with Cauliflower Rice
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Sinigang with Cauliflower Rice");
                    command.Parameters.AddWithValue("@Description", "A keto-friendly version of the beloved Filipino sour soup, using low-carb vegetables and cauliflower rice. This comforting dish maintains the signature tangy flavor while keeping carbs in check, perfect for those missing traditional Filipino flavors on their keto journey.");
                    command.Parameters.AddWithValue("@ImageUrl", "sinigang.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 300.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 400.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Pork Belly", MinPrice = 150, MaxPrice = 200 },
                        new Ingredient { Name = "Cauliflower Rice", MinPrice = 50, MaxPrice = 70 },
                        new Ingredient { Name = "Radish", MinPrice = 20, MaxPrice = 30 },
                        new Ingredient { Name = "Spinach", MinPrice = 30, MaxPrice = 40 },
                        new Ingredient { Name = "Tomatoes", MinPrice = 20, MaxPrice = 30 },
                        new Ingredient { Name = "Sinigang Mix", MinPrice = 15, MaxPrice = 25 },
                        new Ingredient { Name = "Fish Sauce", MinPrice = 15, MaxPrice = 20 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Cook pork belly until crispy and fat is rendered. Remove and set aside. In the same pan, add 2 minced garlic cloves and cook until fragrant.\n\n2. Add 1 cup cauliflower rice, 1 diced radish, 1/2 cup spinach, and 1/2 cup diced tomatoes. Cook for 5 minutes.\n\n3. Add 1/2 cup fish sauce, 1/2 cup coconut aminos, and 1/2 cup water. Simmer for 5 minutes.\n\n4. Add 1/2 cup cooked pork belly and 1/2 cup water. Simmer for another 5 minutes. Serve hot, garnished with extra garlic and fish sauce.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Sisig
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Sisig");
                    command.Parameters.AddWithValue("@Description", "A sizzling keto version of the popular Filipino sisig, made with chopped pork belly and chicken liver, seasoned with calamansi and chili. This protein-rich dish is naturally low in carbs while delivering all the bold, spicy flavors of the original.");
                    command.Parameters.AddWithValue("@ImageUrl", "sisig.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 250.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 350.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "Eggs");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Pork Belly", MinPrice = 150, MaxPrice = 200 },
                        new Ingredient { Name = "Chicken Liver", MinPrice = 40, MaxPrice = 60 },
                        new Ingredient { Name = "Onions", MinPrice = 15, MaxPrice = 20 },
                        new Ingredient { Name = "Calamansi", MinPrice = 20, MaxPrice = 30 },
                        new Ingredient { Name = "Chili Peppers", MinPrice = 10, MaxPrice = 15 },
                        new Ingredient { Name = "Eggs", MinPrice = 20, MaxPrice = 30 },
                        new Ingredient { Name = "Mayonnaise", MinPrice = 30, MaxPrice = 40 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Chop pork belly and chicken liver. In a large skillet over medium heat, cook until crispy. Remove and set aside.\n\n2. In the same pan, add 2 minced garlic cloves and cook until fragrant. Add 1/2 cup chopped onions and 1/2 cup calamansi juice. Cook for 5 minutes.\n\n3. Add 1/2 cup chicken liver and 1/2 cup pork belly. Cook for another 5 minutes.\n\n4. Serve hot, garnished with extra garlic, onions, and calamansi. Top with 1/4 cup mayonnaise.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Bicol Express
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Bicol Express");
                    command.Parameters.AddWithValue("@Description", "A rich and creamy keto adaptation of the spicy Bicolano favorite, using pork belly and coconut cream. This dish is naturally keto-friendly and packed with healthy fats, making it an ideal choice for those following a ketogenic diet while craving Filipino flavors.");
                    command.Parameters.AddWithValue("@ImageUrl", "bicol_express.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 280.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 380.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Pork Belly", MinPrice = 150, MaxPrice = 200 },
                        new Ingredient { Name = "Coconut Cream", MinPrice = 50, MaxPrice = 70 },
                        new Ingredient { Name = "Shrimp Paste", MinPrice = 20, MaxPrice = 30 },
                        new Ingredient { Name = "Green Chilies", MinPrice = 20, MaxPrice = 30 },
                        new Ingredient { Name = "Garlic", MinPrice = 10, MaxPrice = 15 },
                        new Ingredient { Name = "Ginger", MinPrice = 10, MaxPrice = 15 },
                        new Ingredient { Name = "Onions", MinPrice = 15, MaxPrice = 20 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Chop pork belly and cook until crispy. Remove and set aside. In the same pan, add 2 minced garlic cloves and cook until fragrant.\n\n2. Add 1/2 cup chopped onions and 1/2 cup coconut cream. Cook for 5 minutes.\n\n3. Add 1/2 cup shrimp paste and 1/2 cup green chilies. Cook for another 5 minutes.\n\n4. Serve hot, garnished with extra garlic, onions, and coconut cream.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Ginataang Manok
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Ginataang Manok");
                    command.Parameters.AddWithValue("@Description", "A keto-friendly version of chicken cooked in coconut milk, using low-carb vegetables. This creamy, satisfying dish showcases the rich flavors of Filipino coconut-based cuisine while keeping carbs minimal.");
                    command.Parameters.AddWithValue("@ImageUrl", "ginataang_manok.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 260.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 360.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Chicken Pieces", MinPrice = 150, MaxPrice = 200 },
                        new Ingredient { Name = "Coconut Milk", MinPrice = 50, MaxPrice = 70 },
                        new Ingredient { Name = "Spinach", MinPrice = 30, MaxPrice = 40 },
                        new Ingredient { Name = "Green Chilies", MinPrice = 15, MaxPrice = 20 },
                        new Ingredient { Name = "Ginger", MinPrice = 10, MaxPrice = 15 },
                        new Ingredient { Name = "Garlic", MinPrice = 10, MaxPrice = 15 },
                        new Ingredient { Name = "Fish Sauce", MinPrice = 15, MaxPrice = 20 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Cook chicken pieces until browned. Remove and set aside. In the same pan, add 1/2 cup coconut milk and 1/2 cup water. Simmer for 5 minutes.\n\n2. Add 1/2 cup spinach and 1/2 cup green chilies. Cook for another 5 minutes.\n\n3. Return chicken to the pan and cook for another 5 minutes. Serve hot, garnished with extra coconut milk and fish sauce.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Tinolang Manok
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Tinolang Manok");
                    command.Parameters.AddWithValue("@Description", "A light and nourishing Filipino chicken soup made keto-friendly by using low-carb vegetables. This clear, ginger-flavored broth with tender chicken pieces offers comfort without the carbs.");
                    command.Parameters.AddWithValue("@ImageUrl", "tinola.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 240.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 340.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Chicken Pieces", MinPrice = 150, MaxPrice = 200 },
                        new Ingredient { Name = "Chayote", MinPrice = 30, MaxPrice = 40 },
                        new Ingredient { Name = "Moringa Leaves", MinPrice = 20, MaxPrice = 30 },
                        new Ingredient { Name = "Ginger", MinPrice = 15, MaxPrice = 20 },
                        new Ingredient { Name = "Garlic", MinPrice = 10, MaxPrice = 15 },
                        new Ingredient { Name = "Onions", MinPrice = 15, MaxPrice = 20 },
                        new Ingredient { Name = "Fish Sauce", MinPrice = 15, MaxPrice = 20 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Cook chicken pieces until browned. Remove and set aside. In the same pan, add 1/2 cup coconut milk and 1/2 cup water. Simmer for 5 minutes.\n\n2. Add 1/2 cup chayote and 1/2 cup moringa leaves. Cook for another 5 minutes.\n\n3. Return chicken to the pan and cook for another 5 minutes. Serve hot, garnished with extra coconut milk and fish sauce.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Tapa with Cauliflower Rice
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Tapa with Cauliflower Rice");
                    command.Parameters.AddWithValue("@Description", "A keto twist on the Filipino breakfast favorite tapsilog, featuring marinated beef tapa served with cauliflower rice and fried eggs. This low-carb version delivers all the savory flavors of traditional tapa while keeping it keto-friendly.");
                    command.Parameters.AddWithValue("@ImageUrl", "tapa.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 270.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 370.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "Eggs");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Beef Sirloin", MinPrice = 180, MaxPrice = 230 },
                        new Ingredient { Name = "Cauliflower Rice", MinPrice = 50, MaxPrice = 70 },
                        new Ingredient { Name = "Eggs", MinPrice = 30, MaxPrice = 40 },
                        new Ingredient { Name = "Garlic", MinPrice = 10, MaxPrice = 15 },
                        new Ingredient { Name = "Coconut Aminos", MinPrice = 40, MaxPrice = 50 },
                        new Ingredient { Name = "Black Pepper", MinPrice = 10, MaxPrice = 15 },
                        new Ingredient { Name = "Olive Oil", MinPrice = 30, MaxPrice = 40 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Marinate beef sirloin in coconut aminos, garlic, and black pepper for at least 30 minutes.\n\n2. Cook cauliflower rice in a large skillet over medium heat until golden.\n\n3. In the same pan, cook eggs until desired consistency. Remove and set aside.\n\n4. Cook marinated beef until browned. Remove and set aside.\n\n5. Serve beef, cauliflower rice, and eggs together, garnished with extra coconut aminos and black pepper.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Ginisang Munggo with Chicharon
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Ginisang Munggo with Chicharon");
                    command.Parameters.AddWithValue("@Description", "A creative keto take on the Filipino mung bean stew, using spinach as the base and topped with crispy chicharon. This innovative adaptation maintains the comfort-food appeal while keeping it low-carb.");
                    command.Parameters.AddWithValue("@ImageUrl", "munggo.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 220.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 320.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Spinach", MinPrice = 40, MaxPrice = 50 },
                        new Ingredient { Name = "Chicharon", MinPrice = 50, MaxPrice = 70 },
                        new Ingredient { Name = "Pork Belly", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Garlic", MinPrice = 10, MaxPrice = 15 },
                        new Ingredient { Name = "Onions", MinPrice = 15, MaxPrice = 20 },
                        new Ingredient { Name = "Fish Sauce", MinPrice = 15, MaxPrice = 20 },
                        new Ingredient { Name = "Black Pepper", MinPrice = 10, MaxPrice = 15 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Cook pork belly until crispy and fat is rendered. Remove and set aside. In the same pan, add 2 minced garlic cloves and 1/2 cup chopped onions. Cook until fragrant.\n\n2. Add 1/2 cup mung beans and 1/2 cup water. Simmer for 5 minutes.\n\n3. Add 1/2 cup spinach and 1/2 cup chicharon. Cook for another 5 minutes.\n\n4. Serve hot, garnished with extra pork fat, garlic, onions, and fish sauce.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Ube Cheesecake
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Ube Cheesecake");
                    command.Parameters.AddWithValue("@Description", "A Filipino-inspired keto dessert featuring the beloved ube flavor in a rich, creamy cheesecake with an almond flour crust. This purple beauty offers the authentic taste of ube while keeping carbs low using keto-friendly sweeteners.");
                    command.Parameters.AddWithValue("@ImageUrl", "ube_cheesecake.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 300.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 400.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "Dairy, Nuts");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Cream Cheese", MinPrice = 150, MaxPrice = 200 },
                        new Ingredient { Name = "Almond Flour", MinPrice = 80, MaxPrice = 100 },
                        new Ingredient { Name = "Ube Extract", MinPrice = 30, MaxPrice = 40 },
                        new Ingredient { Name = "Erythritol", MinPrice = 40, MaxPrice = 50 },
                        new Ingredient { Name = "Eggs", MinPrice = 30, MaxPrice = 40 },
                        new Ingredient { Name = "Butter", MinPrice = 30, MaxPrice = 40 },
                        new Ingredient { Name = "Heavy Cream", MinPrice = 40, MaxPrice = 50 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Preheat oven to 160°C (325°F). Grease a 9-inch springform pan.\n\n2. Mix almond flour, ube extract, erythritol, and eggs until well combined. Pour into the prepared pan.\n\n3. Bake for 10 minutes. Remove and let cool.\n\n4. In a separate bowl, mix cream cheese, butter, and heavy cream until smooth.\n\n5. Spread over cooled almond mixture. Bake for another 10 minutes. Let cool completely before serving.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Leche Flan
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Leche Flan");
                    command.Parameters.AddWithValue("@Description", "A sugar-free version of the classic Filipino custard dessert, made with low-carb sweeteners and rich egg yolks. This keto adaptation maintains the silky smooth texture and caramel flavor of traditional leche flan without the sugar.");
                    command.Parameters.AddWithValue("@ImageUrl", "leche_flan.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 200.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 300.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "Eggs, Dairy");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Egg Yolks", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Heavy Cream", MinPrice = 50, MaxPrice = 70 },
                        new Ingredient { Name = "Allulose", MinPrice = 40, MaxPrice = 50 },
                        new Ingredient { Name = "Vanilla Extract", MinPrice = 20, MaxPrice = 30 },
                        new Ingredient { Name = "Salt", MinPrice = 5, MaxPrice = 10 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Preheat oven to 160°C (325°F). Grease a 9-inch springform pan.\n\n2. Whisk egg yolks, heavy cream, allulose, vanilla extract, and salt until well combined. Pour into the prepared pan.\n\n3. Bake for 30 minutes. Let cool completely before serving.");
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
                var ingredientsJson = reader.GetString(8);
                var ingredients = JsonSerializer.Deserialize<List<Ingredient>>(ingredientsJson);

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
                    Ingredients = ingredients,
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
                var ingredientsJson = reader.GetString(8);
                var ingredients = JsonSerializer.Deserialize<List<Ingredient>>(ingredientsJson);

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
                    Ingredients = ingredients,
                    Instructions = reader.GetString(9),
                    CreatedAt = DateTime.Parse(reader.GetString(10)),
                    UpdatedAt = DateTime.Parse(reader.GetString(11))
                };
            }

            return null;
        }

        public async Task ResetRecipesAsync()
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            // Delete all recipes
            var deleteCommand = connection.CreateCommand();
            deleteCommand.CommandText = "DELETE FROM Recipes";
            await deleteCommand.ExecuteNonQueryAsync();

            // Reseed the 10 keto recipes
            // (Reuse the logic from SeedTestRecipe, but without the count check)
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

            // Keto Chicken Adobo
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Name", "Keto Chicken Adobo");
            command.Parameters.AddWithValue("@Description", "A Filipino classic reimagined for keto, featuring tender chicken thighs braised in vinegar, coconut aminos (instead of soy sauce), and garlic. This low-carb version maintains all the savory, tangy flavors of traditional adobo while fitting perfectly into a ketogenic diet.");
            command.Parameters.AddWithValue("@ImageUrl", "chicken_adobo.jpg");
            command.Parameters.AddWithValue("@MinPrice", 250.00m);
            command.Parameters.AddWithValue("@MaxPrice", 350.00m);
            command.Parameters.AddWithValue("@DietaryPreference", "Keto");
            command.Parameters.AddWithValue("@Allergens", "");
            command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
            {
                new Ingredient { Name = "Chicken Thighs", MinPrice = 150, MaxPrice = 200 },
                new Ingredient { Name = "Coconut Aminos", MinPrice = 50, MaxPrice = 70 },
                new Ingredient { Name = "White Vinegar", MinPrice = 20, MaxPrice = 30 },
                new Ingredient { Name = "Garlic", MinPrice = 10, MaxPrice = 15 },
                new Ingredient { Name = "Bay Leaves", MinPrice = 10, MaxPrice = 15 },
                new Ingredient { Name = "Black Peppercorns", MinPrice = 10, MaxPrice = 15 },
                new Ingredient { Name = "Olive Oil", MinPrice = 30, MaxPrice = 40 }
            }));
            command.Parameters.AddWithValue("@Instructions", "1. Season chicken thighs with salt and pepper. In a large skillet over medium heat, heat 2 tablespoons olive oil. Add chicken, skin-side down, and cook until golden and cooked through (about 6–7 minutes per side). Remove and set aside.\n\n2. In the same skillet, add 2 minced garlic cloves and cook until fragrant. Add 1/4 cup coconut aminos, 1/4 cup white vinegar, 1 bay leaf, and 1 teaspoon black peppercorns. Simmer for 5 minutes.\n\n3. Return chicken to the skillet, skin-side up, and simmer for another 5 minutes. Serve hot, garnished with extra garlic and bay leaves.");
            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
            command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
            await command.ExecuteNonQueryAsync();

            // Keto Sinigang with Cauliflower Rice
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Name", "Keto Sinigang with Cauliflower Rice");
            command.Parameters.AddWithValue("@Description", "A keto-friendly version of the beloved Filipino sour soup, using low-carb vegetables and cauliflower rice. This comforting dish maintains the signature tangy flavor while keeping carbs in check, perfect for those missing traditional Filipino flavors on their keto journey.");
            command.Parameters.AddWithValue("@ImageUrl", "sinigang.jpg");
            command.Parameters.AddWithValue("@MinPrice", 300.00m);
            command.Parameters.AddWithValue("@MaxPrice", 400.00m);
            command.Parameters.AddWithValue("@DietaryPreference", "Keto");
            command.Parameters.AddWithValue("@Allergens", "");
            command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
            {
                new Ingredient { Name = "Pork Belly", MinPrice = 150, MaxPrice = 200 },
                new Ingredient { Name = "Cauliflower Rice", MinPrice = 50, MaxPrice = 70 },
                new Ingredient { Name = "Radish", MinPrice = 20, MaxPrice = 30 },
                new Ingredient { Name = "Spinach", MinPrice = 30, MaxPrice = 40 },
                new Ingredient { Name = "Tomatoes", MinPrice = 20, MaxPrice = 30 },
                new Ingredient { Name = "Sinigang Mix", MinPrice = 15, MaxPrice = 25 },
                new Ingredient { Name = "Fish Sauce", MinPrice = 15, MaxPrice = 20 }
            }));
            command.Parameters.AddWithValue("@Instructions", "1. Cook pork belly until crispy and fat is rendered. Remove and set aside. In the same pan, add 2 minced garlic cloves and cook until fragrant.\n\n2. Add 1 cup cauliflower rice, 1 diced radish, 1/2 cup spinach, and 1/2 cup diced tomatoes. Cook for 5 minutes.\n\n3. Add 1/2 cup fish sauce, 1/2 cup coconut aminos, and 1/2 cup water. Simmer for 5 minutes.\n\n4. Add 1/2 cup cooked pork belly and 1/2 cup water. Simmer for another 5 minutes. Serve hot, garnished with extra garlic and fish sauce.");
            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
            command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
            await command.ExecuteNonQueryAsync();

            // Keto Sisig
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Name", "Keto Sisig");
            command.Parameters.AddWithValue("@Description", "A sizzling keto version of the popular Filipino sisig, made with chopped pork belly and chicken liver, seasoned with calamansi and chili. This protein-rich dish is naturally low in carbs while delivering all the bold, spicy flavors of the original.");
            command.Parameters.AddWithValue("@ImageUrl", "sisig.jpg");
            command.Parameters.AddWithValue("@MinPrice", 250.00m);
            command.Parameters.AddWithValue("@MaxPrice", 350.00m);
            command.Parameters.AddWithValue("@DietaryPreference", "Keto");
            command.Parameters.AddWithValue("@Allergens", "Eggs");
            command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
            {
                new Ingredient { Name = "Pork Belly", MinPrice = 150, MaxPrice = 200 },
                new Ingredient { Name = "Chicken Liver", MinPrice = 40, MaxPrice = 60 },
                new Ingredient { Name = "Onions", MinPrice = 15, MaxPrice = 20 },
                new Ingredient { Name = "Calamansi", MinPrice = 20, MaxPrice = 30 },
                new Ingredient { Name = "Chili Peppers", MinPrice = 10, MaxPrice = 15 },
                new Ingredient { Name = "Eggs", MinPrice = 20, MaxPrice = 30 },
                new Ingredient { Name = "Mayonnaise", MinPrice = 30, MaxPrice = 40 }
            }));
            command.Parameters.AddWithValue("@Instructions", "1. Chop pork belly and chicken liver. In a large skillet over medium heat, cook until crispy. Remove and set aside.\n\n2. In the same pan, add 2 minced garlic cloves and cook until fragrant. Add 1/2 cup chopped onions and 1/2 cup calamansi juice. Cook for 5 minutes.\n\n3. Add 1/2 cup chicken liver and 1/2 cup pork belly. Cook for another 5 minutes.\n\n4. Serve hot, garnished with extra garlic, onions, and calamansi. Top with 1/4 cup mayonnaise.");
            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
            command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
            await command.ExecuteNonQueryAsync();

            // Keto Bicol Express
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Name", "Keto Bicol Express");
            command.Parameters.AddWithValue("@Description", "A rich and creamy keto adaptation of the spicy Bicolano favorite, using pork belly and coconut cream. This dish is naturally keto-friendly and packed with healthy fats, making it an ideal choice for those following a ketogenic diet while craving Filipino flavors.");
            command.Parameters.AddWithValue("@ImageUrl", "bicol_express.jpg");
            command.Parameters.AddWithValue("@MinPrice", 280.00m);
            command.Parameters.AddWithValue("@MaxPrice", 380.00m);
            command.Parameters.AddWithValue("@DietaryPreference", "Keto");
            command.Parameters.AddWithValue("@Allergens", "");
            command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
            {
                new Ingredient { Name = "Pork Belly", MinPrice = 150, MaxPrice = 200 },
                new Ingredient { Name = "Coconut Cream", MinPrice = 50, MaxPrice = 70 },
                new Ingredient { Name = "Shrimp Paste", MinPrice = 20, MaxPrice = 30 },
                new Ingredient { Name = "Green Chilies", MinPrice = 20, MaxPrice = 30 },
                new Ingredient { Name = "Garlic", MinPrice = 10, MaxPrice = 15 },
                new Ingredient { Name = "Ginger", MinPrice = 10, MaxPrice = 15 },
                new Ingredient { Name = "Onions", MinPrice = 15, MaxPrice = 20 }
            }));
            command.Parameters.AddWithValue("@Instructions", "1. Chop pork belly and cook until crispy. Remove and set aside. In the same pan, add 2 minced garlic cloves and cook until fragrant.\n\n2. Add 1/2 cup chopped onions and 1/2 cup coconut cream. Cook for 5 minutes.\n\n3. Add 1/2 cup shrimp paste and 1/2 cup green chilies. Cook for another 5 minutes.\n\n4. Serve hot, garnished with extra garlic, onions, and coconut cream.");
            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
            command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
            await command.ExecuteNonQueryAsync();

            // Keto Ginataang Manok
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Name", "Keto Ginataang Manok");
            command.Parameters.AddWithValue("@Description", "A keto-friendly version of chicken cooked in coconut milk, using low-carb vegetables. This creamy, satisfying dish showcases the rich flavors of Filipino coconut-based cuisine while keeping carbs minimal.");
            command.Parameters.AddWithValue("@ImageUrl", "ginataang_manok.jpg");
            command.Parameters.AddWithValue("@MinPrice", 260.00m);
            command.Parameters.AddWithValue("@MaxPrice", 360.00m);
            command.Parameters.AddWithValue("@DietaryPreference", "Keto");
            command.Parameters.AddWithValue("@Allergens", "");
            command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
            {
                new Ingredient { Name = "Chicken Pieces", MinPrice = 150, MaxPrice = 200 },
                new Ingredient { Name = "Coconut Milk", MinPrice = 50, MaxPrice = 70 },
                new Ingredient { Name = "Spinach", MinPrice = 30, MaxPrice = 40 },
                new Ingredient { Name = "Green Chilies", MinPrice = 15, MaxPrice = 20 },
                new Ingredient { Name = "Ginger", MinPrice = 10, MaxPrice = 15 },
                new Ingredient { Name = "Garlic", MinPrice = 10, MaxPrice = 15 },
                new Ingredient { Name = "Fish Sauce", MinPrice = 15, MaxPrice = 20 }
            }));
            command.Parameters.AddWithValue("@Instructions", "1. Cook chicken pieces until browned. Remove and set aside. In the same pan, add 1/2 cup coconut milk and 1/2 cup water. Simmer for 5 minutes.\n\n2. Add 1/2 cup spinach and 1/2 cup green chilies. Cook for another 5 minutes.\n\n3. Return chicken to the pan and cook for another 5 minutes. Serve hot, garnished with extra coconut milk and fish sauce.");
            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
            command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
            await command.ExecuteNonQueryAsync();

            // Keto Tinolang Manok
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Name", "Keto Tinolang Manok");
            command.Parameters.AddWithValue("@Description", "A light and nourishing Filipino chicken soup made keto-friendly by using low-carb vegetables. This clear, ginger-flavored broth with tender chicken pieces offers comfort without the carbs.");
            command.Parameters.AddWithValue("@ImageUrl", "tinola.jpg");
            command.Parameters.AddWithValue("@MinPrice", 240.00m);
            command.Parameters.AddWithValue("@MaxPrice", 340.00m);
            command.Parameters.AddWithValue("@DietaryPreference", "Keto");
            command.Parameters.AddWithValue("@Allergens", "");
            command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
            {
                new Ingredient { Name = "Chicken Pieces", MinPrice = 150, MaxPrice = 200 },
                new Ingredient { Name = "Chayote", MinPrice = 30, MaxPrice = 40 },
                new Ingredient { Name = "Moringa Leaves", MinPrice = 20, MaxPrice = 30 },
                new Ingredient { Name = "Ginger", MinPrice = 15, MaxPrice = 20 },
                new Ingredient { Name = "Garlic", MinPrice = 10, MaxPrice = 15 },
                new Ingredient { Name = "Onions", MinPrice = 15, MaxPrice = 20 },
                new Ingredient { Name = "Fish Sauce", MinPrice = 15, MaxPrice = 20 }
            }));
            command.Parameters.AddWithValue("@Instructions", "1. Cook chicken pieces until browned. Remove and set aside. In the same pan, add 1/2 cup coconut milk and 1/2 cup water. Simmer for 5 minutes.\n\n2. Add 1/2 cup chayote and 1/2 cup moringa leaves. Cook for another 5 minutes.\n\n3. Return chicken to the pan and cook for another 5 minutes. Serve hot, garnished with extra coconut milk and fish sauce.");
            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
            command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
            await command.ExecuteNonQueryAsync();

            // Keto Tapa with Cauliflower Rice
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Name", "Keto Tapa with Cauliflower Rice");
            command.Parameters.AddWithValue("@Description", "A keto twist on the Filipino breakfast favorite tapsilog, featuring marinated beef tapa served with cauliflower rice and fried eggs. This low-carb version delivers all the savory flavors of traditional tapa while keeping it keto-friendly.");
            command.Parameters.AddWithValue("@ImageUrl", "tapa.jpg");
            command.Parameters.AddWithValue("@MinPrice", 270.00m);
            command.Parameters.AddWithValue("@MaxPrice", 370.00m);
            command.Parameters.AddWithValue("@DietaryPreference", "Keto");
            command.Parameters.AddWithValue("@Allergens", "Eggs");
            command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
            {
                new Ingredient { Name = "Beef Sirloin", MinPrice = 180, MaxPrice = 230 },
                new Ingredient { Name = "Cauliflower Rice", MinPrice = 50, MaxPrice = 70 },
                new Ingredient { Name = "Eggs", MinPrice = 30, MaxPrice = 40 },
                new Ingredient { Name = "Garlic", MinPrice = 10, MaxPrice = 15 },
                new Ingredient { Name = "Coconut Aminos", MinPrice = 40, MaxPrice = 50 },
                new Ingredient { Name = "Black Pepper", MinPrice = 10, MaxPrice = 15 },
                new Ingredient { Name = "Olive Oil", MinPrice = 30, MaxPrice = 40 }
            }));
            command.Parameters.AddWithValue("@Instructions", "1. Marinate beef sirloin in coconut aminos, garlic, and black pepper for at least 30 minutes.\n\n2. Cook cauliflower rice in a large skillet over medium heat until golden.\n\n3. In the same pan, cook eggs until desired consistency. Remove and set aside.\n\n4. Cook marinated beef until browned. Remove and set aside.\n\n5. Serve beef, cauliflower rice, and eggs together, garnished with extra coconut aminos and black pepper.");
            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
            command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
            await command.ExecuteNonQueryAsync();

            // Keto Ginisang Munggo with Chicharon
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Name", "Keto Ginisang Munggo with Chicharon");
            command.Parameters.AddWithValue("@Description", "A creative keto take on the Filipino mung bean stew, using spinach as the base and topped with crispy chicharon. This innovative adaptation maintains the comfort-food appeal while keeping it low-carb.");
            command.Parameters.AddWithValue("@ImageUrl", "munggo.jpg");
            command.Parameters.AddWithValue("@MinPrice", 220.00m);
            command.Parameters.AddWithValue("@MaxPrice", 320.00m);
            command.Parameters.AddWithValue("@DietaryPreference", "Keto");
            command.Parameters.AddWithValue("@Allergens", "");
            command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
            {
                new Ingredient { Name = "Spinach", MinPrice = 40, MaxPrice = 50 },
                new Ingredient { Name = "Chicharon", MinPrice = 50, MaxPrice = 70 },
                new Ingredient { Name = "Pork Belly", MinPrice = 100, MaxPrice = 150 },
                new Ingredient { Name = "Garlic", MinPrice = 10, MaxPrice = 15 },
                new Ingredient { Name = "Onions", MinPrice = 15, MaxPrice = 20 },
                new Ingredient { Name = "Fish Sauce", MinPrice = 15, MaxPrice = 20 },
                new Ingredient { Name = "Black Pepper", MinPrice = 10, MaxPrice = 15 }
            }));
            command.Parameters.AddWithValue("@Instructions", "1. Cook pork belly until crispy and fat is rendered. Remove and set aside. In the same pan, add 2 minced garlic cloves and 1/2 cup chopped onions. Cook until fragrant.\n\n2. Add 1/2 cup mung beans and 1/2 cup water. Simmer for 5 minutes.\n\n3. Add 1/2 cup spinach and 1/2 cup chicharon. Cook for another 5 minutes.\n\n4. Serve hot, garnished with extra pork fat, garlic, onions, and fish sauce.");
            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
            command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
            await command.ExecuteNonQueryAsync();

            // Keto Ube Cheesecake
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Name", "Keto Ube Cheesecake");
            command.Parameters.AddWithValue("@Description", "A Filipino-inspired keto dessert featuring the beloved ube flavor in a rich, creamy cheesecake with an almond flour crust. This purple beauty offers the authentic taste of ube while keeping carbs low using keto-friendly sweeteners.");
            command.Parameters.AddWithValue("@ImageUrl", "ube_cheesecake.jpg");
            command.Parameters.AddWithValue("@MinPrice", 300.00m);
            command.Parameters.AddWithValue("@MaxPrice", 400.00m);
            command.Parameters.AddWithValue("@DietaryPreference", "Keto");
            command.Parameters.AddWithValue("@Allergens", "Dairy, Nuts");
            command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
            {
                new Ingredient { Name = "Cream Cheese", MinPrice = 150, MaxPrice = 200 },
                new Ingredient { Name = "Almond Flour", MinPrice = 80, MaxPrice = 100 },
                new Ingredient { Name = "Ube Extract", MinPrice = 30, MaxPrice = 40 },
                new Ingredient { Name = "Erythritol", MinPrice = 40, MaxPrice = 50 },
                new Ingredient { Name = "Eggs", MinPrice = 30, MaxPrice = 40 },
                new Ingredient { Name = "Butter", MinPrice = 30, MaxPrice = 40 },
                new Ingredient { Name = "Heavy Cream", MinPrice = 40, MaxPrice = 50 }
            }));
            command.Parameters.AddWithValue("@Instructions", "1. Preheat oven to 160°C (325°F). Grease a 9-inch springform pan.\n\n2. Mix almond flour, ube extract, erythritol, and eggs until well combined. Pour into the prepared pan.\n\n3. Bake for 10 minutes. Remove and let cool.\n\n4. In a separate bowl, mix cream cheese, butter, and heavy cream until smooth.\n\n5. Spread over cooled almond mixture. Bake for another 10 minutes. Let cool completely before serving.");
            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
            command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
            await command.ExecuteNonQueryAsync();

            // Keto Leche Flan
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Name", "Keto Leche Flan");
            command.Parameters.AddWithValue("@Description", "A sugar-free version of the classic Filipino custard dessert, made with low-carb sweeteners and rich egg yolks. This keto adaptation maintains the silky smooth texture and caramel flavor of traditional leche flan without the sugar.");
            command.Parameters.AddWithValue("@ImageUrl", "leche_flan.jpg");
            command.Parameters.AddWithValue("@MinPrice", 200.00m);
            command.Parameters.AddWithValue("@MaxPrice", 300.00m);
            command.Parameters.AddWithValue("@DietaryPreference", "Keto");
            command.Parameters.AddWithValue("@Allergens", "Eggs, Dairy");
            command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
            {
                new Ingredient { Name = "Egg Yolks", MinPrice = 100, MaxPrice = 150 },
                new Ingredient { Name = "Heavy Cream", MinPrice = 50, MaxPrice = 70 },
                new Ingredient { Name = "Allulose", MinPrice = 40, MaxPrice = 50 },
                new Ingredient { Name = "Vanilla Extract", MinPrice = 20, MaxPrice = 30 },
                new Ingredient { Name = "Salt", MinPrice = 5, MaxPrice = 10 }
            }));
            command.Parameters.AddWithValue("@Instructions", "1. Preheat oven to 160°C (325°F). Grease a 9-inch springform pan.\n\n2. Whisk egg yolks, heavy cream, allulose, vanilla extract, and salt until well combined. Pour into the prepared pan.\n\n3. Bake for 30 minutes. Let cool completely before serving.");
            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
            command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
            await command.ExecuteNonQueryAsync();
        }
    }
} 