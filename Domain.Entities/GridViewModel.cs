using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GridModel<T>
    {
        
        public int page { get; set; }
        public int total { get; set; }
        public int recordsFiltered { get; set; }
        
        public List<T> rows { get; set; }
    }
}
