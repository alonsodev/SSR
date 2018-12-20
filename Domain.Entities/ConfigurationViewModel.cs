using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class ConfigurationViewModel : BaseViewModel
    {
        public int configuration_id { get; set; }
        

      
       
        [Display(Name = "Términos y Condiciones"), AllowHtml]
        [Required(ErrorMessage = "Términos y Condiciones es obligatorio.")]
        public string terms_conditions { get; set; }
        [Display(Name = "Excluir ponentes(Importación de Proyectos de Ley)")]
        public string exclude_speakers { get; set; }
        [Display(Name = "Quitar títulos de ponenetes (Importación de Proyectos de Ley)")]
        public string remove_titles_speaker { get; set; }





    }
}
