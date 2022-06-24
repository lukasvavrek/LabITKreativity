using System.Collections.Generic;

namespace SkladUcebnic.Models
{
    public class OrderListViewModel
    {
        public List<Order> Orders { get; set; }
        public OrderState? SelectedState { get; set; }
    }

}