using Kalikse.Models;
using System.Collections.Generic;

namespace Kalikse.Services
{
    public class StoreService
    {
        private static readonly Dictionary<string, Store> _stores = new()
        {
            {
                "Magnolia", new Store
                {
                    Name = "Magnolia",
                    LogoUrl = "magnolia_logo.png",
                    Branches = new List<string>
                    {
                        "SM North EDSA",
                        "SM Megamall",
                        "SM Mall of Asia",
                        "SM Fairview",
                        "SM Southmall"
                    }
                }
            },
            {
                "Chicken Bounty", new Store
                {
                    Name = "Chicken Bounty",
                    LogoUrl = "chicken_bounty_logo.png",
                    Branches = new List<string>
                    {
                        "Quezon City Branch",
                        "Manila Branch",
                        "Pasig Branch",
                        "Makati Branch",
                        "Taguig Branch"
                    }
                }
            },
            {
                "SM Supermarket", new Store
                {
                    Name = "SM Supermarket",
                    LogoUrl = "sm_supermarket_logo.png",
                    Branches = new List<string>
                    {
                        "SM North EDSA",
                        "SM Megamall",
                        "SM Mall of Asia",
                        "SM Fairview",
                        "SM Southmall"
                    }
                }
            },
            {
                "Robinsons Supermart", new Store
                {
                    Name = "Robinsons Supermart",
                    LogoUrl = "robinsons_supermart_logo.png",
                    Branches = new List<string>
                    {
                        "Robinsons Galleria",
                        "Robinsons Magnolia",
                        "Robinsons Place Manila",
                        "Robinsons Metro East",
                        "Robinsons Novaliches"
                    }
                }
            },
            {
                "Agora Market", new Store
                {
                    Name = "Agora Market",
                    LogoUrl = "agora_market_logo.png",
                    Branches = new List<string>
                    {
                        "Quezon City Main Branch",
                        "Manila Branch",
                        "Pasig Branch",
                        "Marikina Branch",
                        "Cainta Branch"
                    }
                }
            },
            {
                "Fresh Market", new Store
                {
                    Name = "Fresh Market by MyMarket.ph",
                    LogoUrl = "fresh_market_logo.png",
                    Branches = new List<string>
                    {
                        "Quezon City Hub",
                        "Manila Hub",
                        "Pasig Hub",
                        "Makati Hub",
                        "Taguig Hub"
                    }
                }
            }
        };

        public static Store GetStoreForIngredient(string ingredientName)
        {
            // Mock store assignments for specific ingredients
            return ingredientName.ToLower() switch
            {
                "chicken" or "chicken breast" => _stores["Magnolia"],
                "eggs" or "hard-boiled eggs" => _stores["SM Supermarket"],
                "lettuce" or "tomatoes" or "bell peppers" or "onions" or "celery" or "green onions" => _stores["Agora Market"],
                "salmon fillet" or "shrimp" or "fish" => _stores["Fresh Market"],
                "sausage" or "bacon" or "ground beef" => _stores["Robinsons Supermart"],
                "frozen vegetables" or "frozen meat" => _stores["Chicken Bounty"],
                _ => null
            };
        }
    }
} 