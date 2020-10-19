using PetStore.Models.InformationAboutModels;
using PetStore.Models.Models;
using PetStore.Models.PetInfo;
using PetStore.Services.Contracts;
using System;

namespace PetStore.Services.Toys
{
    public class ToyDistributorService : IToyDistributorService
    {
        private const string description = "{0} toy with price ${1}";

        public Toy BuyToy(PetType type, string productName, string manufacturer, decimal price, Material material)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                throw new ArgumentException("Specify product name!");
            }

            if (price < 0)
            {
                throw new ArgumentException("Indicate the correct price of the product!");
            }

            var toy = new Toy()
            {
                Type = type,
                ToyName = productName,
                Manufacturer = manufacturer,
                Price = price,
                Material = material,
                Description = string.Format(description, type, price)

            };

            return toy;
        }
    }
}
