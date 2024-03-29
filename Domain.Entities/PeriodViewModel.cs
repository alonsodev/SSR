﻿using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PeriodViewModel : BaseViewModel
    {
        public int period_id { get; set; }

        public bool ValidarFechaFin()
        {
            return true;
        }
        [Display(Name = "Nombre de Periodo")]
        [Required(ErrorMessage = "El Nombre del Periodo es obligatorio.")]
        public string name { get; set; }
        [Display(Name = "Inicio")]
        [Required(ErrorMessage = "El Inicio del Periodo es obligatorio.")]

        public string start_date_text { get; set; }


        public Nullable<System.DateTime> start_date { get; set; }
        [Display(Name = "Fin")]
        [Required(ErrorMessage = "El Fin del Periodo es obligatorio.")]
        [AssertThat("ValidarFechaFin()", ErrorMessage = "La fecha fin del periodo debe ser mayor a la fecha inicio del periodo.")]
        public string end_date_text { get; set; }


        public Nullable<System.DateTime> end_date { get; set; }





    }
}
