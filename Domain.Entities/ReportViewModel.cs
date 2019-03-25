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
    public class ReportViewModel
    {
       
        [Display(Name = "Nro. de Proyecto de Ley")]
        public int draft_law_number { get; set; }
        

        public string title { get; set; }

        public int number_approved_concepts { get; set; }

        public string interest_area { get; set; }
        public string commission { get; set; }

        public string status { get; set; }
        public string origin { get; set; }

        public string institution { get; set; }
        public string investigator { get; set; }
        public string gender { get; set; }

        public int age { get; set; }
        public string nationality { get; set; }
        
        public string program { get; set; }
        public List<string> interest_areas { get; set; }
        public string address { get; set; }
        public string institution_support { get; set; }
        public string investigation_group { get; set; }
        public int approved_concepts { get; set; }
        public int reject_concepts { get; set; }
        public int qualified_concepts { get; set; }
        public string ranking { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string movil { get; set; }
        public DateTime? birthdate { get; set; }
        public int? position { get; set; }
        public string interest_areas_str{ get; set; }
    }
}
