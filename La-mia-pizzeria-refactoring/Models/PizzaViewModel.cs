using Microsoft.AspNetCore.Mvc.Rendering;

namespace La_mia_pizzeria_refactoring.Models
{
    public class PizzaViewModel
    {

        public Pizza Pizza { get; set; }
        public Ingredient Ingredient { get; set; }
        public Category Category { get; set; }
        public IEnumerable<int> SelectedIngredients { get; set; }
        public IEnumerable<SelectListItem> Ingredients { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public PizzaViewModel()
        {
            Pizza = new Pizza();
            Ingredient = new Ingredient();
            Category= new Category();
            SelectedIngredients = new List<int>();
            Ingredients = new List<SelectListItem>();
            Categories = new List<SelectListItem>();
        }
    }
}
