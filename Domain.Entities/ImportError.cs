﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ImportError
    {
        public int nroFila { get; set; }
        public List<string> errores { get; set; }
    }
}
