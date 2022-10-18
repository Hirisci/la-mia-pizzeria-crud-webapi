using System.ComponentModel.DataAnnotations;

namespace La_mia_pizzeria_refactoring.Models
{
    public class IngredientViewModel
    {
        public Ingredient Ingredient { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}
