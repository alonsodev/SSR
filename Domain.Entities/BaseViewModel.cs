using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BaseViewModel
    {
        public Nullable<System.DateTime> date_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<int> user_id_created { get; set; }
        public Nullable<int> user_id_modified { get; set; }

    }
}
