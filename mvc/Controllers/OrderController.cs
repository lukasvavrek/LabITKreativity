using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkladUcebnic.Data;

namespace SkladUcebnic.Controllers
{
    public class OrderController : Controller
    {
        private readonly SkladUcebnicContext _dbContext;

        public OrderController(SkladUcebnicContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _dbContext.Order
                .Include(x => x.BookOrders)
                .ThenInclude(x => x.Book)
                .ToListAsync();
                
            return View(orders);
        }
    }
}