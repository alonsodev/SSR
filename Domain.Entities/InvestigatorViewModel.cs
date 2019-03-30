using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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
        [RegularExpression(@"^[0-9]{8,20}", ErrorMessage = "Número de Documento solo permite números mayores a 8 dígitos")]
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
        [RegularExpression(@"^[0-9]{7,20}", ErrorMessage = "Telefóno de contacto solo permite números mayores a 7 dígitos")]
        public string phone { get; set; }
        [Display(Name = "Telefóno Celular")]
        [Required(ErrorMessage = "Telefóno Celular es obligatorio.")]
        [RegularExpression(@"^[0-9]{10,20}", ErrorMessage = "Telefóno Celular solo permite números mayores a 10 dígitos")]
        public string mobile_phone { get; set; }


        [Display(Name = "Fecha de Nacimiento")]
        [Required(ErrorMessage = "Fecha de Nacimiento es obligatorio.")]
        //[DataType(DataType.Date)]
//        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> birthdate { get; set; }
        [Display(Name = "Fecha de Nacimiento")]
        [Required(ErrorMessage = "Fecha de Nacimiento es obligatorio.")]
        public string birthdate_text { get; set; }

        [Display(Name = "Nacionalidad")]
        [Required(ErrorMessage = "Nacionalidad es obligatorio.")]
        public Nullable<int> nationality_id { get; set; }



        public string contact_name { get; set; }
        [Display(Name = "Nivel Académico")]
        [Required(ErrorMessage = "Nivel Académico es obligatorio.")]
        public Nullable<int> academic_level_id { get; set; }

        [Display(Name = "Comisión")]
        [Required(ErrorMessage = "Comisión es obligatorio.")]
        public List<int> commissions { get; set; }
        public MultiSelectList commissionsMultiSelectList { get; set; }
        [Display(Name = "Pais de residencia")]
        [Required(ErrorMessage = "Pais de residencia es obligatorio.")]
        public Nullable<int> address_country_id { get; set; }

        [Display(Name = "Departamento de residencia")]
        [RequiredIf("address_country_id == 45", ErrorMessage = "Departamento de residencia es obligatorio.")]
        public Nullable<int> department_id { get; set; }

        [Display(Name = "Municipalidad de residencia")]
        
        [RequiredIf("address_country_id == 45", ErrorMessage = "Municipalidad de residencia es obligatorio.")]
        public Nullable<int> address_municipality_id { get; set; }

        [Display(Name = "Lugar de residencia")]
        [Required(ErrorMessage = "Lugar de residencia es obligatorio.")]
        public string address { get; set; }


        [Display(Name = "Correo electrónico usuario")]
        [Required(ErrorMessage = "El Correo electrónico usuario es obligatorio.")]
        public string user_email { get; set; }




        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La Contraseña es obligatoria.")]
        //[StringLength(20, MinimumLength =8, ErrorMessage = "La contraseña debe tener como mínimo 8 caracteres y como máximo 20 caracteres")]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,20}$", ErrorMessage = "La contraseña debe tener al menos 8 caracteres, no más de 20, y debe incluir al menos una letra mayúscula, una letra minúscula y un dígito numérico.")]
        [DataType(DataType.Password)]
        public string user_pass { get; set; }

        [Display(Name = "Confirmar Contraseña")]
        [Required(ErrorMessage = "Confirmar Contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [AssertThat("user_pass2==user_pass", ErrorMessage = "Confirmar Contraseña no coincide con la contraseña ingresada previamente.")]
        public string user_pass2 { get; set; }

        [Display(Name = "Nombre de institución que lo avala")]
        [Required(ErrorMessage = "Nombre de institución que lo avala es obligatorio.")]
        public Nullable<int> institution_id { get; set; }
        public string institution { get; set; }
        [Display(Name = "Grupo de investigación a la que pertenece")]
        [Required(ErrorMessage = "Grupo de investigación a la que pertenece es obligatorio.")]
        public Nullable<int> investigation_group_id { get; set; }

      

        [Display(Name = "Código del grupo")]
        public string code_investigation_group { get; set; }

       

        [Display(Name = "Áreas de interés")]
        [Required(ErrorMessage = "Áreas de interés es obligatorio.")]
        public List<int> interest_areas { get; set; }

        [Display(Name = "Egresado de la institución educativa")]
        [Required(ErrorMessage = "Egresado de la institución educativa es obligatorio.")]
        public Nullable<int> educational_institution_id { get; set; }

        [Display(Name = "Nombre de programa")]
        [Required(ErrorMessage = "Nombre de programa es obligatoria.")]
        public Nullable<int> program_id { get; set; } 

        [Display(Name = "Nivel de formación")]
        [Required(ErrorMessage = "Nivel de formación es obligatorio.")]
        public Nullable<int> education_level_id { get; set; }


        [Display(Name = "URL CVLAC")]
        [Required(ErrorMessage = "URL CVLAC es obligatorio.")]
        
        [Url(ErrorMessage = "URL CVLAC no cumple con el formato URL.")]
        public string CVLAC { get; set; }
        public string user_code_activate { get; set; }
        public string user_code_recover { get; set; }
        public string avatar { get; set; }
        public string terms_conditions { get; set; }
    }
}
