using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProgramViewModel : BaseViewModel
    {
        public int program_id { get; set; }
        

        [Display(Name = "Nombre de Programa")]
        [Required(ErrorMessage = "El Nombre del Programa es obligatorio.")]
        public string name { get; set; }

      

     
        

       
    }
}
