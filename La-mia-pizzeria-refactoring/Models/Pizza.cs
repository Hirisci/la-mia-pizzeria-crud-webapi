using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace La_mia_pizzeria_refactoring.Models
{
    
    [Index(nameof(Name), IsUnique = true)]
    public class Pizza
    {

        public int Id{ get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;
        public string? Img { get; set; }

        [Column(TypeName = "money")]
        [Range(1,999.99)]
        public decimal Price { get; set; }

        public bool IsVisible { get; set; } = true;

        //Relation
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; }

        public Pizza()
        {
            Ingredients = new List<Ingredient>();
        }
    }
}
