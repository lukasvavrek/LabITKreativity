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

        public async Task<IActionResult> Index()
        {
            var orders = await _dbContext.Order
                .Include(x => x.BookOrders)
                .ThenInclude(x => x.Book)
                .ToListAsync();

            return View(orders);
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

        public async Task<IActionResult> Index(string ordState)
        {
            // Use LINQ to get list of orders by state

            IQueryable<Order> orders = _dbContext.Order;

            if (!string.IsNullOrEmpty(ordState))
            {
                orders = orders.Where(order => order.OrderState.ToString() == ordState);
            }

            var stateVM = new OrderStateViewModel
            {
                Orders = await orders.ToListAsync(),
                States = (OrderState[])Enum.GetValues(typeof(OrderState))

            };

            return View(stateVM);

        }
    }
}