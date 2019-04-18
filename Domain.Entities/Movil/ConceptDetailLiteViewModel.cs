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
    public class ConceptDetailLiteViewModel
    {
        public int calificado { get; set; }
        public int concept_id { get; set; }

        public string draft_law_title { get; set; }
        public int draft_law_number { get; set; }
        public string origin { get; set; }
        public string investigator { get; set; }
        public string institution { get; set; }
        public string summary { get; set; }

        public string concept { get; set; }
        public string bibliography { get; set; }

        public string pdf_file { get; set; }

        public string draft_law_link { get; set; }
        public string tags { get; set; }
        public List<string> tags_list { get; set; }
    }
}
