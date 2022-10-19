using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using La_mia_pizzeria_refactoring.Data;
using La_mia_pizzeria_refactoring.Models;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace La_mia_pizzeria_refactoring.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly INotyfService _toastNotification;

        public MessagesController(AppDbContext db, INotyfService toastNotification) {
            _db = db;
            _toastNotification = toastNotification;
        }


        // GET: api/messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> Getmessage()
        {
            return await _db.Messages.ToListAsync();
        }

        // GET: api/messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> Getmessage(int id)
        {
            var message = await _db.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // PUT: api/messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putmessage(int id, Message message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }

            _db.Entry(message).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!messageExists(id))
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

        // POST: api/messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Message>> Postmessage(Message message)
        {
            _db.Messages.Add(message);
            await _db.SaveChangesAsync();
            _toastNotification.Success($"Recensione aggiunta con successo!");

            return CreatedAtAction("Getmessage", new { id = message.Id }, message);
        }

        // DELETE: api/messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletemessage(int id)
        {
            var message = await _db.Messages.FindAsync(id);
            if (message == null)
            {
                _toastNotification.Error("Impossibile cancellare il messaggio");
                return NoContent();
            }

            _toastNotification.Success($"{message.Titolo} é stato cancellato");
            _db.Messages.Remove(message);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool messageExists(int id)
        {
            return _db.Messages.Any(e => e.Id == id);
        }
    }
}
