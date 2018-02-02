using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MejorPrecio4MVC.Models
{
    public class UserViewModel
    {

        public string Name { get; set; }

        public string LastName { get; set; }
        
        [Required]
        public int DNI { get; set; }

        [Required]
        public string Email { get; set; }
        public Guid AccessCode { get; set; }
        public string Rol {get; set; }

    

    }
    
}