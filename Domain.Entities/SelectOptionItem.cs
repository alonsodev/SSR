﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SelectOptionItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public string AdditionalField { get; set; }

        public SelectOptionItem(string Text, string Value)
        {
            this.Value = Value;
            this.Text = Text;
        }
        public SelectOptionItem()
        {
           
        }
    }
}
