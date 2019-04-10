using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace Domain.Entities
{
    [XmlRootAttribute("DraftLawViewModel")]
    [Serializable]
    public class DraftLawViewModel : BaseViewModel
    {
        [XmlAttribute]
        public int draft_law_id { get; set; }

        [Display(Name = "Nro. de Proyecto")]
        [Required(ErrorMessage = "El Nro. de Proyecto es obligatorio.")]
        [XmlAttribute]
        public int draft_law_number { get; set; }

        
        [Display(Name = "Título")]
        [Required(ErrorMessage = "El Título es obligatorio.")]
        [XmlAttribute]
        public string title { get; set; }


        [Display(Name = "Autor")]
        [Required(ErrorMessage = "El Autor es obligatorio.")]
        public string author { get; set; }

        [Display(Name = "Origen")]
        [Required(ErrorMessage = "El Origen es obligatorio.")]
        public int? origin_id { get; set; }

        [Display(Name = "Origen")]
        [Required(ErrorMessage = "El Origen es obligatorio.")]
        public string origin { get; set; }

        [Display(Name = "Fecha Presentación")]
        [Required(ErrorMessage = "La Fecha Presentación es obligatoria.")]
        public string date_presentation_text { get; set; }

        public Nullable<System.DateTime> date_presentation { get; set; }
        
        [Display(Name = "Comision")]
        [Required(ErrorMessage = "La Comision es obligatoria.")]
        public Nullable<int> commission_id { get; set; }
        [XmlAttribute]
        public string commission { get; set; }
        [Display(Name = "Ponente de debate 1")]
        [Required(ErrorMessage = "El Ponente de debate 1 es obligatorio.")]
        public string debate_speaker { get; set; }

        [Display(Name = "Ponente de debate 2")]
       // [Required(ErrorMessage = "El Ponente de debate 2 es obligatorio.")]
        public string debate_speaker2 { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El Estado es obligatorio.")]
        [XmlAttribute]
        public string status { get; set; }

        [Display(Name = "Comentario de estado")]
        //[Required(ErrorMessage = "El Comentario de estado es obligatorio.")]
        public string status_comment { get; set; }

        [Display(Name = "Área de Interés")]
        [Required(ErrorMessage = "El Área de Interés es obligatorio.")]
        public Nullable<int> interest_area_id { get; set; }
        [XmlAttribute]
        public string interest_area { get; set; }

        [Display(Name = "Iniciativa")]
        [Required(ErrorMessage = "La Iniciativa es obligatoria.")]
        public string initiative { get; set; }

        [Display(Name = "Resumen")]
        [Required(ErrorMessage = "El Resumen es obligatorio."), AllowHtml]
        public string summary { get; set; }
        [Display(Name = "Link de Texto Radicado"), AllowHtml]
        //[Required(ErrorMessage = "El Título es obligatorio.")]
        public string link { get; set; }

        public List<int> debate_speakers { get; set; }


        public int draft_law_status_id { get; set; }

        public int period_id { get; set; }

        public int period_closed { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }

        public bool notifiable { get; set; }
        public string sub_type { get; set; }
    }
}
