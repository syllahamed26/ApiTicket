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
    [Route("api/v1/types")]
    [ApiController]
    public class TypeEventsController : ControllerBase
    {
        private readonly ApiTicketDbContext _context;
        public TypeEventsController(ApiTicketDbContext context)
        {
            _context = context;
        }
        // GET: api/<TypeEventsController>
        [HttpGet]
        public async Task<ActionResult<List<TypeEvent>>> Get()
        {
            var AllTypes = await _context.TypeEvents.ToListAsync();

            return Ok(AllTypes);
            //return new string[] { "value1", "value2" };
        }

        // GET api/<TypeEventsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeEvent>> Get(int id)
        {
            var findType = await _context.TypeEvents.FindAsync(id);

            if (findType == null)
            {
                return NotFound();
            }

            return Ok(findType);
        }

        // POST api/<TypeEventsController>
        [HttpPost]
        public async Task<ActionResult<TypeEvent>> Post([FromBody] TypeEvent typeEvent)
        {
            var TypeExist = from t in _context.TypeEvents
                            where t.Libelle == typeEvent.Libelle
                            select new { t.Id, t.Libelle };

            if (TypeExist.Count() > 0)
            {
                return Conflict("Le libelle existe déja !");
            }

            await _context.TypeEvents.AddAsync(typeEvent);
            await _context.SaveChangesAsync();
            return Ok(typeEvent);

        }

        // PUT api/<TypeEventsController>/5
        [HttpPost("{id}")]
        public async Task<ActionResult<TypeEvent>> Put(int id, [FromBody] TypeEvent typeEvent)
        {
            var findType = await _context.TypeEvents.FindAsync(id);
            if (findType == null)
            {
                return NotFound();
            }

            var TypeExist = from t in _context.TypeEvents
                            where t.Id != id && t.Libelle == typeEvent.Libelle
                            select new { t.Id, t.Libelle };

            if (TypeExist.Count() > 0)
            {
                return Conflict("Le libelle existe déja !");
            }

            findType.Libelle = typeEvent.Libelle;
            findType.UpdateAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(findType);
        }

        // DELETE api/<TypeEventsController>/5
        [HttpGet("delete/{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var findType = await _context.TypeEvents.FindAsync(id);
            if (findType == null)
            {
                return NotFound();
            }
            _context.Remove(findType);
            await _context.SaveChangesAsync();

            return Ok("Suppression éffectuée");
        }
    }
}
