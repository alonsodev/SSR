using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MyHistoryViewModel
    {
        [Display(Name = "Conceptos calificados")]
        public int qualified_concepts { get; set; }

        [Display(Name = "Mis puntos")]
        public double? my_points { get; set; }
    }
}
