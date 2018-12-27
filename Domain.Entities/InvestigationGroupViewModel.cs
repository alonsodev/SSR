using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InvestigationGroupViewModel : BaseViewModel
    {
        public int investigation_group_id { get; set; }

        [Display(Name = "Institución")]
        [Required(ErrorMessage = "La Institución es obligatoria.")]

        public int institution_id { get; set; }
        public string institution { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        public string name { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "El Código es obligatorio.")]
        public string code { get; set; }
        




    }
}
