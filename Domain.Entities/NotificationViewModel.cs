using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NotificationViewModel : BaseViewModel
    {
        public int notification_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public string url { get; set; }
        public string message { get; set; }
        public Nullable<bool> notified { get; set; }
      








    }
}
