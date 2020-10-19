using PetStore.Models.Models;
using System.Collections.Generic;

namespace PetStore.Services.Contracts
{
    public interface IStoreService
    {
        IEnumerable<Food> LoadFoodInStore();

        IEnumerable<Toy> LoadToyInStore();

        IEnumerable<Pet> LoadPetsInStore();
    }
}
