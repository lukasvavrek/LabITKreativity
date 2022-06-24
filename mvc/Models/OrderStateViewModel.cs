using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SkladUcebnic.Models
{
    public class OrderStateViewModel
    {
        public List<Order> Orders { get; set; }
        public OrderState[] States { get; set; }
    }

}