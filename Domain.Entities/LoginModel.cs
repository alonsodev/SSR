using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class LoginModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Usuario")]
        public string usuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "La contraseña ingresada no es válida. Debe tener como mínimo 6 caractares.")]
        [Display(Name = "Contraseña")]
        public string clave { get; set; }
    }
}
