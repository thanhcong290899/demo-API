using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeMoAuthen.Data;

namespace DeMoAuthen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookDBsController : ControllerBase
    {
        private readonly BookDbContext _context;

        public BookDBsController(BookDbContext context)
        {
            _context = context;
        }

        // GET: api/BookDBs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDB>>> GetBookDBs()
        {
          if (_context.BookDBs == null)
          {
              return NotFound();
          }
            return await _context.BookDBs.ToListAsync();
        }

        // GET: api/BookDBs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDB>> GetBookDB(int id)
        {
          if (_context.BookDBs == null)
          {
              return NotFound();
          }
            var bookDB = await _context.BookDBs.FindAsync(id);

            if (bookDB == null)
            {
                return NotFound();
            }

            return bookDB;
        }

        // PUT: api/BookDBs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookDB(int id, BookDB bookDB)
        {
            if (id != bookDB.ID)
            {
                return BadRequest();
            }

            _context.Entry(bookDB).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookDBExists(id))
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

        // POST: api/BookDBs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookDB>> PostBookDB(BookDB bookDB)
        {
          if (_context.BookDBs == null)
          {
              return Problem("Entity set 'BookDbContext.BookDBs'  is null.");
          }
            _context.BookDBs.Add(bookDB);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookDB", new { id = bookDB.ID }, bookDB);
        }

        // DELETE: api/BookDBs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookDB(int id)
        {
            if (_context.BookDBs == null)
            {
                return NotFound();
            }
            var bookDB = await _context.BookDBs.FindAsync(id);
            if (bookDB == null)
            {
                return NotFound();
            }

            _context.BookDBs.Remove(bookDB);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookDBExists(int id)
        {
            return (_context.BookDBs?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
