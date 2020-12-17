using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiTckets.Entities;
using apiTckets.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiTckets.Functions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiTckets.Controllers
{
    [Route("api/v1/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApiTicketDbContext _context;
        public EventsController(ApiTicketDbContext context)
        {
            _context = context;
        }

        // GET: api/<EventsController>
        [HttpGet]
        public async Task<ActionResult<List<Event>>> Get()
        {
            var AllEvents = await _context.Events.ToListAsync();

            return Ok(AllEvents);
        }

        // GET api/<EventsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            var findEvent = await _context.Events.FindAsync(id);

            if (findEvent == null)
            {
                return NotFound();
            }
            return Ok(findEvent);
        }

        // POST api/<EventsController>
        [HttpPost]
        public async Task<ActionResult<Event>> Post([FromBody] Event events)
        {
            var EventExist = from e in _context.Events
                             where e.Nom == events.Nom
                             select new { e.Id, e.Nom };
            if (EventExist.Count() > 0)
            {
                return Conflict("Cet évenement exsite déjà");
            }
            events.CodeEvent = MyFunctions.GenererCodeEvent(_context.Events);
            events.Statut = "A venir";
            events.Heure = DateTime.Parse(events.Heure.ToLongTimeString());
            await _context.Events.AddAsync(events);
            await _context.SaveChangesAsync();

            return Ok(events);
        }

        // PUT api/<EventsController>/5
        [HttpPost("{id}")]
        public async Task<ActionResult<Event>> Put(int id, [FromBody] Event events)
        {
            var findEvent = await _context.Events.FindAsync(id);

            if (findEvent == null)
            {
                return NotFound();
            }
            var EventExist = from e in _context.Events
                             where e.Id!= id && e.Nom == events.Nom
                             select new { e.Id, e.Nom };
            if (EventExist.Count()>0)
            {
                return Conflict("Cet évenement exsite déjà");
            }
            findEvent.Nom = events.Nom;
            findEvent.Lieu = events.Lieu;
            findEvent.Date = events.Date;
            findEvent.Image = events.Image;
            findEvent.Heure = events.Heure;
            findEvent.TypeId = events.TypeId;
            findEvent.UpdateAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(findEvent);
        }

        // DELETE api/<EventsController>/5
        [HttpGet("delete/{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var findEvent = await _context.Events.FindAsync(id);

            if (findEvent == null)
            {
                return NotFound();
            }

            _context.Remove(findEvent);
            await _context.SaveChangesAsync();
            return Ok("Suppression éffectuée");
        }

    }

}
