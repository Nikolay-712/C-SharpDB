using AutoMapper;
using ProductShop.Data;
using ProductShop.Dtos.Import;
using ProductShop.Dtos.Export;
using ProductShop.Models;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Text;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Xml;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(configuration =>
             configuration.AddProfile<ProductShopProfile>());

            string path = "./../../../Datasets/{0}.xml";

            using (var productShopContex = new ProductShopContext())
            {
                // productShopContex.Database.EnsureDeleted();
                // productShopContex.Database.EnsureCreated();


                //01. Import Users 
                //var usersXml = File.ReadAllText("./../../../Datasets/users.xml");
                //Console.WriteLine(ImportUsers(productShopContex, usersXml));

                //02. Import Products
                //var productsXml = File.ReadAllText(string.Format(path, "products"));
                //Console.WriteLine(ImportProducts(productShopContex, productsXml));


                //03. Import Categories
                //var categoriesXml = File.ReadAllText(string.Format(path, "categories"));
                //Console.WriteLine(ImportCategories(productShopContex, categoriesXml));

                //04. Import Categories and Products 
                //var categoryProductXml = File.ReadAllText(string.Format(path, "categories-products"));
                //Console.WriteLine(ImportCategoryProducts(productShopContex,categoryProductXml));

                //05. Export Products In Range 
                //Console.WriteLine(GetProductsInRange(productShopContex));

                //06.Export Sold Products
                //Console.WriteLine(GetSoldProducts(productShopContex));

                //07. Export Categories By Products Count 
                //Console.WriteLine(GetCategoriesByProductsCount(productShopContex));

                //08. Export Users and Products 
                //Console.WriteLine(GetUsersWithProducts(productShopContex));

            }
        }

        public static string ImportUsers(ProductShopContext context, string stringinputXml)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(ImportUserDTO[]),
                new XmlRootAttribute("Users"));

            var usersCount = 0;

            using (var reader = new StringReader(stringinputXml))
            {
                var users = (ImportUserDTO[])serializer.Deserialize(reader);

                var usersInfo = Mapper.Map<User[]>(users).ToList();

                context.Users.AddRange(usersInfo);
                context.SaveChanges();

                usersCount = usersInfo.Count();
            }

            return $"Successfully imported {usersCount}";

        }

        public static string ImportProducts(ProductShopContext context, string stringinputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportProductDTO[])
                , new XmlRootAttribute("Products"));

            var productsCount = 0;

            using (var reader = new StringReader(stringinputXml))
            {
                var products = (ImportProductDTO[])serializer.Deserialize(reader);

                var productsInfo = Mapper.Map<Product[]>(products).ToList();


                context.Products.AddRange(productsInfo);
                context.SaveChanges();

                productsCount = productsInfo.Count();
            }

            return $"Successfully imported { productsCount}";

        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportCategoryDTO[])
                , new XmlRootAttribute("Categories"));

            var categoriesCount = 0;

            using (var reader = new StringReader(inputXml))
            {
                var categories = (ImportCategoryDTO[])serializer.Deserialize(reader);

                var categoryInfo = Mapper.Map<Category[]>(categories).Where(x => x.Name != null).ToList();

                context.Categories.AddRange(categoryInfo);
                context.SaveChanges();

                categoriesCount = categoryInfo.Count();
            }

            return $"Successfully imported { categoriesCount}";

        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportCategoryProductDTO[])
                , new XmlRootAttribute("CategoryProducts"));

            var count = 0;

            using (var reader = new StringReader(inputXml))
            {
                var validPdoductsInBase = context.Products.Select(x => x.Id);
                var validCategoriesInBase = context.Categories.Select(x => x.Id);

                var categoryProducts = (ImportCategoryProductDTO[])serializer.Deserialize(reader);

                var categoryProductInfo = Mapper
                    .Map<CategoryProduct[]>(categoryProducts)
                    .ToList()
                    .Where(x => validCategoriesInBase.Contains(x.CategoryId) &&
                    validPdoductsInBase.Contains(x.ProductId));

                count = categoryProductInfo.Count();

                context.CategoryProducts.AddRange(categoryProductInfo);
                // context.SaveChanges();
            }

            return $"Successfully imported { count}";

        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            StringBuilder builder = new StringBuilder();


            var products = context.Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .ProjectTo<ProductsInRangeDTO>()
                .OrderBy(x => x.Price)
                .Take(10).ToList();

            XmlSerializer serializer = new XmlSerializer(typeof(List<ProductsInRangeDTO>)
                , new XmlRootAttribute("Products"));

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            using (var writer = new StringWriter(builder))
            {
                serializer.Serialize(writer, products, namespaces);
            }


            return builder.ToString().TrimEnd();
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            StringBuilder builder = new StringBuilder();

            var users = context.Users.Where(x => x.ProductsSold.Count > 0)
                .ProjectTo<UserWithLeastOneSoldItemDTO>()
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Take(5).ToList();

            XmlSerializer serializer = new XmlSerializer(typeof(List<UserWithLeastOneSoldItemDTO>)
                , new XmlRootAttribute("Users"));

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            using (var writer = new StringWriter(builder))
            {
                serializer.Serialize(writer, users, namespaces);
            }


            return builder.ToString().TrimEnd();
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            StringBuilder builder = new StringBuilder();

            var categoryInfo = context.Categories.Select(x =>
            new CategoryDTO
            {
                Name = x.Name,
                ProductsCount = x.CategoryProducts.Count(),
                AveragePrice = x.CategoryProducts.Average(cp => cp.Product.Price),
                TotalRevenue = x.CategoryProducts.Sum(p => p.Product.Price)

            })
                .OrderByDescending(x => x.ProductsCount)
                .ToList();

            XmlSerializer serializer = new XmlSerializer(typeof(List<CategoryDTO>)
                , new XmlRootAttribute("Categories"));

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            using (var writer = new StringWriter(builder))
            {

                serializer.Serialize(writer, categoryInfo, namespaces);
            }


            return builder.ToString().TrimEnd();
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            StringBuilder builder = new StringBuilder();

            var users = context.Users
                .Where(u => u.ProductsSold.Any())
                .Select(x =>
                new UserInfoDTO
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age,


                    SoldProducts = new SoldProductsInfoDTO
                    {
                        Count = x.ProductsSold.Count,
                        ProductsSold = x.ProductsSold
                        .Select(p =>
                        new SoldPruductInfoDTO
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                        .OrderByDescending(p => p.Price)
                        .ToList()
                    }
                }
                )
                .OrderByDescending(p => p.SoldProducts.Count)
                .Take(10)
                .ToList();

            var usersInfo = new UsersInfoDTO
            {
                Count = users.Count,
                Users = users
            };


            XmlSerializer serializer = new XmlSerializer(typeof(UsersInfoDTO));

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            using (var writer = new StringWriter(builder))
            {
                serializer.Serialize(writer, usersInfo, namespaces);
            }


            return builder.ToString().TrimEnd();
        }

    }
}
