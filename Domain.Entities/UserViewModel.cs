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
       



        public int id { get; set; }
        

        [Display(Name = "Nombre Completo")]
        [Required(ErrorMessage = "la Contraseña es obligatorio.")]
        public string user_name { get; set; }

        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "El Correo Electrónico es obligatorio.")]
        public string user_email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La Contraseña es obligatorio.")]
        public string user_pass { get; set; }

       


        [Display(Name = "Rol de Usuario")]
        [Required(ErrorMessage = "El Rol de Usuario es obligatorio.")]
        public Nullable<int> user_role_id { get; set; }
        public string user_role { get; set; }

        [Display(Name = "Estatus del Usuario")]
        [Required(ErrorMessage = "Estatus del Usuario es obligatorio.")]
        public Nullable<int> user_status_id { get; set; }
        public string user_status { get; set; }

        public Nullable<byte> is_super_admin { get; set; }
        public Nullable<System.DateTime> user_date_last_login { get; set; }


    }
}
