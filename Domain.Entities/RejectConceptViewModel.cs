using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RejectConceptViewModel : BaseViewModel
    {
       
        [Display(Name = "Motivo de Rechazo")]
        //[RequiredIf("reject == 1", ErrorMessage = "El Motivo de Rechazo es obligatorio.")]
        //[AssertThat("ValidarRazonRechazo()", ErrorMessage = "El Motivo de Rechazo es obligatorio.")]
        public int? reason_reject_id { get; set; }
        [Display(Name = "Comentario")]
        //[RequiredIf("reject == 1", ErrorMessage = "La descripción es obligatoria.")]
        // [AssertThat("ValidarRazonRechazoDescripcion()", ErrorMessage = "La descripción es obligatoria.")]
        public string reason_reject_description { get; set; }


        public string reason_reject { get; set; }

        [Display(Name = "Periodo")]
        public int period_id { get; set; }
        


    }
}
