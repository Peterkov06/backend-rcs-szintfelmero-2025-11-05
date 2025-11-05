using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Euroskills2018.Data;
using Euroskills2018.Models;
using Euroskills2018.DTOs;

namespace Euroskills2018.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrszagController : ControllerBase
    {
        private readonly EuroskillsDbContext _context;

        public OrszagController(EuroskillsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrszagDTO>>> GetOrszagok()
        {
            return await _context.Orszagok.Select(x => new OrszagDTO { id = x.id, orszagNev = x.orszagNev}).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrszagDTO>> GetOrszag(string id)
        {
            var orszagModel = await _context.Orszagok.FindAsync(id);

            if (orszagModel == null)
            {
                return NotFound();
            }

            return new OrszagDTO { id = orszagModel.id, orszagNev = orszagModel.orszagNev};
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrszagModel(string id, OrszagModel orszagModel)
        {
            if (id != orszagModel.id)
            {
                return BadRequest();
            }

            _context.Entry(orszagModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrszagModelExists(id))
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

        [HttpPost]
        public async Task<ActionResult<OrszagDTO>> PostOrszagModel(OrszagDTO orszagModel)
        {
            _context.Orszagok.Add(new OrszagModel { id = orszagModel.id, orszagNev = orszagModel.orszagNev});
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrszagModelExists(orszagModel.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrszagModel", new { id = orszagModel.id }, orszagModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrszagModel(string id)
        {
            var orszagModel = await _context.Orszagok.FindAsync(id);
            if (orszagModel == null)
            {
                return NotFound();
            }

            _context.Orszagok.Remove(orszagModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrszagModelExists(string id)
        {
            return _context.Orszagok.Any(e => e.id == id);
        }
    }
}
