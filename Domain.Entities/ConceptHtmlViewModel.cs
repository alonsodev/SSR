using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Domain.Entities
{
    [XmlRootAttribute("ConceptHtmlViewModel")]
    [Serializable]
    public class ConceptHtmlViewModel
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
        public string institucion { get; set; }
        [XmlAttribute]
        public string grupo_vinculado { get; set; }
        [XmlAttribute]
        public string codigo_grupo_vinculado { get; set; }
        [XmlAttribute]
        public string fecha_elaboracion_txt { get; set; }

        [XmlAttribute]
        public string concept { get; set; }

        [XmlAttribute]
        public string summary { get; set; }

        [XmlAttribute]
        public string bibliography { get; set; }

        [XmlAttribute]
        public string draft_law_title { get; set; }

        [XmlAttribute]
        public int draft_law_id { get; set; }
        [XmlAttribute]
        public int draft_law_number { get; set; }


        [XmlAttribute]
        public string draf_law_origen { get; set; }

        [XmlAttribute]
        public string draf_law_fecha_presentacion_txt { get; set; }

        [XmlAttribute]
        public string draf_law_commission { get; set; }

        [XmlAttribute]
        public string draf_law_interested_area { get; set; }
        public DateTime? draf_law_fecha_presentacion { get; set; }

        public DateTime? fecha_elaboracion { get; set; }

        public string pdf_path { get; set; }
        public string base_url { get; set; }
    }
}
