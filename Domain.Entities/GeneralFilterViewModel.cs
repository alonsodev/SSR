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
    public class GeneralFilterViewModel
    {       
       

        [Display(Name = "Periodo legislativo")]
        public int period_id { get; set; }


        [Display(Name = "Número de proyecto de ley")]
        public string draft_law_number { get; set; }

        [Display(Name = "Título de proyecto de ley")]
        public string  draft_law_title { get; set; }

        [Display(Name = "Comisión Constitucional de Interés")]
        public int commission_id { get; set; }

        [Display(Name = "Origen")]
        public int  origin_id { get; set; }

        [Display(Name = "Palabra clave")]
        public int tag_id { get; set; }

        [Display(Name = "Área de interés")]
        public int interest_area_id { get; set; }
    }
}
