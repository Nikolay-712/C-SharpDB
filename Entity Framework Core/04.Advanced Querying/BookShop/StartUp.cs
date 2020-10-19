namespace BookShop
{
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Castle.DynamicProxy.Generators;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.EntityFrameworkCore.Storage;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices.ComTypes;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using AutoMapper;
    using BookShop.DTO;
    using Microsoft.Extensions.DependencyInjection;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                // DbInitializer.ResetDatabase(db);
            }

            var bookShopContex = new BookShopContext();

            //1. Age Restriction
            //Console.WriteLine(GetBooksByAgeRestriction(bookShopContex, Console.ReadLine()));

            //2. Golden Books
            //Console.WriteLine(GetGoldenBooks(bookShopContex));

            //3. Books by Price 
            //Console.WriteLine(GetBooksByPrice(bookShopContex));

            //4. Not Released In 
            //Console.WriteLine(GetBooksNotReleasedIn(bookShopContex, int.Parse(Console.ReadLine())));

            //5. Book Titles by Category
            //Console.WriteLine(GetBooksByCategory(bookShopContex, Console.ReadLine()));

            //6. Released Before Date
            //Console.WriteLine(GetBooksReleasedBefore(bookShopContex,Console.ReadLine()));

            //7. Author Search
            //Console.WriteLine(GetAuthorNamesEndingIn(bookShopContex, Console.ReadLine()));

            //8. Book Search
            //Console.WriteLine(GetBookTitlesContaining(bookShopContex,Console.ReadLine()));

            //9. Book Search by Author
            //Console.WriteLine(GetBooksByAuthor(bookShopContex, Console.ReadLine()));

            //10. Count Books
            //Console.WriteLine(CountBooks(bookShopContex,int.Parse(Console.ReadLine())));

            //11. Total Book Copies
            //Console.WriteLine(CountCopiesByAuthor(bookShopContex));

            //12. Profit by Category
            //Console.WriteLine(GetTotalProfitByCategory(bookShopContex));

            //13. Most Recent Books
            //Console.WriteLine(GetMostRecentBooks(bookShopContex));

            //14. Increase Prices
            //IncreasePrices(bookShopContex);

            //15. Remove Books
            //Console.WriteLine(RemoveBooks(bookShopContex));

        }


        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var books = context.Books
                    .Select(x => new { x.Title, x.AgeRestriction })
                    .Where(x => x.AgeRestriction.ToString().ToLower() == command.ToLower())
                    .OrderBy(x => x.Title).ToList();

            books.ForEach(x => stringBuilder.AppendLine(x.Title));

            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var books = context.Books
                 .Select(x => new { x.Title, x.Copies, x.EditionType, x.BookId })
                 .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                 .OrderBy(x => x.BookId).ToList();

            books.ForEach(x => stringBuilder.AppendLine(x.Title));

            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var books = context.Books
                    .Where(x => x.Price > 40)
                    .OrderByDescending(x => x.Price).ToList();

            books.ForEach(x => stringBuilder.AppendLine($"{x.Title} - ${x.Price:F2}"));

            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var books = context.Books
                .Select(x => new { x.BookId, x.Title, x.ReleaseDate })
                .Where(x => x.ReleaseDate.Value.Year != year)
                .OrderBy(x => x.BookId).ToList();

            books.ForEach(x => stringBuilder.AppendLine(x.Title));

            return stringBuilder.ToString().TrimEnd();

        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var booksByCategories = new List<Book>();

            var bookCategories = input.Split(" ");

            foreach (var category in bookCategories)
            {
                var books = context.Books
                    .Where(x => x.BookCategories
                    .Any(s => s.Category.Name.ToLower() == category.ToLower()))
                    .ToList();

                booksByCategories.AddRange(books);
            }

            booksByCategories.OrderBy(x => x.Title).ToList()
                .ForEach(x => stringBuilder.AppendLine(x.Title));

            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            StringBuilder stringBuilder = new StringBuilder();


            var parsedDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);


            var books = context.Books
                .Select(x => new { x.Title, x.EditionType, x.Price, x.ReleaseDate })
                .Where(x => x.ReleaseDate < parsedDate)
                .OrderByDescending(x => x.ReleaseDate).ToList();

            books.ForEach(x => stringBuilder.AppendLine($"{x.Title} - {x.EditionType} - ${x.Price:F2}"));

            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var authors = context.Authors
                .Select(x => new { x.FirstName, x.LastName })
                .Where(x => x.FirstName.EndsWith(input))
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();

            authors
                .ForEach(x => stringBuilder.AppendLine($"{x.FirstName} {x.LastName}"));

            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var books = context.Books
                .Select(x => new { x.Title })
                .Where(x => x.Title.ToLower()
                .Contains(input.ToLower()))
                .OrderBy(x => x.Title)
                .ToList();

            books.ForEach(x => stringBuilder.AppendLine(x.Title));

            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var books = context.Books
                .Select(x => new { x.BookId, x.Title, x.Author.FirstName, x.Author.LastName })
                .Where(x => x.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(x => x.BookId).ToList();

            books.ForEach(x => stringBuilder.AppendLine($"{x.Title} ({x.FirstName} {x.LastName})"));

            return stringBuilder.ToString().TrimEnd();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int count = context.Books
                .Select(x => new { x.Title })
                .Where(x => x.Title.Length > lengthCheck)
                .ToList().Count;

            return count;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var authors = context.Authors
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    TotalCopies = x.Books
                .Select(b => b.Copies).Sum()
                })
                .OrderByDescending(x => x.TotalCopies).ToList();

            authors.ForEach(x => stringBuilder.AppendLine($"{x.FirstName} {x.LastName} - {x.TotalCopies}"));

            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var categories = context.Categories
                .Select(x => new
                {
                    x.Name,
                    MostRecent = x.CategoryBooks

                .Select(b => b.Book).Select(b => b.Copies * b.Price).Sum()
                })
                .OrderByDescending(x => x.MostRecent).ToList();


            categories.ForEach(x => stringBuilder.AppendLine($"{x.Name} ${x.MostRecent:F2}"));

            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var categories = context.Categories
                .Select(x => new
                {
                    x.Name,
                    MostRecent = x.CategoryBooks
                .Select(b => b.Book)
                .OrderByDescending(b => b.ReleaseDate)
                .Take(3)
                }).OrderBy(x => x.Name).ToList();


            foreach (var category in categories)
            {
                stringBuilder.AppendLine($"--{category.Name}");

                foreach (var book in category.MostRecent)
                {
                    stringBuilder.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }

            return stringBuilder.ToString().TrimEnd();
        }

        public static void IncreasePrices(BookShopContext context)
        {
            decimal IncreasePricesWith = 5;

            context.Books
                   .Where(x => x.ReleaseDate.Value.Year < 2015)
                   .ToList()
                   .ForEach(b => b.Price += IncreasePricesWith); ;


            context.SaveChanges();
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var copies = 4200;

            var books = context.Books.Where(x => x.Copies < copies).ToList();
            var count = books.Count();

            context.Books.RemoveRange(books);

            context.SaveChanges();

            return count;
        }
    }
}
