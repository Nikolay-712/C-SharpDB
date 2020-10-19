namespace ProductShop
{
    using System.Linq;
    using Newtonsoft.Json;
    using ProductShop.Data;
    using ProductShop.Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {

            using (var productShopContext = new ProductShopContext())
            {
                // productShopContext.Database.EnsureCreated();


                //01. Import Users 
                //var usersInputJson = File.ReadAllText("./../../../Datasets/users.json");
                //Console.WriteLine(ImportUsers(productShopContext, usersInputJson));

                //02. Import Products 
                //var productsInputJson = File.ReadAllText("./../../../Datasets/products.json");
                // Console.WriteLine(ImportProducts(productShopContext,productsInputJson));

                //03. Import Categories
                //var categoriesInputJson = File.ReadAllText("./../../../Datasets/categories.json");
                //Console.WriteLine(ImportCategories(productShopContext, categoriesInputJson));

                //04. Import Categories and Products 
                //var categoryProductsInputJson = File.ReadAllText("./../../../Datasets/categories-products.json");
                //Console.WriteLine(ImportCategoryProducts(productShopContext,categoryProductsInputJson));

                //05. Export Products In Range 
                //Console.WriteLine(GetProductsInRange(productShopContext));

                //06. Export Sold Products 
                //Console.WriteLine(GetSoldProducts(productShopContext));

                //07. Export Categories By Products Count 
                //Console.WriteLine(GetCategoriesByProductsCount(productShopContext));

                //08. Export Users and Products 
                //Console.WriteLine(GetUsersWithProducts(productShopContext));


            }

        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";


        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {

            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<Category[]>(inputJson)
                .Where(x => x.Name != null).ToArray();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Length}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Length}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var productsInRange = context.Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .OrderBy(x => x.Price)
                .Select(x => new
                {
                    name = x.Name,
                    price = x.Price,
                    seller =
                        $"{x.Seller.FirstName} " +
                        $"{x.Seller.LastName}"
                })

               .ToList();

            var productsInRangeJson = JsonConvert.SerializeObject(productsInRange, Formatting.Indented);

            return productsInRangeJson;

        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                    .Where(u => u.ProductsSold
                    .Any(x => x.BuyerId != null))
                    .Select(x => new
                    {
                        firstName = x.FirstName,
                        lastName = x.LastName,
                        soldProducts = x.ProductsSold
                        .Where(p => p.BuyerId != null)
                        .Select(p => new
                        {

                            name = p.Name,
                            price = p.Price,
                            buyerFirstName = p.Buyer.FirstName,
                            buyerLastName = p.Buyer.LastName
                        })
                    })
                    .OrderBy(x => x.lastName)
                    .ThenBy(x => x.firstName)
                    .ToList();

            var usersWithSoldProducts = JsonConvert.SerializeObject(users, Formatting.Indented);

            return usersWithSoldProducts;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categoriesInfo = context.Categories
                .Where(x => x.Name != null)
                .Select(x => new
                {
                    category = x.Name,
                    productsCount = x.CategoryProducts.Count,
                    averagePrice = $"{ x.CategoryProducts.Select(p => p.Product.Price).Average():f2}",
                    totalRevenue = $"{ x.CategoryProducts.Select(p => p.Product.Price).Sum():f2}"
                })
                .OrderByDescending(pc => pc.productsCount)
                .ToList();

            var info = JsonConvert.SerializeObject(categoriesInfo, Formatting.Indented);

            return info;
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var usersWithProducts = context.Users
                 .Where(u => u.ProductsSold
                 .Any(x => x.BuyerId != null))
                .Select(x => new
                {
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    age = x.Age,
                    soldProducts = new
                    {
                        count = x.ProductsSold.Count(),
                        products = x.ProductsSold.Where(p => p.BuyerId != null)
                        .Select(p => new { name = p.Name, price = p.Price })

                    }
                })
                .OrderByDescending(p => p.soldProducts.count)
                .ToList();

            var usersCount = new
            {
                usersCount = usersWithProducts.Count(),
                users = usersWithProducts
            };


            var usersInfo = JsonConvert
                .SerializeObject(usersCount, Formatting.Indented,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });


            return usersInfo;
        }


    }
}