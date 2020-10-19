using PetStore.Models.Models;
using PetStore.Models.PetInfo;

namespace PetStore.Services.Contracts
{
    public interface IFoodDistributorService
    {
        Food BuyFood(PetType type, string productName, string Manufacturer, string expiryDate, int weight, decimal price);
    }
}
