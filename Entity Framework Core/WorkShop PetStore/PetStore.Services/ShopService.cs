using PetStore.Models.PetInfo;
using PetStore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;


namespace PetStore.Services
{
    public class ShopService : IShopService
    {
        public void ShowAvailableFoodinStore()
        {
          
        }

        public void ShowAvailableFoodOfTypeinStore(PetType type)
        {
            
            throw new NotImplementedException();
        }

        public void ShowAvailablePetsinStore()
        {
            throw new NotImplementedException();
        }

        public void ShowAvailablePetsOfTypeinStore(PetType type)
        {
            throw new NotImplementedException();
        }

        public void ShowAvailableToysinStore()
        {
            throw new NotImplementedException();
        }

        public void ShowAvailableToysOfTypeinStore(PetType type)
        {
            throw new NotImplementedException();
        }
    }
}
