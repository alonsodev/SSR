using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MeritRangeViewModel : BaseViewModel
    {
        public int merit_range_id { get; set; }
        

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        public string name { get; set; }
        [Display(Name = "Límite inferior")]
        [Required(ErrorMessage = "El Límite inferior es obligatorio.")]
        public Nullable<int> lower_limit { get; set; }
        [Display(Name = "Límite superior")]
        [Required(ErrorMessage = "El Límite superior es obligatorio.")]
        public Nullable<int> upper_limit { get; set; }
        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "La Imagen es obligatorio.")]
        public string url_image { get; set; }





    }
}
