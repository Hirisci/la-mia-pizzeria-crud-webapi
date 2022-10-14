using System.ComponentModel.DataAnnotations;

namespace La_mia_pizzeria_refactoring.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        //Relation
        public IEnumerable<Pizza> Pizzas { get; set; }

        public Category()
        {
            Pizzas = new List<Pizza>();
        }
    }
}
