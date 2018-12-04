using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BadLanguageViewModel : BaseViewModel
    {
        public int bad_language_id { get; set; }
        

        [Display(Name = "Palabra no adecuada")]
        [Required(ErrorMessage = "La Palabra no adecuada es obligatorio.")]
        public string name { get; set; }

      

     
        

       
    }
}
