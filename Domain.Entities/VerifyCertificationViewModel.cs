using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class VerifyCertificationViewModel : BaseViewModel
    {
        [Display(Name = "Nro. de concepto")]
        public int concept_id { get; set; }
        

        [Display(Name = "Investigador")]        
        public string investigador { get; set; }

        [Display(Name = "Cédula de ciudadanía")]        
        public string cedula { get; set; }

        [Display(Name = "Institución")]
        public string institucion { get; set; }


        [Display(Name = "Grupo de Investigación")]
        public string grupo { get; set; }

        [Display(Name = "Código de grupo")]
        public string codigo_grupo { get; set; }

        [Display(Name = "Fecha de presentación del concepto")]
        public string fecha_presentacion_concepto { get; set; }



        [Display(Name = "Nro. proyecto de ley")]
        public string nro_proyecto { get; set; }

        [Display(Name = "Título de proyecto de ley")]
        public string titulo_proyecto { get; set; }


        [Display(Name = "Fecha de presentación del concepto")]
        public DateTime fecha_presentacion { get; set; }
        public int concept_status_id { get; set; }
        
    }
}
