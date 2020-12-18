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
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiTicketDbContext _context;
        public UsersController(ApiTicketDbContext context)
        {
            _context = context;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            var users = await _context.Users.ToListAsync();

            return Ok(users);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("Utilisateur introuvable");
            }

            return Ok(user);

        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            var verifuser = from u in _context.Users
                            where u.Contact == user.Contact || u.Mail == user.Mail
                            select new { u.Id, u.Nom, u.Contact, u.Mail };

            if (verifuser.Count() > 0)
            {
                return Conflict("L'utilisateur existe déja");
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // PUT api/<UsersController>/5
        [HttpPost("{id}")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] User user)
        {
            var findUser = await _context.Users.FindAsync(id);

            if (findUser==null)
            {
                return NotFound("Utilisateur introuvable");
            }

            var verifuser = from u in _context.Users
                            where u.Id != id && (u.Contact == user.Contact || u.Mail == user.Mail)
                            select new { u.Id, u.Nom, u.Contact, u.Mail };

            if (verifuser.Count() > 0)
            {
                return Conflict("Les informations existent déja");
            }

            findUser.Nom = user.Nom;
            findUser.Contact = user.Contact;
            findUser.Mail = user.Mail;
            findUser.UpdateAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(findUser);
        }

        // DELETE api/<UsersController>/5
        [HttpGet("delete/{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var findUser = await _context.Users.FindAsync(id);

            if (findUser == null)
            {
                return NotFound("Utilisateur introuvable");
            }

            _context.Remove(findUser);
            await _context.SaveChangesAsync();

            return Ok("Suppression éffectuée");
        }
    }
}
