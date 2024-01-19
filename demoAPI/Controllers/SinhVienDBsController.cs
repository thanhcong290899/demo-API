using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demoAPI.Data;

namespace demoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinhVienDBsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public SinhVienDBsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/SinhVienDBs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SinhVienDB>>> GetSinhViens()
        {
          if (_context.SinhViens == null)
          {
              return NotFound();
          }
            return await _context.SinhViens.ToListAsync();
        }

        // GET: api/SinhVienDBs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SinhVienDB>> GetSinhVienDB(Guid id)
        {
          if (_context.SinhViens == null)
          {
              return NotFound();
          }
            var sinhVienDB = await _context.SinhViens.FindAsync(id);

            if (sinhVienDB == null)
            {
                return NotFound();
            }

            return sinhVienDB;
        }

        // PUT: api/SinhVienDBs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSinhVienDB(Guid id, SinhVienDB sinhVienDB)
        {
            if (id != sinhVienDB.MaSV)
            {
                return BadRequest();
            }

            _context.Entry(sinhVienDB).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SinhVienDBExists(id))
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

        // POST: api/SinhVienDBs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SinhVienDB>> PostSinhVienDB(SinhVienDB sinhVienDB)
        {
          if (_context.SinhViens == null)
          {
              return Problem("Entity set 'ApiDbContext.SinhViens'  is null.");
          }
            sinhVienDB.MaSV = new Guid();
            _context.SinhViens.Add(sinhVienDB);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSinhVienDB", new { id = new Guid() }, sinhVienDB);
        }

        // DELETE: api/SinhVienDBs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSinhVienDB(Guid id)
        {
            if (_context.SinhViens == null)
            {
                return NotFound();
            }
            var sinhVienDB = await _context.SinhViens.FindAsync(id);
            if (sinhVienDB == null)
            {
                return NotFound();
            }

            _context.SinhViens.Remove(sinhVienDB);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SinhVienDBExists(Guid id)
        {
            return (_context.SinhViens?.Any(e => e.MaSV == id)).GetValueOrDefault();
        }
    }
}
