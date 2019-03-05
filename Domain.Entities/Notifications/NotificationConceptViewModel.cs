using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Domain.Entities.Notifications
{
    [XmlRootAttribute("NotificationConceptViewModel")]
    [Serializable]
    public class NotificationConceptViewModel
    {


        [XmlAttribute]
        public string url_view_concept { get; set; }
       

        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute]
        public string url_calificar_concept { get; set; }

        [XmlAttribute]
        public string url_edit_concept { get; set; }
        [XmlAttribute]
        public string url_list_draft_law { get; set; }

        [XmlAttribute]
        public string to { get; set; }

       
    }
}
