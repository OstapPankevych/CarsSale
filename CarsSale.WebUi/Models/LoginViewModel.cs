using System.ComponentModel.DataAnnotations;

namespace CarsSale.WebUi.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}