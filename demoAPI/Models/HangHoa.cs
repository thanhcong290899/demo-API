namespace demoAPI.Models
{
    public class HangHoaCT
    {
        public string TenHangHoa { get; set; }
        public double DonGia { get; set; }
    }
    public class HangHoa : HangHoaCT 
    {
        public Guid MaHangHoa { get; set; }
       
    }
}
