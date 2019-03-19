using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CurrentUserViewModel
    {
        public int user_id { get; set; }
        public string user_email { get; set; }
        public string name { get; set; }
        public string name_abbre { get; set; }
        public string pass { get; set; }
        public string avatar { get; set; }
        public int? role_id { get; set; }

        public int? status_id { get; set; }

        public int investigator_id { get; set; }
        
        public List<int> permissions { get; set; }

        public int first_time { get; set; }
        
    }
}
