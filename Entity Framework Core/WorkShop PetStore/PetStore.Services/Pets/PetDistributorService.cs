using PetStore.Models.Models;
using PetStore.Models.PetInfo;
using PetStore.Services.Contracts;
using System;

namespace PetStore.Services.Pets
{
    public class PetDistributorService : IPetDistributorService
    {

        public Pet BuyPet(string breed, string dateOfBirth, PetType type, Gender gender, decimal price, string description)
        {

            if (string.IsNullOrEmpty(breed))
            {
                throw new ArgumentException($"The breed of the {type} must be specified");
            }

            if (price < 0)
            {
                throw new ArgumentException("Indicate the correct price!");
            }

            var pet = new Pet()
            {
                Breed = breed,
                DateOfBirth = Convert.ToDateTime(dateOfBirth),
                Type = type,
                Gender = gender,
                Price = price,
                Description = description
            };

            return pet;
        }
    }
}
