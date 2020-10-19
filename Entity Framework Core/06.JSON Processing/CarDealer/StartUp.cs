using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var carDealerContex = new CarDealerContext())
            {
                //carDealerContex.Database.EnsureDeleted();
                //carDealerContex.Database.EnsureCreated();


                //09. Import Suppliers
                //var suppliersJson = File.ReadAllText("./../../../Datasets/suppliers.json");
                //Console.WriteLine(ImportSuppliers(carDealerContex, suppliersJson));

                //10. Import Parts 
                //var partsJson = File.ReadAllText("./../../../Datasets/parts.json");
                //Console.WriteLine(ImportParts(carDealerContex, partsJson));

                //11. Import Cars
                //var carsJson = File.ReadAllText("./../../../Datasets/cars.json");
                //Console.WriteLine(ImportCars(carDealerContex, carsJson));


                ///12.Import Customers
                //var customersJson = File.ReadAllText("./../../../Datasets/customers.json");
                //Console.WriteLine(ImportCustomers(carDealerContex, customersJson));

                //13. Import Sales
                //var salesJson = File.ReadAllText("./../../../Datasets/sales.json");
                //Console.WriteLine(ImportSales(carDealerContex, salesJson));

                //14. Export Ordered Customers 
                //Console.WriteLine(GetOrderedCustomers(carDealerContex));

                //15. Export Cars From Make Toyota 
                //Console.WriteLine(GetCarsFromMakeToyota(carDealerContex));

                //16. Export Local Suppliers 
                //Console.WriteLine(GetLocalSuppliers(carDealerContex));

                //17. Export Cars With Their List Of Parts 
                //Console.WriteLine(GetCarsWithTheirListOfParts(carDealerContex));

                // 18. Export Total Sales By Customer
                //Console.WriteLine(GetTotalSalesByCustomer(carDealerContex));

                //19. Export Sales With Applied Discount 
                //Console.WriteLine(GetSalesWithAppliedDiscount(carDealerContex));



            }
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var Suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson).ToList();

            context.Suppliers.AddRange(Suppliers);
            context.SaveChanges();

            return $"Successfully imported {Suppliers.Count}.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var validSupplier = context.Suppliers.Select(x => x.Id).ToList();

            var Parts = JsonConvert.DeserializeObject<Part[]>(inputJson)
                .Where(x => validSupplier.Contains(x.SupplierId)).ToList();

            context.Parts.AddRange(Parts);
            context.SaveChanges();

            return $"Successfully imported {Parts.Count}.";
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var validParts = context.Parts.Select(x => x.Id);
            var Cars = JsonConvert.DeserializeObject<CarInfoDTO[]>(inputJson).ToList();

            var cars = new List<Car>();
            var partsCar = new List<PartCar>();

            foreach (var car in Cars)
            {
                Car currentCar = new Car
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance,
                };


                cars.Add(currentCar);

                var parttsInBase = car.PartsId.Distinct().Where(x => validParts.Contains(x));

                foreach (var partId in parttsInBase)
                {
                    PartCar partCar = new PartCar
                    {
                        Car = currentCar,
                        PartId = partId
                    };

                    partsCar.Add(partCar);
                }
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();

            context.PartCars.AddRange(partsCar);
            context.SaveChanges();


            return $"Successfully imported {Cars.Count}.";
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var Customers = JsonConvert.DeserializeObject<Customer[]>(inputJson).ToList();

            context.AddRange(Customers);
            context.SaveChanges();


            return $"Successfully imported {Customers.Count}.";
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var Sales = JsonConvert.DeserializeObject<Sale[]>(inputJson).ToList();

            context.Sales.AddRange(Sales);
            context.SaveChanges();

            return $"Successfully imported {Sales.Count}.";
        }

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var Customers = context.Customers
                .Select(x => new
                {
                    x.Name,
                    BirthDate = x.BirthDate
                    .ToString("dd/MM/yyyy"),
                    x.IsYoungDriver
                })
                .OrderBy(x => x.BirthDate)
                .ThenBy(x => x.IsYoungDriver)
                .ToList();

            var customersInfo = JsonConvert
                .SerializeObject(Customers, Formatting.Indented);

            return customersInfo;
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var carsFromMakeToyota = context.Cars
                .Where(x => x.Make.ToLower() == "toyota")
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .Select(x => new
                {
                    Id = x.Id,
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance =
                    x.TravelledDistance
                })
                .ToList();

            var carsInfo = JsonConvert
                .SerializeObject(carsFromMakeToyota, Formatting.Indented);

            return carsInfo;
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var localSuppliers = context.Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count
                }
                )
                .ToList();

            var suppliersInfo = JsonConvert
                .SerializeObject(localSuppliers, Formatting.Indented);

            return suppliersInfo;
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsWithParts = context.Cars
                .Select(x => new
                {
                    car = new
                    {
                        x.Make,
                        x.Model,
                        x.TravelledDistance
                    },
                    parts = x.PartCars
                    .Select(p => new
                    {
                        p.Part.Name,
                        Price = $"{p.Part.Price:F2}"
                    })
                });


            var carInfo = JsonConvert.SerializeObject(carsWithParts, Formatting.Indented);


            return carInfo;

        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var salesByCustomer = context.Customers
                .Where(x => x.Sales.Count > 0)
                .Select(x => new
                {
                    fullName = x.Name,
                    boughtCars = x.Sales.Count,
                    spentMoney = x.Sales
                    .Select(s => s.Car.PartCars
                    .Select(p => p.Part.Price).Sum()
                    ).Sum()
                }
                )
                .OrderByDescending(x => x.spentMoney)
                .ToList();

            var CustomerInfo = JsonConvert.SerializeObject(salesByCustomer, Formatting.Indented);

            return CustomerInfo;
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var appliedDiscount = context.Sales.Take(10)
                .Select(x => new
                {
                    car = new
                    {
                        Make = x.Car.Make,
                        Model = x.Car.Model,
                        TravelledDistance = x.Car.TravelledDistance
                    },
                    customerName = x.Customer.Name,
                    Discount = x.Discount.ToString("f2"),
                    price = x.Car.PartCars.Select(p => p.Part.Price).Sum().ToString("f2"),
                    priceWithDiscount = ((x.Car.PartCars.Sum(pc => pc.Part.Price)) * (1 - x.Discount * 0.01m)).ToString("f2")
                })
                .ToList();



            var appliedDiscountInfo = JsonConvert.SerializeObject(appliedDiscount, Formatting.Indented);


            var ss = appliedDiscountInfo.Length;

            return appliedDiscountInfo;

        }


    }
}