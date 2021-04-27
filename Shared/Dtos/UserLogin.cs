using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorRPG.Shared.Dtos
{
    public class UserLogin
    {
        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Parola obligatorie")]
        public string Password { get; set; }
    }
}
