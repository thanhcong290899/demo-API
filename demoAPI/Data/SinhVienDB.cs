using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demoAPI.Data
{
    [Table("SinhVien")]
    public class SinhVienDB
    {
        [Key]
        public Guid MaSV {  get; set; }
       
        public string? TenSV { get; set; }
        public string? DiaChi { get;set; }
        public string? Email { get; set; }
        public string? SDT { get; set;}

       
    }
}
