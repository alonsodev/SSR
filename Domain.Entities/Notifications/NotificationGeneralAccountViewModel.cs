using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Domain.Entities.Notifications
{
    [XmlRootAttribute("NotificationGeneralAccountViewModel")]
    [Serializable]
    public class NotificationGeneralAccountViewModel
    {


        [XmlAttribute]
        public string url_activar_cuenta { get; set; }
       

        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute]
        public string url_recuperar_cuenta { get; set; }

        [XmlAttribute]
        public string to { get; set; }
        [XmlAttribute]
        public string url_solicitud_concepto { get; set; }
        
    }
}
