﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RoleViewModel : BaseViewModel
    {
        public int role_id { get; set; }
        

        [Display(Name = "Nombre del Role")]
        [Required(ErrorMessage = "El Nombre del Role es obligatorio.")]
        public string role { get; set; }

      

     
        

       
    }
}