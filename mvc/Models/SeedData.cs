using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SkladUcebnic.Data;
using System;
using System.Linq;

namespace SkladUcebnic.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SkladUcebnicContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SkladUcebnicContext>>()))
            {
                // Look for any movies.
                if (context.Book.Any())
                {
                    return;   // DB has been seeded
                }


                context.Book.AddRange(
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
                        Title = "Geografia pre 1. ročník gymnázia so štvorročným štúdiom a 5. ročník gymnázia s osemročným štúdiom",
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

                ) ;

                context.SaveChanges();
            }
        }
    }
}