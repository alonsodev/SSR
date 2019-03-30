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
    public class ConceptLiteViewModel 
    {
        public int concept_id { get; set; }           
        public string draft_law_status { get; set; }
        public int? draft_law_status_id { get; set; }       
        public string summary { get; set; }
        public string sub_type { get; set; }
        
    }
}
