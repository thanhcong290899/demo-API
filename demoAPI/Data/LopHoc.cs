using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demoAPI.Data
{
    [Table("LopHoc")]
    public class LopHoc
    {
        [Key]
        public int MaLop {  get; set; }
        public string TenLop { get; set; }

        public Guid? MaSV { get; set; }
        [ForeignKey("MaSV")]
        public SinhVienDB SinhVienDB { get; set; }

    }
}
