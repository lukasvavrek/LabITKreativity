using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SkladUcebnic.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string ISBN  { get; set; }


        [StringLength(8)]
        [Required]
        public string StorageNumber { get; set; }

        
        public decimal Price { get; set; }
    }
}
