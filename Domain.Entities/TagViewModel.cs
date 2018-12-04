using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TagViewModel : BaseViewModel
    {
        public int tag_id { get; set; }
        

        [Display(Name = "Palabra clave")]
        [Required(ErrorMessage = "La Palabra clave es obligatoria.")]
        public string name { get; set; }

      

     
        

       
    }
}
