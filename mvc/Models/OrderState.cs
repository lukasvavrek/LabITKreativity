using System.ComponentModel.DataAnnotations;

namespace SkladUcebnic.Models
{
    public enum OrderState
    {
        [Display(Name = "Nová")]
        New = 0,
        [Display(Name = "V procese")]
        InProgress = 1,
        [Display(Name = "Dokončená")]
        Finished = 2,
    }
}