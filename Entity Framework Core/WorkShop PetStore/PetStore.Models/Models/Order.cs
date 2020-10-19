using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace PetStore.Models.Models
{
    public class Order
    {
        private const string description = "The order is accepted on {0} the final amount is ${1}";

        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalSum 
            => this.Foods.Select(x => x.Price).Sum() +
            this.Toys.Select(x => x.Price).Sum() +
            this.Pets.Select(x => x.Price).Sum();

        public int Qunatity { get; set; }

        public string Description => string.Format(description, OrderDate.ToString("dd.MM.yyyy"), TotalSum);

        public int? UserId { get; set; }

        public User User { get; set; }

        public List<Food> Foods { get; set; }

        public List<Toy> Toys { get; set; }

        public List<Pet> Pets { get; set; }

    }
}
