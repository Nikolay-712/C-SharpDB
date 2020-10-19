using PetStore.Models.InformationAboutModels;
using PetStore.Models.PetInfo;

namespace PetStore.Models.Models
{
    public class Toy
    {
        public int Id { get; set; }

        public PetType Type { get; set; }

        public string Manufacturer { get; set; }


        public string ToyName { get; set; }

        public decimal Price { get; set; }

        public Material Material { get; set; }

        public string Description { get; set; }

        public int? OrderId { get; set; }

        public Order Order { get; set; }
    }
}
