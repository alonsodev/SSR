using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Domain.Entities.Notifications
{
    [XmlRootAttribute("NotificacionConfig")]
    [Serializable]
    public class NotificacionConfig
    {
        public string To { get; set; }

        public string xslPath { get; set; }
        public string Asunto { get; set; }
        public string From { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
          

        public NotificacionConfig(string tipo)
        {
            this.Asunto = ConfigurationManager.AppSettings[tipo + ".asunto"];
            this.From = ConfigurationManager.AppSettings[tipo + ".from"];
            this.To = ConfigurationManager.AppSettings[tipo + ".to"];
            this.Cc = ConfigurationManager.AppSettings[tipo + ".cc"];
            this.Bcc = ConfigurationManager.AppSettings[tipo + ".bcc"];
            this.xslPath = ConfigurationManager.AppSettings[tipo + ".xslPath"];


        }
    }
}
