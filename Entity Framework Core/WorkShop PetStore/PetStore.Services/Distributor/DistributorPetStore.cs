using System.Collections.Generic;

namespace PetStore.Services.Distributor
{
    public class DistributorPetStore
    {
        public List<string> CatBreeds;

        public List<string> DogBreeds;

        public List<string> SmallPets;

        public List<string> Birds;

        public List<string> FishType;

        public List<string> ReptilType;

        public List<string> Manufactureres;

        public List<string> PetproductsName;

        public DistributorPetStore()
        {
            LoadAnimalTypes();

            Manufactureres = new List<string>()
            {
                "Nestle Purina Petcare",
                "Diamond Pet Foods",
                "Blue Buffalo",
                "Ainsworth Pet Nutrition",
                "JM Smucker"
            };

            PetproductsName = new List<string>()
            {
               "Pushup",
               "Pet swag",
               "Doggy swag",
               "Foxoup",
               "Feeds",
               "Animal feeds",
               "Foodly",
               "Feeding stuff",
               "Animals stuff"
            };
        }


        private void LoadAnimalTypes()
        {
            this.CatBreeds = new List<string>
            {
                "Birman","Abyssinian","Ragamuffin","American Wirehair","Korat","Norwegian Forest Cat"
            };

            this.DogBreeds = new List<string>
            {
                 "Shar Pei","Miniature Bull Terrier","Carolina Dog","Norwegian Elkhound","Schipperke","Basset Hound"
            };

            this.SmallPets = new List<string>()
            {
                "Hmaster","Guinea pig","Mini Rabbit",
            };

            this.Birds = new List<string>()
            {
                "Parrot","Canary","jackdaw"

            };

            this.FishType = new List<string>()
            {
                 "Guppy","Gold fish","Shark"
            };

            this.ReptilType = new List<string>()
            {
                "Snake","Gecko","Lizard"
            };
        }
    }
}
