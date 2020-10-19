using PetStore.Models.InformationAboutModels;
using PetStore.Models.Models;
using PetStore.Models.PetInfo;
using PetStore.Services.Contracts;
using PetStore.Services.Distributor;
using PetStore.Services.Pets;
using PetStore.Services.Products;
using PetStore.Services.Toys;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PetStore.Services
{
    public class StoreSevice : IStoreService
    {
        private readonly List<Pet> pets;
        private readonly List<Toy> toys;
        private readonly List<Food> food;

        public StoreSevice()
        {
            this.PetDistributor = new PetDistributorService();
            this.FoodDistributor = new FoodDistributorService();
            this.ToyDistributor = new ToyDistributorService();
            this.Distributor = new DistributorPetStore();

            this.pets = new List<Pet>();
            this.toys = new List<Toy>();
            this.food = new List<Food>();

            this.Random = new Random();
        }

      
        private DistributorPetStore Distributor { get; set; }

        private FoodDistributorService FoodDistributor { get; set; }

        private ToyDistributorService ToyDistributor { get; set; }

        private PetDistributorService PetDistributor { get; set; }

        private Random Random { get; set; }


        public IEnumerable<Food> LoadFoodInStore()
        {
            for (int i = 0; i < 100; i++)
            {
                var foodType = Enum.GetValues(typeof(PetType)).OfType<PetType>().OrderBy(e => Guid.NewGuid()).FirstOrDefault();
                var productNameIndex = Random.Next(0, this.Distributor.PetproductsName.Count);
                var manufacturerNameIndex = Random.Next(0, this.Distributor.Manufactureres.Count);
                var expiryDate = DateTime.UtcNow.AddMonths(manufacturerNameIndex).ToString("dd.MM.yyyy");
                var weight = 300;
                var price = 7 + productNameIndex * 2;

                var food = this.FoodDistributor
                     .BuyFood
                     (
                         foodType,
                         Distributor.PetproductsName[productNameIndex],
                         Distributor.Manufactureres[manufacturerNameIndex],
                         expiryDate,
                         weight,
                         price
                     );

                this.food.Add(food);
            }

            return this.food;
           

        }


        public IEnumerable<Pet> LoadPetsInStore()
        {
            for (int i = 0; i < 100; i++)
            {
                var randomIndex = this.Random.Next(0, 10);
                var petType = Enum.GetValues(typeof(PetType)).OfType<PetType>().OrderBy(e => Guid.NewGuid()).FirstOrDefault();
                var breedIndex = 0;
                string breed = CheckPetType(petType, breedIndex);
                var dateOfBirth = DateTime.UtcNow.AddMonths(randomIndex).ToString("dd.MM.yyyy");
                var price = Convert.ToDecimal(130 / 0.5 + randomIndex);

                var gender = Gender.Male;

                if (breedIndex % 2 == 0)
                {
                    gender = Gender.Female;
                }

                var pet = this.PetDistributor
                    .BuyPet
                    (
                        breed,
                        dateOfBirth,
                        petType,
                        gender,
                        price,
                        $"Animla of type {petType}"
                    );

                this.pets.Add(pet);

            }

            return this.pets;

          
        }


        public IEnumerable<Toy> LoadToyInStore()
        {
            for (int i = 0; i < 100; i++)
            {
                var toyType = Enum.GetValues(typeof(PetType)).OfType<PetType>().OrderBy(e => Guid.NewGuid()).FirstOrDefault();
                var productNameIndex = Random.Next(0, this.Distributor.PetproductsName.Count);
                var manufacturerNameIndex = Random.Next(0, this.Distributor.Manufactureres.Count);
                var price = 7 / (productNameIndex + 1) * 2;
                var material = Enum.GetValues(typeof(Material)).OfType<Material>().OrderBy(e => Guid.NewGuid()).FirstOrDefault();

                var toy = this.ToyDistributor
                    .BuyToy
                    (
                        toyType,
                        this.Distributor.PetproductsName[productNameIndex],
                        this.Distributor.Manufactureres[manufacturerNameIndex],
                        price,
                        material
                    );

                this.toys.Add(toy);
            }

            return this.toys;
        }

        private string CheckPetType(PetType petType, int breedIndex)
        {
            var breed = string.Empty;

            switch (petType)
            {
                case PetType.Cat:
                    breedIndex = Random.Next(0, this.Distributor.CatBreeds.Count);
                    breed = this.Distributor.CatBreeds[breedIndex];
                    break;
                case PetType.Dog:
                    breedIndex = Random.Next(0, this.Distributor.DogBreeds.Count);
                    breed = this.Distributor.DogBreeds[breedIndex];
                    break;
                case PetType.SmallPet:
                    breedIndex = Random.Next(0, this.Distributor.SmallPets.Count);
                    breed = this.Distributor.SmallPets[breedIndex];
                    break;
                case PetType.Bird:
                    breedIndex = Random.Next(0, this.Distributor.Birds.Count);
                    breed = this.Distributor.Birds[breedIndex];
                    break;
                case PetType.Fish:
                    breedIndex = Random.Next(0, this.Distributor.FishType.Count);
                    breed = this.Distributor.FishType[breedIndex];
                    break;
                case PetType.Reptil:
                    breedIndex = Random.Next(0, this.Distributor.ReptilType.Count);
                    breed = this.Distributor.ReptilType[breedIndex];
                    break;
                default:
                    break;
            }

            return breed;
        }

    }
}
