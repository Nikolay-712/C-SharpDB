namespace CarDealer
{
    using CarDealer.Models;
    using CarDealer.Data;
    using CarDealer.Dtos.Import;

    using System;
    using System.IO;
    using System.Xml.Serialization;
    using AutoMapper;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using System.Text;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using CarDealer.Dtos.Export.CarsWithDistance;
    using System.Xml;
    using CarDealer.Dtos.Export.CarsFromBMW;
    using CarDealer.Dtos.Export.LocalSuppliers;
    using CarDealer.Dtos.Export.CarsWithListOfParts;
    using CarDealer.Dtos.Export.TotalSalesByCustomer;
    using CarDealer.Dtos.Export.GetSalesWithAppliedDiscount;

    public class StartUp
    {
        public static int count = 0;

        public static void Main(string[] args)
        {
            //var path = "./../../../Datasets/{0}.xml";

            Mapper.Initialize(configuration =>
             configuration.AddProfile<CarDealerProfile>());

            using (var carDealerContext = new CarDealerContext())
            {
                //09. Import Suppliers
                //var suppliersXml = File.ReadAllText(string.Format(path, "suppliers"));
                //Console.WriteLine(ImportSuppliers(carDealerContext, suppliersXml));

                //10. Import Parts 
                //var partsXml = File.ReadAllText(string.Format(path, "parts"));
                //Console.WriteLine(ImportParts(carDealerContext, partsXml));

                //11. Import Cars 
                //var carsXml = File.ReadAllText(string.Format(path, "cars"));
                //Console.WriteLine(ImportCars(carDealerContext, carsXml));

                //12. Import Customers
                //var customersXml = File.ReadAllText(string.Format(path, "customers"));
                //Console.WriteLine(ImportCustomers(carDealerContext, customersXml));

                //13. Import Sales 
                //var salseXml = File.ReadAllText(string.Format(path, "sales"));
                //Console.WriteLine(ImportSales(carDealerContext, salseXml));


                //14. Export Cars With Distance 
                //Console.WriteLine(GetCarsWithDistance(carDealerContext));

                //15. Export Cars From Make BMW 
                //Console.WriteLine(GetCarsFromMakeBmw(carDealerContext));

                //16. Export Local Suppliers 
                //Console.WriteLine(GetLocalSuppliers(carDealerContext));

                //17. Export Cars With Their List Of Parts 
                //Console.WriteLine(GetCarsWithTheirListOfParts(carDealerContext));

                //18. Export Total Sales By Customer 
                //Console.WriteLine(GetTotalSalesByCustomer(carDealerContext));

                //19. Export Sales With Applied Discount 
                Console.WriteLine(GetSalesWithAppliedDiscount(carDealerContext));

            }

        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportSupplierDTO[])
                , new XmlRootAttribute("Suppliers"));

            using (var reader = new StringReader(inputXml))
            {
                var suppliers = (ImportSupplierDTO[])serializer.Deserialize(reader);

                var suppliersInfo = Mapper.Map<Supplier[]>(suppliers).ToList();
                count = suppliersInfo.Count();

                context.Suppliers.AddRange(suppliersInfo);
                context.SaveChanges();
            }
            return $"Successfully imported { count}";

        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportPartDTO[])
                , new XmlRootAttribute("Parts"));

            using (var reader = new StringReader(inputXml))
            {
                var parts = (ImportPartDTO[])serializer.Deserialize(reader);

                var partsInfo = Mapper.Map<Part[]>(parts)
                    .Where(x => context.Suppliers
                    .Select(s => s.Id).Contains(x.SupplierId))
                    .ToList();

                count = partsInfo.Count();

                context.Parts.AddRange(partsInfo);
                context.SaveChanges();

            }

            return $"Successfully imported { count}";

        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ImportCarDTO>)
                , new XmlRootAttribute("Cars"));

            var cars = new List<Car>();

            using (var reader = new StringReader(inputXml))
            {
                var carsDTO = (List<ImportCarDTO>)serializer.Deserialize(reader);

                foreach (var car in carsDTO)
                {
                    var currentCar = Mapper.Map<Car>(car);

                    foreach (var partIdInfo in car.Parts.Select(x => x.PartId).Distinct())
                    {
                        var partCar = new PartCar()
                        {
                            Car = currentCar,
                            PartId = partIdInfo

                        };

                        currentCar.PartCars.Add(partCar);
                    }

                    cars.Add(currentCar);
                }

                count = cars.Count();

                context.Cars.AddRange(cars);
                context.SaveChanges();
            }



            return $"Successfully imported { count}";

        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ImportCustomerDto>)
                , new XmlRootAttribute("Customers"));

            using (var reader = new StringReader(inputXml))
            {
                var custumersDto = (List<ImportCustomerDto>)serializer.Deserialize(reader);

                var custumers = Mapper.Map<List<Customer>>(custumersDto);


                count = custumers.Count;

                context.Customers.AddRange(custumers);
                context.SaveChanges();
            }


            return $"Successfully imported {count}";
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ImportSalesDTO>)
                , new XmlRootAttribute("Sales"));

            var validCarsInBase = context.Cars.Select(x => x.Id);

            using (var reader = new StringReader(inputXml))
            {
                var salesDto = (List<ImportSalesDTO>)serializer.Deserialize(reader);

                var sales = Mapper.Map<List<Sale>>(salesDto)
                    .Where(x => validCarsInBase
                    .Contains(x.CarId)).ToList();

                count = sales.Count;

                context.Sales.AddRange(sales);
                context.SaveChanges();
            }


            return $"Successfully imported {count}";

        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();


            var cars = context.Cars
                .Where(x => x.TravelledDistance > 2000000)
                .Select(x =>
                new CarWithDistanceDTO
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                }
                )
                .OrderBy(x => x.Make)
                .ThenBy(x => x.Model)
                .Take(10).ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(CarWithDistanceDTO[])
                , new XmlRootAttribute("cars"));

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            serializer.Serialize(new StringWriter(stringBuilder), cars, namespaces);


            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var cars = context.Cars
                .Where(x => x.Make.ToLower() == "bmw")
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .Select(x =>
                new CarFromMakeDTO
                {
                    Id = x.Id,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                }
                ).ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(CarFromMakeDTO[])
                , new XmlRootAttribute("cars"));

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            serializer.Serialize(new StringWriter(stringBuilder), cars, namespaces);


            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var suppliers = context.Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x =>
                new LocalSuppliersDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count()
                }).ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(LocalSuppliersDTO[])
                ,new XmlRootAttribute("suppliers"));

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            serializer.Serialize(new StringWriter(stringBuilder), suppliers, namespaces);


            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var cars = context.Cars
                .Select(x =>
                new CarInfoDTO
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledlDistance = x.TravelledDistance,
                    Parts = x.PartCars.Select(p => 
                    new CarPartInfoDTO 
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    })
                    .OrderByDescending(p => p.Price)
                    .ToList()



                })
                .OrderByDescending(x => x.TravelledlDistance)
                .ThenBy(x => x.Model)
                .Take(5)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(CarInfoDTO[]), new XmlRootAttribute("cars"));

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            serializer.Serialize(new StringWriter(stringBuilder), cars, namespaces);




            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var custumers = context.Customers
                .Where(x => x.Sales.Count > 0)
                .Select(x => 
                new CustumerInfoDTO 
                {
                    FullName =x.Name,
                    BoughtCars = x.Sales.Count(),
                    SpentMoney = x.Sales
                    .Select(c => c.Car.PartCars
                    .Select(p => p.Part.Price).Sum()).Sum()
                })
                .OrderByDescending(x => x.SpentMoney)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(CustumerInfoDTO[])
                ,new XmlRootAttribute("customers"));

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            serializer.Serialize(new StringWriter(stringBuilder), custumers, namespaces);


            return stringBuilder.ToString().TrimEnd();

        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var info = context.Sales
                .Select(x =>
                new SaleInfoDTO
                {
                    Car = new
                    CarBySaleInfoDTO
                    {
                        Make = x.Car.Make,
                        Model = x.Car.Model,
                        TravelledDistance = 
                        x.Car.TravelledDistance,
                    },

                    Discount = x.Discount,
                    CustumerName = x.Customer.Name,
                    Price = x.Car.PartCars
                    .Select(p => p.Part.Price)
                    .Sum()


                }).ToArray();


            XmlSerializer serializer = new XmlSerializer(typeof(SaleInfoDTO[])
                ,new XmlRootAttribute("sales"));

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            serializer.Serialize(new StringWriter(stringBuilder), info, namespaces);

            return stringBuilder.ToString().TrimEnd();
        }
    }
}