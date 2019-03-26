using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Domain.Entities
{


    [XmlRootAttribute("NotificationDraftLawViewModel")]
    [Serializable]
    public class NotificationDraftLawViewModel
    {


        [XmlAttribute]
        public string url { get; set; }


        [XmlAttribute]
        public string name { get; set; }


        [XmlAttribute]
        public string to { get; set; }




        public List<DraftLawViewModel> DraftLaws { get; set; }


        [XmlAttribute]
        public string url_home { get; set; }

        [XmlAttribute]
        public string url_privacidad { get; set; }
        [XmlAttribute]
        public string url_contacto { get; set; }
        [XmlAttribute]
        public string url_politicas { get; set; }

    }
}
