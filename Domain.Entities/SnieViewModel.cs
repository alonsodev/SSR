using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SnieViewModel : BaseViewModel
    {
        public int snie_id { get; set; }
        [Display(Name = "Institución educativa")]
        [Required(ErrorMessage = "El Institución educativa es obligatorio.")]
        public Nullable<int> educational_institution_id { get; set; }
        public string educational_institution { get; set; }

        [Display(Name = "Área de Conocimiento")]
        [Required(ErrorMessage = "El Área de Conocimiento es obligatorio.")]
        public Nullable<int> knowledge_area_id { get; set; }
        public string knowledge_area { get; set; }

        [Display(Name = "Programa")]
        [Required(ErrorMessage = "El Programa es obligatorio.")]
        public Nullable<int> program_id { get; set; }
        public string program { get; set; }

        [Display(Name = "Nivel Académico")]
        [Required(ErrorMessage = "El Nivel Académico es obligatorio.")]
        public Nullable<int> academic_level_id { get; set; }
        public string academic_level { get; set; }

        [Display(Name = "Nivel de Formación")]
        [Required(ErrorMessage = "El Nivel de Formación es obligatorio.")]
        public Nullable<int> education_level_id { get; set; }
        public string education_level { get; set; }

        [Display(Name = "Título Otorgado")]
        [Required(ErrorMessage = "El Título Otorgado es obligatorio.")]
        public string name { get; set; }







    }
}
