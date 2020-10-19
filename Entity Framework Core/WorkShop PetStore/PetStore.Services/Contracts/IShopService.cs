using PetStore.Models.PetInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetStore.Services.Contracts
{
    public interface IShopService
    {
        void ShowAvailablePetsinStore();

        void ShowAvailablePetsOfTypeinStore(PetType type);

        void ShowAvailableFoodinStore();

        void ShowAvailableFoodOfTypeinStore(PetType type);

        void ShowAvailableToysinStore();

        void ShowAvailableToysOfTypeinStore(PetType type);



    }
}
