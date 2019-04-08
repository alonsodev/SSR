using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Domain.Entities.Movil
{
    [XmlRootAttribute("NotificationConceptMovil")]
    [Serializable]
    public class NotificationConceptMovil
    {

        [XmlAttribute]
        public string name { get; set; }


        [XmlAttribute]
        public string contact_data_name { get; set; }
       

        [XmlAttribute]
        public string contact_data_phone { get; set; }
        [XmlAttribute]
        public string contact_data_email { get; set; }

       

        [XmlAttribute]
        public string to { get; set; }


        [XmlAttribute]
        public string url_home { get; set; }

        [XmlAttribute]
        public string url_privacidad { get; set; }
        [XmlAttribute]
        public string url_contacto { get; set; }
        [XmlAttribute]
        public string url_politicas { get; set; }

        [XmlAttribute]
        public int concept_id { get; set; }
        

    }
}
