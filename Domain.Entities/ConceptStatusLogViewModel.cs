using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ConceptStatusLogViewModel
    {
        public Nullable<System.DateTime> date_created { get; set; }
        public Nullable<int> user_id_created { get; set; }
        public Nullable<int> reason_reject_id { get; set; }

        public int concept_id { get; set; }
        public int concept_status_id { get; set; }
        public string description { get; set; }
        public int qualification { get; set; }
        





    }
}
