using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demoAPI.Data;
using demoAPI.Models;

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
        [HttpGet]
        public IActionResult GetAll() {
            var sinhVien = _context.SinhViens.ToList();
            return Ok(sinhVien);
        }
        [HttpPost]
        public IActionResult CreatNew(SinhVien sinhVien) {
            try
            {
                var sv = new SinhVienDB
                {
                    TenSV = sinhVien.TenSV,
                    DiaChi = sinhVien.DiaChi,
                    Email = sinhVien.Email,
                    SDT = sinhVien.SDT,


                };
                _context.Add(sv);
                _context.SaveChanges();
                return Ok(sv);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(String id) {
            try
            {
                var sv = _context.SinhViens.SingleOrDefault(s => s.MaSV == Guid.Parse(id));
                if (sv == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(sv);
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(String id, SinhVien sinhVien)
        {
            try
            {
                var sv = _context.SinhViens.SingleOrDefault(s => s.MaSV == Guid.Parse(id));
                if (sv == null)
                {
                    return NotFound();
                }
                else
                {
                    sv.TenSV = sinhVien.TenSV;
                    sv.DiaChi = sinhVien.DiaChi;
                    sv.SDT = sinhVien.SDT;
                    sv.Email = sinhVien.Email;
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
        public IActionResult Delete(String id)
        {
            try
            {
                var sv = _context.SinhViens.SingleOrDefault(s => s.MaSV == Guid.Parse(id));
                if (sv == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.Remove(sv);
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
