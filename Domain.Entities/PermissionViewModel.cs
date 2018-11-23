using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PermissionViewModel : BaseViewModel
    {
        public int id_permission { get; set; }
        

        [Display(Name = "Nombre del Permiso")]
        [Required(ErrorMessage = "El Nombre del Permiso es obligatorio.")]
        public string title { get; set; }

        [Display(Name = "Llave del Permiso")]
        [Required(ErrorMessage = "La Llave del Permiso es obligatorio.")]
        public string name { get; set; }
        public bool is_only_for_super_admin { get; set; }

       
    }
}
