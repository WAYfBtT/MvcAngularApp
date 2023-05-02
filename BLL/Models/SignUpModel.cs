using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "Should not be empty.")]
        [MinLength(5, ErrorMessage = "Not less than 5 letters.")]
        [MaxLength(64, ErrorMessage = "No more than 64 letters.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Should not be empty.")]
        [MinLength(8, ErrorMessage = "Not less than 8 letters.")]
        [MaxLength(128, ErrorMessage = "Not less than 128 letters.")]
        public string Password { get; set; }
    }
}
