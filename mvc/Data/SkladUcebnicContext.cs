#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkladUcebnic.Models;

namespace SkladUcebnic.Data
{
    public class SkladUcebnicContext : DbContext
    {
        public SkladUcebnicContext (DbContextOptions<SkladUcebnicContext> options)
            : base(options)
        {
        }

        public DbSet<SkladUcebnic.Models.Book> Book { get; set; }
    }
}
