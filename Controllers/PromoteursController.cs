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
    [Route("api/v1/promoteurs")]
    [ApiController]
    public class PromoteursController : ControllerBase
    {
        private readonly ApiTicketDbContext _context;
        public PromoteursController(ApiTicketDbContext context)
        {
            _context = context;
        }
        // GET: api/<PromoteursController>
        [HttpGet]
        public async Task<ActionResult<List<Promoteur>>> Get()
        {
            var Promoteurs = await _context.Promoteurs.ToListAsync();

            return Ok(Promoteurs);
        }

        // GET api/<PromoteursController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Promoteur>> Get(int id)
        {
            var promoteur = await  _context.Promoteurs.FindAsync(id);

            if(promoteur == null)
            {
                return NotFound();
            }

            return Ok(promoteur);
        }

        // POST api/<PromoteursController>
        [HttpPost]
        public async Task<ActionResult<Promoteur>> Post([FromBody] Promoteur promoteur)
        {
            await _context.Promoteurs.AddAsync(promoteur);
            await _context.SaveChangesAsync();

            return Ok(promoteur);
        }

        // PUT api/<PromoteursController>/5
        [HttpPost("{id}")]
        public async Task<ActionResult<Promoteur>> Put(int id, [FromBody] Promoteur promoteur)
        {
            var findPromoteur = await _context.Promoteurs.FindAsync(id);

            if (findPromoteur == null)
            {
                return NotFound();
            }

            findPromoteur.Nom = promoteur.Nom;
            findPromoteur.Mail = promoteur.Mail;
            findPromoteur.Contact = promoteur.Contact;

            await _context.SaveChangesAsync();

            return Ok(findPromoteur);
        }

        // DELETE api/<PromoteursController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
