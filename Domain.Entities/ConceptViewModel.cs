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
    public class ConceptViewModel : BaseViewModel
    {
        public bool ValidarResumen()
        {
            return true;
        }
        public bool ValidarConcepto()
        {
            return true;
        }

        public string bad_languages { get; set; }

        public int concept_id { get; set; }
        public int draft_law_id { get; set; }

        [Display(Name = "Nro. de Proyecto de Ley")]
        public int draft_law_number { get; set; }

        [Display(Name = "Título de Proyecto de Ley")]
        public string title { get; set; }


          
        public string author { get; set; }  
        public Nullable<System.DateTime> date_presentation { get; set; }
        public string commission { get; set; }    
     
        public string status { get; set; }   

       
        public string interest_area { get; set; }      

        
        public string summary_draft_law { get; set; }
        [Display(Name = "Link de Texto Radicado"), AllowHtml]
        //[Required(ErrorMessage = "El Título es obligatorio.")]
        public string link { get; set; }
     
        [Display(Name = "Comisión")]        
        public Nullable<int> commission_id { get; set; }

        [Display(Name = "Resumen")]
        [Required(ErrorMessage = "El Resumen es obligatorio."), AllowHtml]
        [AssertThat("ValidarResumen()", ErrorMessage = "El Resumen debe contener máximo 200 palabras.")]
        public string summary { get; set; }

        [Display(Name = "Concepto")]
        [Required(ErrorMessage = "El Concepto es obligatorio."), AllowHtml]
        [AssertThat("ValidarConcepto()", ErrorMessage = "El Concepto debe contener máximo 3000 palabras.")]
        public string concept { get; set; }
      
        public Nullable<int> investigator_id { get; set; }

        [Display(Name = "Palabras Claves")]
        [Required(ErrorMessage = "Las Palabras Claves son obligatorio.")]        
        public List<string> tags { get; set; }

        public List<int> tag_ids { get; set; }
        
        [Display(Name = "Bibliografía")]              
        public string bibliography { get; set; }

    }
}
