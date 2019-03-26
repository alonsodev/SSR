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
    public class ReportFilterViewModel
    {       
        [Display(Name = "Área de interés")]
        public int interest_area_id { get; set; }

        [Display(Name = "Comisión")]
        public int commission_id { get; set; }

        [Display(Name = "Estado")]
        public int  status_id { get; set; }

        [Display(Name = "Origen")]
        public string origin_id { get; set; }

        [Display(Name = "Periodo")]
        public int period_id { get; set; }

        public int institution_id { get; set; }

    }
}
