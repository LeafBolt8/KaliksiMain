using Microsoft.Data.Sqlite;
using Kalikse.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

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

        public async Task<List<Recipe>> GetFilteredRecipesAsync(decimal maxBudget, string dietaryPreference, List<string> allergens)
        {
            var recipes = new List<Recipe>();
            
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT * FROM Recipes 
                WHERE MaxPrice <= @MaxBudget 
                AND DietaryPreference = @DietaryPreference
                AND Allergens NOT LIKE @AllergenPattern;";

            command.Parameters.AddWithValue("@MaxBudget", maxBudget);
            command.Parameters.AddWithValue("@DietaryPreference", dietaryPreference);
            
            // Create pattern for allergens (e.g., "%peanut%")
            var allergenPattern = string.Join(" OR ", allergens.Select(a => $"Allergens LIKE '%{a}%'"));
            command.Parameters.AddWithValue("@AllergenPattern", allergenPattern);

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