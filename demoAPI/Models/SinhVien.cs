namespace demoAPI.Models
{
    public class SinhVienMV
    {
        public string TenSinhVien { get; set; }
        public int NamSinh { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string SDT {  get; set; }
    }
    public class SinhVien : SinhVienMV
    {
        public Guid MaSinhVien { get; set; }
    }
}
