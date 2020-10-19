using PetStore.Models.Models;
using PetStore.Models.PetInfo;
using PetStore.Services.Contracts;
using System;


namespace PetStore.Services.Products
{
    public class FoodDistributorService : IFoodDistributorService
    {
        private const string description = "{0} food with price ${1}";

        public Food BuyFood(PetType type, string productName, string manufacturer, string expiryDate, int weight, decimal price)
        {

            if (string.IsNullOrWhiteSpace(productName))
            {
                throw new ArgumentException("Specify product name!");
            }

            if (price < 0)
            {
                throw new ArgumentException("Indicate the correct price of the product!");
            }

            var food = new Food()
            {
                Type = type,
                FoodName = productName,
                Manufacturer = manufacturer,
                ExpiryDate = Convert.ToDateTime(expiryDate),
                Weight = weight,
                Price = price,
                Description = string.Format(description, type, price)
            };

            return food;
        }
    }
}
