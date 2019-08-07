using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UsersReportFilterViewModel
    {
        [Display(Name = "roles")]
        public int role_id { get; set; }
    }
}
