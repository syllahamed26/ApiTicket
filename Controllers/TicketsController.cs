using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiTckets.Entities;
using apiTckets.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiTckets.Controllers
{
    [Route("api/v1/tickets")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private protected ApiTicketDbContext _context;
        public TicketsController(ApiTicketDbContext context)
        {
            _context = context;
        }
        // GET: api/<TicketsController>
        [HttpGet]
        public async Task<ActionResult<List<Ticket>>> Get()
        {
            var tickets = await _context.Tickets.ToListAsync();

            return Ok(tickets);
        }

        // GET api/<TicketsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> Get(int id)
        {
            var findTicket = await _context.Tickets.FindAsync(id);

            if (findTicket == null)
            {
                return NotFound();
            }

            return Ok(findTicket);
        }

        // POST api/<TicketsController>
        [HttpPost]
        public async Task<ActionResult<Ticket>> Post([FromBody] Ticket ticket)
        {
            var verifEvent = from e in _context.Events
                             where e.Id == ticket.EventId
                             select new { e.Id, e.Nom };

            if (verifEvent.Count() == 0)
            {
                return NotFound("L'évenement n'existe pas");
            }

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return Ok(ticket);
        }

        // PUT api/<TicketsController>/5
        [HttpPost("{id}")]
        public async Task<ActionResult<Ticket>> Put(int id, [FromBody] Ticket ticket)
        {
            var findTicket = await _context.Tickets.FindAsync(id);

            if (findTicket == null)
            {
                return NotFound();
            }

            findTicket.Nombre = ticket.Nombre;
            findTicket.Type = ticket.Type;
            findTicket.Nombre = ticket.Nombre;
            findTicket.UpdateAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(findTicket);
        }

        // DELETE api/<TicketsController>/5
        [HttpGet("delete/{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var findTicket = await _context.Tickets.FindAsync(id);

            if (findTicket == null)
            {
                return NotFound();
            }

            _context.Remove(findTicket);
            await _context.SaveChangesAsync();

            return Ok("Suppression éffectuée");
        }
    }
}