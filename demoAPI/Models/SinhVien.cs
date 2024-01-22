namespace demoAPI.Models
{
    public class SinhVienMv
    {
        public string? TenSV { get; set; }
        public string? DiaChi { get; set; }
        public string? Email { get; set; }
        public string? SDT { get; set; }

    }
    public class SinhVien : SinhVienMv
    {
        public Guid MaSinhVien { get; set; }
    }
}
