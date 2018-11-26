using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InvestigatorViewModel : BaseViewModel
    {



        public int investigator_id { get; set; }

        public Nullable<int> user_id { get; set; }


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

        [Display(Name = "Primer Nombre")]
        [Required(ErrorMessage = "Primer Nombre es obligatorio.")]
        public string first_name { get; set; }

        [Display(Name = "Segundo Nombre")]
        [Required(ErrorMessage = "Segundo Nombre es obligatorio.")]

        public string second_name { get; set; }

        [Display(Name = "Primer Apellido")]
        [Required(ErrorMessage = "Primer Apellido es obligatorio.")]
        
        public string last_name { get; set; }

        [Display(Name = "Segundo Apellido")]
        [Required(ErrorMessage = "Segundo Apellido es obligatorio.")]

        public string second_last_name { get; set; }
        [Display(Name = "Género")]
        [Required(ErrorMessage = "Género es obligatorio.")]
        public Nullable<int> gender_id { get; set; }

        [Display(Name = "Telefóno de contacto")]
        [Required(ErrorMessage = "Telefóno de contacto es obligatorio.")]
        public string phone { get; set; }
        [Display(Name = "Telefóno Celular")]
        [Required(ErrorMessage = "Telefóno Celular es obligatorio.")]
        public string mobile_phone { get; set; }


        [Display(Name = "Fecha de Nacimiento")]
        [Required(ErrorMessage = "Fecha de Nacimiento es obligatorio.")]
        //[DataType(DataType.Date)]
//        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> birthdate { get; set; }

        [Display(Name = "Nacionalidad")]
        [Required(ErrorMessage = "Nacionalidad es obligatorio.")]
        public Nullable<int> nationality_id { get; set; }

      

      
        [Display(Name = "Lugar de residencia")]
        [Required(ErrorMessage = "Lugar de residencia es obligatorio.")]
        public string address { get; set; }


        [Display(Name = "Correo electrónico usuario")]
        [Required(ErrorMessage = "El Correo electrónico usuario es obligatorio.")]
        public string user_email { get; set; }

      


      
      
       
     
     
       
    }
}
