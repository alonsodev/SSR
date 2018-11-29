using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InterestAreaViewModel : BaseViewModel
    {
        
        public int interest_area_id { get; set; }
        [Display(Name = "Grupo de investigación")]
        [Required(ErrorMessage = "El Grupo de investigación es obligatorio.")]

        public int investigation_group_id { get; set; }
        public string investigation_group { get; set; }

        [Display(Name = "Institución")]
        [Required(ErrorMessage = "La Institución es obligatoria.")]

        public int institution_id { get; set; }
        public string institution { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        public string name { get; set; }

      

     
        

       
    }
}
