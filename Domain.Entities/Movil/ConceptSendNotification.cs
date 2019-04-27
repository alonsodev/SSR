using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities.Movil
{
    public class ConceptSendNotification
    {
        public int concept_id { get; set; }

     
        public int user_id { get; set; }
        public int solicitud_datos_investigador { get; set; }
        public int solicitud_ampliacion { get; set; }
        public string message { get; set; }
    }
}
