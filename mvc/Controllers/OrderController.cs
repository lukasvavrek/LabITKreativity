using System;
using System.Linq;
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

        [HttpGet]
        public async Task<IActionResult> Index(string ordState)
        {
            IQueryable<Order> orders = _dbContext.Order
                .Include(x => x.BookOrders)
                .ThenInclude(x => x.Book);

            var orderStateFilterActive = Enum.TryParse(ordState, out OrderState stateToFilter);
            
            if (!string.IsNullOrEmpty(ordState) && orderStateFilterActive)
            {
                orders = orders.Where(order => order.OrderState == stateToFilter);
            }

            var orderListVM = new OrderListViewModel
            {
                Orders = await orders.ToListAsync(),
                SelectedState = orderStateFilterActive ? stateToFilter : null
            };

            return View(orderListVM);
        }

        // GET: Orders/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(await GetNewOrderAsync());
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind]Order order)
        {
            if (ModelState.IsValid)
            {
                order.BookOrders = order.BookOrders.Where(bookOrder => bookOrder.Quantity > 0).ToList();

                order.OrderState = OrderState.New;
                order.OrderedAt = DateTime.Now;

                await _dbContext.AddAsync(order);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(await GetOrderWithAlreadyFilledDataAsync(order));
        }

        private async Task<Order> GetNewOrderAsync()
        {
            var books = await _dbContext.Book.ToListAsync();
            var bookOrders = books.ConvertAll(book => new BookOrder {Book = book, BookId = book.Id});
            return new Order {BookOrders = bookOrders};
        }

        private async Task<Order> GetOrderWithAlreadyFilledDataAsync(Order order)
        {
            var newOrder = await GetNewOrderAsync();

            newOrder.ClassIdentifier = order.ClassIdentifier;
            newOrder.OrderedBy = order.OrderedBy;
            newOrder.BookOrders.ForEach(bookOrder =>
            {
                var existingBookOrder = order.BookOrders.SingleOrDefault(bo => bo.BookId == bookOrder.BookId);
                if (existingBookOrder != null)
                {
                    bookOrder.Quantity = existingBookOrder.Quantity;
                }
            });
            return newOrder;
        }
    }
}
