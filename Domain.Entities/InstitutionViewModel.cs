using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InstitutionViewModel : BaseViewModel
    {
        public int institution_id { get; set; }
        

        [Display(Name = "Nombre de Institución")]
        [Required(ErrorMessage = "El Nombre de la Institución es obligatoria.")]
        public string name { get; set; }

      

     
        

       
    }
}
