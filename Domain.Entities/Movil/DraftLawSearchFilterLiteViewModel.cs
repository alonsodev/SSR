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
    public class DraftLawSearchFilterLiteViewModel
    {
        public int period_id { get; set; }

        public int draft_law_number { get; set; }
        public string draft_law_title { get; set; }
        public string commission { get; set; }
        public string origin { get; set; }


    }
}
