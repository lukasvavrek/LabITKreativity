using System;
using System.Collections.Generic;

namespace SkladUcebnic.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        public OrderState OrderState { get; set; }
        
        public string OrderedBy { get; set; }
        public DateTime OrderedAt { get; set; }
        
        public string ClassIdentifier { get; set; }
        
        public List<BookOrder> BookOrders { get; set; }
    }
}