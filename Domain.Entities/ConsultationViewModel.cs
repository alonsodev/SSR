﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class ConsultationViewModel : BaseViewModel
    {
       

        
        public int consultation_id { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "El Título es obligatorio.")]
        public string title { get; set; }
        [Display(Name = "Mensaje")]
        [Required(ErrorMessage = "El Mensaje es obligatorio.")]
        public string message { get; set; }
        

        [Display(Name = "Áreas de interés")]
        [Required(ErrorMessage = "Áreas de interés es obligatorio.")]
        public List<int> interest_areas { get; set; }

        [Display(Name = "Tipo de solicitud")]
        [Required(ErrorMessage = "Tipo de solicitud es obligatorio.")]
        public int? consultation_type_id { get; set; }

        public string consultation_type { get; set; }
        public string interest_areas_str { get; set; }

        public List<string> interest_areas_list { get; set; }
        [Display(Name = "Solicitante")]
        public string debate_speaker { get; set; }
        public MultiSelectList interest_areasMultiSelectList { get; set; }
    }
}
