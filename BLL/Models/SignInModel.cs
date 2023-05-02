using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Should not be empty.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Should not be empty.")]
        public string Password { get; set; }
    }
}
