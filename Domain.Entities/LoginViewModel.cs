using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LoginViewModel
    {
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public string user_email { get; set; }




        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La Contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string user_pass { get; set; }
    }
}
