namespace SkladUcebnic.Models
{
    public class BookOrder
    {
        public int BookId { get; set; }
        public int OrderId { get; set; }
        
        public Book? Book { get; set; }
        public Order? Order { get; set; }
        
        public int Quantity { get; set; }
    }
}