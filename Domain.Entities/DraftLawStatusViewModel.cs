using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public  class DraftLawStatusViewModel
    {
        public int draft_law_status_id { get; set; }
        public string name { get; set; }
        public bool notifiable { get; set; }
    }
}
