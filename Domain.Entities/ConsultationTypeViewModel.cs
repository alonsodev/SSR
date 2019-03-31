using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ConsultationTypeViewModel : BaseViewModel
    {
        public int consultation_type_id { get; set; }
        

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El Nombre  es obligatorio.")]
        public string name { get; set; }  

     
        

       
    }
}
