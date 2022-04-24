using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SkladUcebnic.Data
{
    public static class Database
    {
        public static async Task Migrate(SkladUcebnicContext dbContext)
        {
            await dbContext.Database.MigrateAsync();
        }
    }
}