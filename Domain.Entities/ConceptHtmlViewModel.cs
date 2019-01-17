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
        [XmlAttribute]
        public int draft_law_id { get; set; }


        
    }
}
