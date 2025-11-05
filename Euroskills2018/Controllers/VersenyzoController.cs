using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Euroskills2018.Data;
using Euroskills2018.Models;

namespace Euroskills2018.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersenyzoController : ControllerBase
    {
        private readonly EuroskillsDbContext _context;

        public VersenyzoController(EuroskillsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VersenyzoModel>>> GetVersenyzok()
        {
            return await _context.Versenyzok.Include(x => x.Szakma).Include(x => x.Orszag).ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<VersenyzoModel>> GetVersenyzoModel(int id)
        {
            var versenyzoModel = await _context.Versenyzok.FindAsync(id);

            if (versenyzoModel == null)
            {
                return NotFound();
            }

            return versenyzoModel;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutVersenyzoModel(int id, VersenyzoModel versenyzoModel)
        {
            if (id != versenyzoModel.id)
            {
                return BadRequest();
            }

            _context.Entry(versenyzoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VersenyzoModelExists(id))
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
        public async Task<ActionResult<VersenyzoModel>> PostVersenyzoModel(VersenyzoModel versenyzoModel)
        {
            _context.Versenyzok.Add(versenyzoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVersenyzoModel", new { id = versenyzoModel.id }, versenyzoModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVersenyzoModel(int id)
        {
            var versenyzoModel = await _context.Versenyzok.FindAsync(id);
            if (versenyzoModel == null)
            {
                return NotFound();
            }

            _context.Versenyzok.Remove(versenyzoModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VersenyzoModelExists(int id)
        {
            return _context.Versenyzok.Any(e => e.id == id);
        }
    }
}
