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
            var promoteur = await _context.Promoteurs.FindAsync(id);

            if (promoteur == null)
            {
                return NotFound();
            }

            return Ok(promoteur);
        }

        // POST api/<PromoteursController>
        [HttpPost]
        public async Task<ActionResult<Promoteur>> Post([FromBody] Promoteur promoteur)
        {
            var promoteurExist = from p in _context.Promoteurs
                                 where p.Contact == promoteur.Contact || p.Mail == promoteur.Mail
                                 select new { p.Id, p.Nom, p.Contact, p.Mail, p.Photo };

            if (promoteurExist.Count() > 0)
            {
                return Conflict("Les informations existent déja !");
            }

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

            var promoteurExist = from p in _context.Promoteurs
                                 where p.Id != id && (p.Contact == promoteur.Contact || p.Mail == promoteur.Mail)
                                 select new { p.Id, p.Nom, p.Contact, p.Mail, p.Photo };

            if (promoteurExist.Count() > 0)
            {
                return Conflict("Les informations existent déja !");
            }

            findPromoteur.Nom = promoteur.Nom;
            findPromoteur.Mail = promoteur.Mail;
            findPromoteur.Contact = promoteur.Contact;
            findPromoteur.UpdateAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(findPromoteur);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Promoteur>> Login([FromBody] string login, string pwd)
        {
            var searchPromoteur = from p in _context.Promoteurs
                                  where p.Login == login && p.Pwd == pwd
                                  select new
                                  {
                                      p.Id,
                                      p.Login,
                                      p.Nom,
                                      p.Contact,
                                      p.Mail,
                                      p.Photo
                                  };
            if (searchPromoteur.Count() > 0)
            {
                return NotFound("Utilisateur introuvable");
            }

            return Ok(searchPromoteur);
        }

        // DELETE api/<PromoteursController>/5
        [HttpGet("delete/{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var findPromoteur = await _context.Promoteurs.FindAsync(id);

            if (findPromoteur == null)
            {
                return NotFound();
            }

            _context.Remove(findPromoteur);

            return Ok("Suppression éffectuée");
        }
    }
}
