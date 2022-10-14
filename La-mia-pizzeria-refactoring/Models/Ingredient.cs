using System.ComponentModel.DataAnnotations;

namespace La_mia_pizzeria_refactoring.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string? Icon { get; set; } 
        public string? Name { get; set; } 
        public string? Color { get; set; } 

        //Relation
        public IEnumerable<Pizza> Pizzas { get; set; }

        public Ingredient()
        {
            Pizzas = new List<Pizza>();
        }
    }
}
