using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CambiarPasswordViewModel
    {


        public int userd_id { get; set; }


        [Display(Name = "Contraseña anterior")]
        [Required(ErrorMessage = "La Contraseña anterior es obligatoria.")]
        [DataType(DataType.Password)]
        public string old_pass { get; set; }


        [Display(Name = "Nueva contraseña")]
        [Required(ErrorMessage = "La nueva contraseña es obligatoria.")]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,20}$", ErrorMessage = "La nueva contraseña debe tener al menos 8 caracteres, no más de 20, y debe incluir al menos una letra mayúscula, una letra minúscula y un dígito numérico.")]
        [DataType(DataType.Password)]
        public string new_pass { get; set; }

        [Display(Name = "Confirmar nueva contraseña")]
        [Required(ErrorMessage = "Confirmar nueva contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [AssertThat("new_pass==new_pass2", ErrorMessage = "Confirmar nueva contraseña no coincide con la contraseña ingresada previamente.")]
        public string new_pass2 { get; set; }

        public string user_code { get; set; }
    }
}
