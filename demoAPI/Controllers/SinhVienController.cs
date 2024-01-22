using demoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinhVienController : ControllerBase
    {
        public static List<SinhVien> sinhViens = new List<SinhVien>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(sinhViens);
        }
        [HttpPost]
        public IActionResult PostSinhVien(SinhVien sinhVienMv)
        {
            var sinhvien = new SinhVien
            {
                MaSinhVien = Guid.NewGuid(),
                TenSV = sinhVienMv.TenSV,
                Email = sinhVienMv.Email,
                DiaChi = sinhVienMv.DiaChi,
                SDT = sinhVienMv.SDT,
            };
            sinhViens.Add(sinhvien);
            return Ok(new
            {
                Success = true,
                Data = sinhViens
            });
        }
        [HttpGet("{id}")]
        public IActionResult GetId(String id)
        {
            try
            {
                var sinhvien = sinhViens.SingleOrDefault(sv => sv.MaSinhVien == Guid.Parse(id));
                if (sinhvien == null)
                {
                    return NotFound();
                }
                return Ok(sinhvien);
            }
            catch
            {
                return BadRequest();
            }


        }
        [HttpPut("{id}")]
        public IActionResult SinhVienEdit(String id, SinhVien sinhVienEdit)
        {
            try
            {
                var sinhvien = sinhViens.SingleOrDefault(sv => sv.MaSinhVien == Guid.Parse(id));
                if (sinhvien == null)
                {
                    return NotFound();
                }
                sinhvien.TenSV = sinhVienEdit.TenSV;
                sinhvien.DiaChi = sinhVienEdit.DiaChi;
                sinhvien.Email = sinhVienEdit.Email;
                sinhvien.SDT = sinhVienEdit.SDT;
                return Ok(sinhvien);
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
                var sinhvien = sinhViens.SingleOrDefault(sv => sv.MaSinhVien == Guid.Parse(id));
                if (sinhvien == null)
                {
                    return NotFound();
                }
                if(id != sinhvien.MaSinhVien.ToString())
                {
                    return BadRequest();
                }
                sinhViens.Remove(sinhvien);
                return Ok(sinhvien);
            }
            catch
            {
                return BadRequest();
            }


        }
    }
}
