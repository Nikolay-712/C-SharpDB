using PetStore.Models.InformationAboutModels;
using PetStore.Models.Models;
using PetStore.Models.PetInfo;

namespace PetStore.Services.Contracts
{
    public interface IToyDistributorService
    {
        Toy BuyToy(PetType type,string productName, string manufacturer,decimal price,Material material);
    }
}
