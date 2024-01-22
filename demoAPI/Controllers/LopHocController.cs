using demoAPI.Data;
using demoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LopHocController : ControllerBase
    {
        private ApiDbContext _context;

        public LopHocController(ApiDbContext context) {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll() {
            var loais = _context.lopHocs.ToList();
            return Ok(loais);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            try
            {
                var dsLoai = _context.lopHocs.SingleOrDefault(lop => lop.MaLop == id);
                if (dsLoai == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(dsLoai);
                }
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPost]
        public IActionResult PostLop(LopHocMd hocMd)
        {
            try
            {
                var lopHoc = new LopHoc
                {
                    TenLop = hocMd.TenLop,
                };
                _context.Add(lopHoc);
                _context.SaveChanges();
                return Ok(lopHoc);
            }
            catch
            {
                return BadRequest(0);
            }


        }
        [HttpPut("{id}")]
        public IActionResult update(int id, LopHocMd lopHocMd)
        {
            try
            {
                var lopHoc = _context.lopHocs.SingleOrDefault(lh => lh.MaLop == id);
                if (lopHoc == null)
                {
                    return NotFound();
                }
                else
                {
                    lopHoc.TenLop = lopHocMd.TenLop;
                    _context.SaveChanges();
                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var lopHoc = _context.lopHocs.SingleOrDefault(lh => lh.MaLop == id);
                if(lopHoc == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.Remove(lopHoc);
                    _context.SaveChanges();
                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
           
        }

    } 
        }
    

