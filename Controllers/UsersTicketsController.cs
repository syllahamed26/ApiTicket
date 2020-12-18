using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiTckets.Entities;
using apiTckets.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using apiTckets.Functions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiTckets.Controllers
{
    [Route("api/v1/achats")]
    [ApiController]
    public class UsersTicketsController : ControllerBase
    {
        private readonly ApiTicketDbContext _context;
        public UsersTicketsController(ApiTicketDbContext context)
        {
            _context = context;
        }
        // GET: api/<UsersTicketsController>
        [HttpGet]
        public async Task<ActionResult<List<UserTicket>>> Get()
        {
            var achats = from a in _context.UsersTickets
                         join t in _context.Tickets on a.TicketId equals t.Id
                         join u in _context.Users on a.UserId equals u.Id
                         orderby a.CreatedAt descending
                         select new { u.Id, u.Nom, u.Contact, u.Mail, t.Montant, t.Type, a.CodeQr, a.CreatedAt };

            return Ok(achats);
        }

        // GET api/<UsersTicketsController>/5
        /*[HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<UsersTicketsController>
        [HttpPost]
        public async Task<ActionResult<UserTicket>> Post([FromBody] UserTicket achat)
        {
            var findTicket = await _context.Tickets.FindAsync(achat.TicketId);
            var findUser = await _context.Users.FindAsync(achat.UserId);

            if (findTicket == null || findUser == null)
            {
                return NotFound("Le client et le ticket n'existe pas");
            }

            achat.CodeQr = MyFunctions.GenererCodeQr(_context.UsersTickets);

            await _context.UsersTickets.AddAsync(achat);
            await _context.SaveChangesAsync();

            return Ok(achat);
        }

        // PUT api/<UsersTicketsController>/5
        /*[HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersTicketsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
