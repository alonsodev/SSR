using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Domain.Entities
{
    [XmlRootAttribute("CertificationHtmlViewModel")]
    [Serializable]
    public class CertificationHtmlViewModel
    {
        [XmlAttribute]
        public int concept_id { get; set; }
       
        public DateTime? fecha_presentacion { get; set; }
        [XmlAttribute]
        public string fecha_presentacion_txt { get; set; }
        [XmlAttribute]
        public string investigador { get; set; }
        [XmlAttribute]
        public string cedula { get; set; }
        [XmlAttribute]
        public string ciudad { get; set; }
        [XmlAttribute]
        public string institucion { get; set; }
        [XmlAttribute]
        public string grupo_vinculado { get; set; }
        [XmlAttribute]
        public string qr_url { get; set; }

        [XmlAttribute]
        public string margin_footer { get; set; }

        [XmlAttribute]
        public string draft_law_title { get; set; }

        [XmlAttribute]
        public int draft_law_id { get; set; }

   

        [XmlAttribute]
        public string fecha_aceptacion_txt { get; set; }
        [XmlAttribute]
        public string fecha_certificacion_txt { get; set; }

     

        public DateTime? fecha_aceptacion { get; set; }
        public DateTime? fecha_certificacion { get; set; }

        public string pdf_path { get; set; }
        public string base_url { get; set; }
        public Guid hash { get; set; }


        [XmlAttribute]
        public int day { get; set; }

        [XmlAttribute]
        public string day_name { get; set; }


        [XmlAttribute]
        public string month_name { get; set; }


        [XmlAttribute]
        public int year { get; set; }

        [XmlAttribute]
        public string year_name { get; set; }





    }
}
