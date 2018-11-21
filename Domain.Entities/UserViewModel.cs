using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserViewModel : BaseViewModel
    {
        public long Id { get; set; }
        public Nullable<byte> IsSuperAdmin { get; set; }
        public Nullable<DateTime> UserDateLastLogin { get; set; }

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string UserEmail { get; set; }

        [Display(Name = "Clave")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string UserPass { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int UserRoleId { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int UserStatusId { get; set; }
    }
}
