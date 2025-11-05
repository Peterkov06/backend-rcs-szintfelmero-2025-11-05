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
    public class SzakmaController : ControllerBase
    {
        private readonly EuroskillsDbContext _context;

        public SzakmaController(EuroskillsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SzakmaDTO>>> GetSzakmak()
        {
            return await _context.Szakmak.Select(x => new SzakmaDTO { id = x.id, szakmaNev = x.szakmaNev}).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SzakmaDTO>> GetSzakmaModel(string id)
        {
            var szakmaModel = await _context.Szakmak.FindAsync(id);

            if (szakmaModel == null)
            {
                return NotFound();
            }

            return new SzakmaDTO { id = szakmaModel.id, szakmaNev = szakmaModel.szakmaNev};
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSzakmaModel(string id, SzakmaModel szakmaModel)
        {
            if (id != szakmaModel.id)
            {
                return BadRequest();
            }

            _context.Entry(szakmaModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SzakmaModelExists(id))
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
        public async Task<ActionResult<SzakmaModel>> PostSzakmaModel(SzakmaDTO szakmaDTO)
        {
            _context.Szakmak.Add(new SzakmaModel { id = szakmaDTO.id, szakmaNev = szakmaDTO.szakmaNev});
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SzakmaModelExists(szakmaDTO.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSzakmaModel", new { id = szakmaDTO.id }, szakmaDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSzakmaModel(string id)
        {
            var szakmaModel = await _context.Szakmak.FindAsync(id);
            if (szakmaModel == null)
            {
                return NotFound();
            }

            _context.Szakmak.Remove(szakmaModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SzakmaModelExists(string id)
        {
            return _context.Szakmak.Any(e => e.id == id);
        }
    }
}
