using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SkladUcebnic.Models;

namespace SkladUcebnic.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var contextOptions = serviceProvider.GetRequiredService<DbContextOptions<SkladUcebnicContext>>();
            await using var dbContext = new SkladUcebnicContext(contextOptions);
            
            if (await DbAlreadySeeded(dbContext))
            {
                return; 
            }

            await SeedBooks(dbContext);
            await SeedOrders(dbContext);

            await dbContext.SaveChangesAsync();
        }

        private static Task<bool> DbAlreadySeeded(SkladUcebnicContext dbContext)
        {
            return dbContext.Book.AnyAsync();
        }

        private static async Task SeedBooks(SkladUcebnicContext dbContext)
        {
            await dbContext.Book.AddRangeAsync(Books);
        }
        
        private static async Task SeedOrders(SkladUcebnicContext dbContext)
        {
            await dbContext.Order.AddRangeAsync(Orders);
        }

        private static readonly List<Book> Books = new List<Book>
        {
            new Book
            {
                Title = "Matematika Algebra rovnice a nerovnice",
                Author = "Jozef Smida",
                ISBN = "80-10-00560-6",
                StorageNumber = "1-31-189",
                Price = 4.5M
            },
            new Book
            {
                Title = "Matematika pre 1. ročník gymnázií",
                Author = "Zbynek Kubáček",
                ISBN = "978-80-10-01758-0",
                StorageNumber = "1-31-001",
                Price = 11.7M
            },
            new Book
            {
                Title =
                    "Geografia pre 1. ročník gymnázia so štvorročným štúdiom a 5. ročník gymnázia s osemročným štúdiom",
                Author = "Peter Likavský",
                ISBN = "978-80-8091-503-2",
                StorageNumber = "1-31-111",
                Price = 7.59M
            },
            new Book
            {
                Title = "Slovenský jazyk pre stredné školy 1",
                Author = "Milada Caltíková",
                ISBN = "978-80-8120-685-6",
                StorageNumber = "1-31-156",
                Price = 3.95M
            }
        };

        private static List<Order> Orders => new List<Order> {
            new Order
            {
                ClassIdentifier = "3.C",
                OrderedAt = DateTime.Today,
                OrderedBy = "Janko Hraško",
                OrderState = OrderState.New,
                BookOrders = new List<BookOrder>
                {
                    new BookOrder
                    {
                        Book = Books[0],
                        Quantity = 10,
                    },
                    new BookOrder
                    {
                        Book = Books[1],
                        Quantity = 15,
                    }
                }
            }
        };
    }
}