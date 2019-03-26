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


        [Display(Name = "Nombre o Institución")]
        [Required(ErrorMessage = "Nombre o Institución es obligatorio.")]        
        public string user_name { get; set; }   


        

        public Nullable<byte> is_super_admin { get; set; }
        public Nullable<System.DateTime> user_date_last_login { get; set; }


     

        [Display(Name = "Tipo de Documento")]
        [Required(ErrorMessage = "Tipo de Documento es obligatorio.")]
        public Nullable<int> document_type_id { get; set; }
        public string document_type { get; set; }

        [Display(Name = "Número de Documento")]
        [Required(ErrorMessage = "Número de Documento es obligatorio.")]
        public string doc_nro { get; set; }

        [Display(Name = "Nacionalidad")]
        [Required(ErrorMessage = "Nacionalidad es obligatorio.")]
        public Nullable<int> nationality_id { get; set; }

        [Display(Name = "Nombre de contacto")]
        [Required(ErrorMessage = "Nombre de contacto es obligatorio.")]
        public string contact_name { get; set; }

        [Display(Name = "Telefóno de contacto")]
        [Required(ErrorMessage = "Telefóno de contacto es obligatorio.")]
        public string phone { get; set; }
        [Display(Name = "Dirección - Lugar de residencia")]
        [Required(ErrorMessage = "Dirección - Lugar de residencia es obligatorio.")]
        public string address { get; set; }


        [Display(Name = "Correo electrónico usuario")]
        [Required(ErrorMessage = "El Correo electrónico usuario es obligatorio.")]
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
        public string user_code_activate { get; set; }
        public string user_code_recover { get; set; }
        public string avatar { get; set; }

        
    }
}
