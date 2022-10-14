using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using La_mia_pizzeria_refactoring.Data;
using La_mia_pizzeria_refactoring.Models;

namespace La_mia_pizzeria_refactoring.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly AppDbContext _db;

        public PizzasController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/Pizzas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pizza>>> GetPizzas()
        {
            return await _db.Pizzas.Include("Ingredients").Where(x=> x.IsVisible ).ToListAsync();
        }

        // GET: api/Pizzas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> GetPizza(int id)
        {
            var pizza = await _db.Pizzas.FindAsync(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return pizza;
        }

        // PUT: api/Pizzas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizza(int id, Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return BadRequest();
            }

            _db.Entry(pizza).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pizzas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pizza>> PostPizza(Pizza pizza)
        {
            _db.Pizzas.Add(pizza);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetPizza", new { id = pizza.Id }, pizza);
        }

        // DELETE: api/Pizzas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            var pizza = await _db.Pizzas.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }

            _db.Pizzas.Remove(pizza);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool PizzaExists(int id)
        {
            return _db.Pizzas.Any(e => e.Id == id);
        }
    }
}
