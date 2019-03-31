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
       

        [Display(Name = "Periodo")]
        public int period_id { get; set; }


    }
}
