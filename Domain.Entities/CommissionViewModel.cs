using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CommissionViewModel : BaseViewModel
    {
        public int commission_id { get; set; }
        

        [Display(Name = "Nombre de Comisión")]
        [Required(ErrorMessage = "El Nombre de la Comisión es obligatoria.")]
        public string name { get; set; }

      

     
        

       
    }
}
