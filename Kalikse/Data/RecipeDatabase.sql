-- Create tables for recipe database
CREATE TABLE Recipes (
    RecipeId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Description TEXT,
    ImagePath TEXT,
    PriceRangeMin DECIMAL(10,2) NOT NULL,
    PriceRangeMax DECIMAL(10,2) NOT NULL,
    CreatedBy TEXT,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Ingredients (
    IngredientId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    IsAllergen BOOLEAN DEFAULT 0
);

CREATE TABLE RecipeIngredients (
    RecipeId INTEGER,
    IngredientId INTEGER,
    Quantity TEXT,
    Unit TEXT,
    PRIMARY KEY (RecipeId, IngredientId),
    FOREIGN KEY (RecipeId) REFERENCES Recipes(RecipeId),
    FOREIGN KEY (IngredientId) REFERENCES Ingredients(IngredientId)
);

CREATE TABLE DietaryPreferences (
    PreferenceId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL
);

CREATE TABLE RecipeDietaryPreferences (
    RecipeId INTEGER,
    PreferenceId INTEGER,
    PRIMARY KEY (RecipeId, PreferenceId),
    FOREIGN KEY (RecipeId) REFERENCES Recipes(RecipeId),
    FOREIGN KEY (PreferenceId) REFERENCES DietaryPreferences(PreferenceId)
);

-- Insert initial dietary preferences
INSERT INTO DietaryPreferences (Name) VALUES 
('None'),
('Keto'),
('Vegetarian'),
('Vegan'),
('Low Carb'),
('High Protein');

-- Insert some common Filipino ingredients
INSERT INTO Ingredients (Name, IsAllergen) VALUES
('Peanuts', 1),
('Shrimp', 1),
('Crab', 1),
('Milk', 1),
('Wheat', 1),
('Rice', 0),
('Garlic', 0),
('Onion', 0),
('Ginger', 0),
('Soy Sauce', 0),
('Vinegar', 0),
('Pork', 0),
('Chicken', 0),
('Beef', 0),
('Fish', 0),
('Eggs', 0),
('Vegetables', 0),
('Coconut Milk', 0);