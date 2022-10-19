using La_mia_pizzeria_refactoring.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace La_mia_pizzeria_refactoring.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //DbSet
        public DbSet<Pizza> Pizzas { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Ingredient> Ingredients { set; get; }
        public DbSet<Message> Messages { get; set; }


    }
}
