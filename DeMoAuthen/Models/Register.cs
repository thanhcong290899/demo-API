using System.ComponentModel.DataAnnotations;

namespace DeMoAuthen.Models
{
    public class Register
    {
        [Required]
        public string FirtName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;

        public string Email { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword {get; set; }

    }
}
