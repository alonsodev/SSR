using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Select2Model
    {
        public int total_count { get; set; }
        public List<Select2ItemModel> items { get; set; }
    }

    public class Select2ItemModel
    {
        public string id { get; set; }
        public string text { get; set; }
    }
}
