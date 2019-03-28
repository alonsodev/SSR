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
        
            public bool ValidarTags()
        {
            return true;
        }
        public bool ValidarResumen()
        {
            return true;
        }
        public bool ValidarConcepto()
        {
            return true;
        }

        public bool ValidarRazonRechazo()
        {
            return true;
        }
        public bool ValidarRazonRechazoDescripcion()
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

        [Display(Name = "Concepto"), AllowHtml]
       // [Required(ErrorMessage = "El Concepto es obligatorio.")]
        [AssertThat("ValidarConcepto()", ErrorMessage = "El Concepto debe contener máximo 3000 palabras.")]
        public string concept { get; set; }
      
        public Nullable<int> investigator_id { get; set; }

        [Display(Name = "Palabras Claves")]
        
            //[AssertThat("ValidarTags()", ErrorMessage = "Las Palabras Claves son obligatorios.")]
        public List<string> tags { get; set; }
        public MultiSelectList tagsMultiSelectList { get; set; }
        public List<int> tag_ids { get; set; }
        
        [Display(Name = "Bibliografía")]              
        public string bibliography { get; set; }
        [Display(Name = "Motivo de Rechazo")]
        [RequiredIf("reject == 1", ErrorMessage = "El Motivo de Rechazo es obligatorio.")]
        //[AssertThat("ValidarRazonRechazo()", ErrorMessage = "El Motivo de Rechazo es obligatorio.")]
        public string reason_reject_id { get; set; }
        [Display(Name = "Comentario")]
        [RequiredIf("reject == 1", ErrorMessage = "La descripción es obligatoria.")]
       // [AssertThat("ValidarRazonRechazoDescripcion()", ErrorMessage = "La descripción es obligatoria.")]
        public string reason_reject_description { get; set; }


        public int reject { get; set; }
        [Display(Name = "Calificación")]
        [Required(ErrorMessage = "La Calificación es obligatoria.")]
        public Nullable<double> qualification { get; set; }

        public string investigator { get; set; }
        public string institution { get; set; }
        public int concept_status_id { get; set; }

        public string pdf_path { get; set; }
        public string certification_path { get; set; }

        [Display(Name = "Destinatarios")]
        public List<string> speakers { get; set; }
        public MultiSelectList speakersMultiSelectList { get; set; }
        public List<int> speakers_ids { get; set; }

        public Guid? hash { get; set; }

        public int existe_concepto { get; set; }
        public int period_closed { get; set; }

    }
}
