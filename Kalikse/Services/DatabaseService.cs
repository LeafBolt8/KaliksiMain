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

                    // Keto Bulletproof Coffee
                    command.Parameters.AddWithValue("@Name", "Keto Bulletproof Coffee");
                    command.Parameters.AddWithValue("@Description", "Experience a creamy, energy-boosting coffee that's perfect for starting your day on a ketogenic diet. This industry-favorite beverage combines high-quality coffee with healthy fats to promote mental clarity and sustained energy, all while keeping you in ketosis.");
                    command.Parameters.AddWithValue("@ImageUrl", "bulletproof_coffee.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 100.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 150.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "Dairy");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Coffee", MinPrice = 40, MaxPrice = 45 },
                        new Ingredient { Name = "Butter", MinPrice = 20, MaxPrice = 25 },
                        new Ingredient { Name = "MCT Oil", MinPrice = 30, MaxPrice = 35 },
                        new Ingredient { Name = "Heavy Cream", MinPrice = 15, MaxPrice = 20 },
                        new Ingredient { Name = "Cinnamon", MinPrice = 5, MaxPrice = 8 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Brew 1 cup (240ml) of your favorite high-quality coffee using your preferred method (drip, French press, etc.).\n2. While the coffee is hot, add 1 tablespoon of unsalted, grass-fed butter and 1 tablespoon of MCT oil to the cup.\n3. Pour in 2 tablespoons of heavy cream for extra richness.\n4. Use a blender or milk frother to blend the mixture for 20–30 seconds, until it becomes frothy and fully emulsified.\n5. Pour into a mug, sprinkle with a pinch of ground cinnamon, and serve immediately.\nTip: For best results, blend rather than stir to achieve a creamy, latte-like texture.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Avocado Egg Salad
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Avocado Egg Salad");
                    command.Parameters.AddWithValue("@Description", "A creamy, protein-packed salad that's both satisfying and nutritious. This keto-friendly egg salad uses ripe avocado for a silky texture and healthy fats, making it a perfect lunch or snack for anyone following a low-carb lifestyle.");
                    command.Parameters.AddWithValue("@ImageUrl", "avocado_egg_salad.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 150.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 200.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "Eggs");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Hard-boiled Eggs", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Avocado", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Mayonnaise", MinPrice = 50, MaxPrice = 75 },
                        new Ingredient { Name = "Dijon Mustard", MinPrice = 10, MaxPrice = 20 },
                        new Ingredient { Name = "Celery", MinPrice = 10, MaxPrice = 20 },
                        new Ingredient { Name = "Green Onions", MinPrice = 10, MaxPrice = 20 },
                        new Ingredient { Name = "Salt", MinPrice = 5, MaxPrice = 10 },
                        new Ingredient { Name = "Pepper", MinPrice = 5, MaxPrice = 10 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Hard-boil 4 large eggs: Place eggs in a saucepan, cover with water, bring to a boil, then simmer for 10 minutes. Cool in ice water, then peel and chop.\n2. In a medium bowl, mash 1 ripe avocado until smooth.\n3. Add the chopped eggs, 2 tablespoons mayonnaise, 1 teaspoon Dijon mustard, 1 finely chopped celery stalk, and 2 sliced green onions.\n4. Season with 1/4 teaspoon salt and 1/8 teaspoon black pepper. Mix gently until well combined.\n5. Serve chilled on a bed of lettuce or as a filling for lettuce wraps.\nTip: For extra flavor, add a squeeze of lemon juice and a sprinkle of paprika.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Cauliflower Rice Stir-Fry
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Cauliflower Rice Stir-Fry");
                    command.Parameters.AddWithValue("@Description", "Enjoy a low-carb twist on classic fried rice. This stir-fry uses riced cauliflower as a nutritious base, combined with crispy bacon, eggs, and fresh vegetables for a quick, satisfying meal that fits perfectly into a ketogenic diet.");
                    command.Parameters.AddWithValue("@ImageUrl", "cauliflower_stirfry.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 200.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 300.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "Eggs, Soy");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Cauliflower Rice", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Eggs", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Bacon", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Green Onions", MinPrice = 10, MaxPrice = 20 },
                        new Ingredient { Name = "Soy Sauce", MinPrice = 10, MaxPrice = 20 },
                        new Ingredient { Name = "Garlic", MinPrice = 5, MaxPrice = 10 },
                        new Ingredient { Name = "Ginger", MinPrice = 5, MaxPrice = 10 },
                        new Ingredient { Name = "Sesame Oil", MinPrice = 10, MaxPrice = 20 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Heat a large skillet or wok over medium-high heat. Add 3 strips of chopped bacon and cook until crispy. Remove and set aside, leaving the fat in the pan.\n2. Add 2 beaten eggs to the pan and scramble until just set. Remove and set aside.\n3. Add 2 cups of riced cauliflower, 2 chopped green onions, 1 minced garlic clove, and 1 teaspoon grated ginger to the pan. Stir-fry for 3–4 minutes until the cauliflower is tender.\n4. Return the bacon and eggs to the pan. Add 1 tablespoon soy sauce (or coconut aminos for soy-free), and 1 teaspoon sesame oil. Stir well to combine.\n5. Cook for another 2 minutes, then serve hot, garnished with extra green onions.\nTip: Add a handful of chopped vegetables like bell peppers or snap peas for extra color and nutrition.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Fat Bombs
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Fat Bombs");
                    command.Parameters.AddWithValue("@Description", "These rich, bite-sized snacks are designed to provide a quick source of healthy fats and energy for keto dieters. Perfect for curbing cravings, these fat bombs combine coconut oil, cream cheese, and cocoa for a decadent, guilt-free treat.");
                    command.Parameters.AddWithValue("@ImageUrl", "fat_bombs.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 100.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 150.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "Dairy, Nuts");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Coconut Oil", MinPrice = 40, MaxPrice = 45 },
                        new Ingredient { Name = "Cream Cheese", MinPrice = 35, MaxPrice = 40 },
                        new Ingredient { Name = "Cocoa Powder", MinPrice = 25, MaxPrice = 30 },
                        new Ingredient { Name = "Stevia", MinPrice = 15, MaxPrice = 20 },
                        new Ingredient { Name = "Vanilla Extract", MinPrice = 20, MaxPrice = 25 },
                        new Ingredient { Name = "Almonds", MinPrice = 30, MaxPrice = 35 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. In a mixing bowl, combine 1/2 cup softened cream cheese and 1/4 cup coconut oil until smooth.\n2. Add 2 tablespoons unsweetened cocoa powder, 1 tablespoon powdered erythritol or stevia, and 1/2 teaspoon vanilla extract. Mix until fully incorporated.\n3. Fold in 2 tablespoons finely chopped almonds.\n4. Using a small scoop or spoon, form the mixture into 1-inch balls and place on a parchment-lined tray.\n5. Freeze for at least 30 minutes, or until firm. Store in an airtight container in the refrigerator for up to 1 week.\nTip: Roll the fat bombs in extra chopped nuts or shredded coconut for added texture.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Zucchini Lasagna
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Zucchini Lasagna");
                    command.Parameters.AddWithValue("@Description", "A delicious, low-carb alternative to traditional lasagna, this dish uses thinly sliced zucchini in place of pasta. Layered with a rich meat sauce and a blend of cheeses, it's a hearty, comforting meal that's perfect for family dinners.");
                    command.Parameters.AddWithValue("@ImageUrl", "zucchini_lasagna.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 300.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 400.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "Dairy");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Zucchini", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Ground Beef", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Marinara Sauce", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Ricotta Cheese", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Mozzarella", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Parmesan", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Italian Seasoning", MinPrice = 10, MaxPrice = 20 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Preheat oven to 190°C (375°F). Slice 2 large zucchinis lengthwise into thin strips using a mandoline or sharp knife. Lay slices on paper towels and sprinkle with salt to draw out moisture; let sit for 15 minutes, then pat dry.\n2. In a skillet, cook 300g ground beef with 1/2 cup chopped onions until browned. Drain excess fat, then add 1 cup marinara sauce and 1 teaspoon Italian seasoning. Simmer for 5 minutes.\n3. In a bowl, mix 1 cup ricotta cheese, 1 cup shredded mozzarella, and 1/4 cup grated Parmesan.\n4. In a baking dish, layer zucchini slices, meat sauce, and cheese mixture. Repeat layers, finishing with cheese on top.\n5. Bake for 40–45 minutes, until bubbly and golden. Let rest for 10 minutes before slicing and serving.\nTip: For firmer lasagna, grill or roast zucchini slices before assembling.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Chicken Alfredo
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Chicken Alfredo");
                    command.Parameters.AddWithValue("@Description", "This creamy, indulgent dish features tender chicken breast and spiralized zucchini noodles tossed in a rich Alfredo sauce. It's a restaurant-quality meal that's low in carbs and high in flavor, perfect for a special dinner.");
                    command.Parameters.AddWithValue("@ImageUrl", "chicken_alfredo.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 250.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 350.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "Dairy");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Chicken Breast", MinPrice = 180, MaxPrice = 200 },
                        new Ingredient { Name = "Zucchini", MinPrice = 25, MaxPrice = 30 },
                        new Ingredient { Name = "Heavy Cream", MinPrice = 35, MaxPrice = 40 },
                        new Ingredient { Name = "Parmesan", MinPrice = 45, MaxPrice = 50 },
                        new Ingredient { Name = "Garlic", MinPrice = 5, MaxPrice = 8 },
                        new Ingredient { Name = "Butter", MinPrice = 15, MaxPrice = 20 },
                        new Ingredient { Name = "Italian Seasoning", MinPrice = 10, MaxPrice = 15 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Season 2 chicken breasts with salt, pepper, and Italian seasoning. In a skillet over medium heat, melt 1 tablespoon butter and cook chicken until golden and cooked through (about 6–7 minutes per side). Remove and slice.\n2. In the same pan, add 2 minced garlic cloves and sauté for 1 minute. Pour in 1 cup heavy cream and bring to a gentle simmer.\n3. Stir in 1/2 cup grated Parmesan cheese and cook until the sauce thickens, about 3–4 minutes.\n4. Add 2 cups spiralized zucchini noodles and toss to coat in the sauce. Cook for 2–3 minutes until just tender.\n5. Return sliced chicken to the pan, toss to combine, and serve immediately, garnished with extra Parmesan and fresh parsley.\nTip: Do not overcook zucchini noodles to prevent them from becoming soggy.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Taco Bowl
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Taco Bowl");
                    command.Parameters.AddWithValue("@Description", "A vibrant, Mexican-inspired bowl packed with seasoned ground beef, fresh vegetables, and creamy toppings. This keto taco bowl delivers all the flavors of a classic taco without the carbs, making it a satisfying and customizable meal.");
                    command.Parameters.AddWithValue("@ImageUrl", "taco_bowl.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 200.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 300.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "Dairy");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Ground Beef", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Lettuce", MinPrice = 10, MaxPrice = 20 },
                        new Ingredient { Name = "Avocado", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Cheese", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Sour Cream", MinPrice = 50, MaxPrice = 75 },
                        new Ingredient { Name = "Tomatoes", MinPrice = 50, MaxPrice = 75 },
                        new Ingredient { Name = "Taco Seasoning", MinPrice = 10, MaxPrice = 20 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. In a skillet over medium heat, cook 250g ground beef with 1 tablespoon taco seasoning until browned and fragrant. Drain excess fat.\n2. Prepare the base: In a large bowl, layer 2 cups shredded lettuce.\n3. Top with cooked beef, 1 diced avocado, 1/2 cup shredded cheese, 1/4 cup sour cream, and 1/2 cup diced tomatoes.\n4. Garnish with chopped cilantro and a squeeze of lime juice.\n5. Serve immediately, allowing each person to mix their bowl as desired.\nTip: Add sliced jalapeños or hot sauce for extra heat.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Chocolate Mousse
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Chocolate Mousse");
                    command.Parameters.AddWithValue("@Description", "A decadent, silky-smooth dessert that's rich in chocolate flavor but low in carbs. This keto chocolate mousse is quick to prepare and perfect for satisfying your sweet tooth while staying on track with your diet.");
                    command.Parameters.AddWithValue("@ImageUrl", "chocolate_mousse.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 150.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 200.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "Dairy");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Heavy Cream", MinPrice = 15, MaxPrice = 20 },
                        new Ingredient { Name = "Dark Chocolate", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Erythritol", MinPrice = 10, MaxPrice = 20 },
                        new Ingredient { Name = "Vanilla Extract", MinPrice = 20, MaxPrice = 25 },
                        new Ingredient { Name = "Salt", MinPrice = 5, MaxPrice = 10 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Melt 100g dark chocolate (85% cocoa or higher) in a heatproof bowl over simmering water or in the microwave. Let cool slightly.\n2. In a separate bowl, whip 1 cup heavy cream with 2 tablespoons powdered erythritol and 1 teaspoon vanilla extract until soft peaks form.\n3. Gently fold the melted chocolate into the whipped cream until fully combined and smooth.\n4. Spoon the mousse into serving glasses and chill for at least 2 hours before serving.\n5. Garnish with a pinch of sea salt or a few fresh berries if desired.\nTip: For extra fluffiness, whip the cream until stiff peaks form before folding in the chocolate.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Salmon with Avocado Salsa
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Salmon with Avocado Salsa");
                    command.Parameters.AddWithValue("@Description", "A fresh, nutrient-rich dish featuring perfectly baked salmon fillets topped with a zesty avocado salsa. This meal is packed with healthy fats and omega-3s, making it ideal for a wholesome keto dinner.");
                    command.Parameters.AddWithValue("@ImageUrl", "salmon_salsa.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 300.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 400.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Salmon Fillet", MinPrice = 200, MaxPrice = 250 },
                        new Ingredient { Name = "Avocado", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Lime", MinPrice = 10, MaxPrice = 20 },
                        new Ingredient { Name = "Cilantro", MinPrice = 10, MaxPrice = 20 },
                        new Ingredient { Name = "Red Onion", MinPrice = 10, MaxPrice = 20 },
                        new Ingredient { Name = "Olive Oil", MinPrice = 10, MaxPrice = 20 },
                        new Ingredient { Name = "Salt", MinPrice = 5, MaxPrice = 10 },
                        new Ingredient { Name = "Pepper", MinPrice = 5, MaxPrice = 10 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Preheat oven to 200°C (400°F). Place 2 salmon fillets on a baking sheet lined with parchment paper. Drizzle with 1 tablespoon olive oil and season with salt and pepper.\n2. Bake for 12–15 minutes, or until the salmon flakes easily with a fork.\n3. Meanwhile, prepare the salsa: In a bowl, combine 1 diced avocado, 2 tablespoons chopped red onion, 1 tablespoon chopped cilantro, and juice of 1 lime. Season with salt and pepper to taste.\n4. Remove salmon from the oven and top each fillet with a generous spoonful of avocado salsa.\n5. Serve immediately, garnished with extra cilantro and lime wedges.\nTip: For added flavor, sprinkle the salmon with smoked paprika before baking.");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
                    command.ExecuteNonQuery();

                    // Keto Breakfast Casserole
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", "Keto Breakfast Casserole");
                    command.Parameters.AddWithValue("@Description", "Start your day with this hearty, make-ahead breakfast casserole loaded with eggs, sausage, cheese, and vegetables. It's perfect for meal prep and will keep you full and energized all morning.");
                    command.Parameters.AddWithValue("@ImageUrl", "breakfast_casserole.jpg");
                    command.Parameters.AddWithValue("@MinPrice", 250.00m);
                    command.Parameters.AddWithValue("@MaxPrice", 350.00m);
                    command.Parameters.AddWithValue("@DietaryPreference", "Keto");
                    command.Parameters.AddWithValue("@Allergens", "Eggs, Dairy");
                    command.Parameters.AddWithValue("@Ingredients", JsonSerializer.Serialize(new List<Ingredient>
                    {
                        new Ingredient { Name = "Eggs", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Sausage", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Cheese", MinPrice = 100, MaxPrice = 150 },
                        new Ingredient { Name = "Bell Peppers", MinPrice = 10, MaxPrice = 20 },
                        new Ingredient { Name = "Onions", MinPrice = 10, MaxPrice = 20 },
                        new Ingredient { Name = "Heavy Cream", MinPrice = 15, MaxPrice = 20 },
                        new Ingredient { Name = "Salt", MinPrice = 5, MaxPrice = 10 },
                        new Ingredient { Name = "Pepper", MinPrice = 5, MaxPrice = 10 }
                    }));
                    command.Parameters.AddWithValue("@Instructions", "1. Preheat oven to 175°C (350°F). Grease a 9x13-inch baking dish.\n2. In a skillet, cook 250g breakfast sausage with 1/2 cup chopped onions and 1/2 cup diced bell peppers until sausage is browned and vegetables are tender. Drain excess fat.\n3. In a large bowl, whisk together 8 eggs, 1/2 cup heavy cream, 1 cup shredded cheese, 1/2 teaspoon salt, and 1/4 teaspoon black pepper.\n4. Spread the sausage and vegetable mixture evenly in the baking dish. Pour the egg mixture over the top.\n5. Bake for 40–45 minutes, or until the casserole is set and golden brown. Let rest for 10 minutes before slicing and serving.\nTip: Add spinach or mushrooms for extra nutrition and flavor.");
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
            command.Parameters.AddWithValue("@Instructions", "1. Season chicken thighs with salt and pepper. In a large skillet over medium heat, heat 2 tablespoons olive oil. Add chicken, skin-side down, and cook until golden and cooked through (about 6–7 minutes per side). Remove and set aside.\n2. In the same skillet, add 2 minced garlic cloves and cook until fragrant. Add 1/4 cup coconut aminos, 1/4 cup white vinegar, 1 bay leaf, and 1 teaspoon black peppercorns. Simmer for 5 minutes.\n3. Return chicken to the skillet, skin-side up, and simmer for another 5 minutes. Serve hot, garnished with extra garlic and bay leaves.");
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
            command.Parameters.AddWithValue("@Instructions", "1. Cook pork belly until crispy and fat is rendered. Remove and set aside. In the same pan, add 2 minced garlic cloves and cook until fragrant.\n2. Add 1 cup cauliflower rice, 1 diced radish, 1/2 cup spinach, and 1/2 cup diced tomatoes. Cook for 5 minutes.\n3. Add 1/2 cup fish sauce, 1/2 cup coconut aminos, and 1/2 cup water. Simmer for 5 minutes.\n4. Add 1/2 cup cooked pork belly and 1/2 cup water. Simmer for another 5 minutes. Serve hot, garnished with extra garlic and fish sauce.");
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
            command.Parameters.AddWithValue("@Instructions", "1. Chop pork belly and chicken liver. In a large skillet over medium heat, cook until crispy. Remove and set aside.\n2. In the same pan, add 2 minced garlic cloves and cook until fragrant. Add 1/2 cup chopped onions and 1/2 cup calamansi juice. Cook for 5 minutes.\n3. Add 1/2 cup chicken liver and 1/2 cup pork belly. Cook for another 5 minutes.\n4. Serve hot, garnished with extra garlic, onions, and calamansi. Top with 1/4 cup mayonnaise.");
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
            command.Parameters.AddWithValue("@Instructions", "1. Chop pork belly and cook until crispy. Remove and set aside. In the same pan, add 2 minced garlic cloves and cook until fragrant.\n2. Add 1/2 cup chopped onions and 1/2 cup coconut cream. Cook for 5 minutes.\n3. Add 1/2 cup shrimp paste and 1/2 cup green chilies. Cook for another 5 minutes.\n4. Serve hot, garnished with extra garlic, onions, and coconut cream.");
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
            command.Parameters.AddWithValue("@Instructions", "1. Cook chicken pieces until browned. Remove and set aside. In the same pan, add 1/2 cup coconut milk and 1/2 cup water. Simmer for 5 minutes.\n2. Add 1/2 cup spinach and 1/2 cup green chilies. Cook for another 5 minutes.\n3. Return chicken to the pan and cook for another 5 minutes. Serve hot, garnished with extra coconut milk and fish sauce.");
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
            command.Parameters.AddWithValue("@Instructions", "1. Cook chicken pieces until browned. Remove and set aside. In the same pan, add 1/2 cup coconut milk and 1/2 cup water. Simmer for 5 minutes.\n2. Add 1/2 cup chayote and 1/2 cup moringa leaves. Cook for another 5 minutes.\n3. Return chicken to the pan and cook for another 5 minutes. Serve hot, garnished with extra coconut milk and fish sauce.");
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
            command.Parameters.AddWithValue("@Instructions", "1. Marinate beef sirloin in coconut aminos, garlic, and black pepper for at least 30 minutes.\n2. Cook cauliflower rice in a large skillet over medium heat until golden.\n3. In the same pan, cook eggs until desired consistency. Remove and set aside.\n4. Cook marinated beef until browned. Remove and set aside.\n5. Serve beef, cauliflower rice, and eggs together, garnished with extra coconut aminos and black pepper.");
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
            command.Parameters.AddWithValue("@Instructions", "1. Cook pork belly until crispy and fat is rendered. Remove and set aside. In the same pan, add 2 minced garlic cloves and 1/2 cup chopped onions. Cook until fragrant.\n2. Add 1/2 cup mung beans and 1/2 cup water. Simmer for 5 minutes.\n3. Add 1/2 cup spinach and 1/2 cup chicharon. Cook for another 5 minutes.\n4. Serve hot, garnished with extra pork fat, garlic, onions, and fish sauce.");
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
            command.Parameters.AddWithValue("@Instructions", "1. Preheat oven to 160°C (325°F). Grease a 9-inch springform pan.\n2. Mix almond flour, ube extract, erythritol, and eggs until well combined. Pour into the prepared pan.\n3. Bake for 10 minutes. Remove and let cool.\n4. In a separate bowl, mix cream cheese, butter, and heavy cream until smooth.\n5. Spread over cooled almond mixture. Bake for another 10 minutes. Let cool completely before serving.");
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
            command.Parameters.AddWithValue("@Instructions", "1. Preheat oven to 160°C (325°F). Grease a 9-inch springform pan.\n2. Whisk egg yolks, heavy cream, allulose, vanilla extract, and salt until well combined. Pour into the prepared pan.\n3. Bake for 30 minutes. Let cool completely before serving.");
            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("o"));
            command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("o"));
            await command.ExecuteNonQueryAsync();
        }
    }
} 