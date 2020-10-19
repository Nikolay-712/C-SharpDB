using PetStore.Models.PetInfo;
using System;

namespace PetStore.Models.Models
{
    public class Pet
    {
        public int Id { get; set; }

        public string Breed { get; set; }

        public DateTime DateOfBirth { get; set; }

        public PetType Type { get; set; }

        public Gender Gender { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int? OrderId { get; set; }

        public Order Order { get; set; }
    }
}
