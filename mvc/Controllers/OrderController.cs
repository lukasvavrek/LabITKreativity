using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkladUcebnic.Data;
using SkladUcebnic.Models;

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

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            var books = await _dbContext.Book.ToListAsync();
            var bookOrders = books.ConvertAll<BookOrder>(book => new BookOrder { Book = book, BookId = book.Id });
            var order = new Order { BookOrders = bookOrders };

            return View(order);
        }
    }
}