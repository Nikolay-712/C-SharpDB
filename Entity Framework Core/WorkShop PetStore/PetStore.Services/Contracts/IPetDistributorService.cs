using PetStore.Models.Models;
using PetStore.Models.PetInfo;

namespace PetStore.Services.Contracts
{
    public interface IPetDistributorService
    {
        Pet BuyPet(string breed, string dateOfBirth, PetType type, Gender gender, decimal price, string description);
    }
}
