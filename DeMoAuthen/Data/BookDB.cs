using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeMoAuthen.Data
{
    [Table("Book")]
    public class BookDB
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price {  get; set; }
    }
}
