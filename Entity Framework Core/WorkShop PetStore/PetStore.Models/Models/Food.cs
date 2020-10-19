using PetStore.Models.PetInfo;
using System;

namespace PetStore.Models.Models
{
    public class Food
    {
        public int Id { get; set; }

        public PetType Type { get; set; }

        public string Manufacturer { get; set; }

        public string FoodName { get; set; }

        public DateTime ExpiryDate { get; set; }

        public int Weight { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int? OrderId { get; set; }

        public Order Order { get; set; }


    }
}
