using System;
using System.ComponentModel.DataAnnotations;

namespace MejorPrecio4MVC.Models
{
    public class ProductViewModel
    {   
        [Required]  
        public string Name { get; set; }
        [Required]
        public string BarCode { get; set; }
        [Required]
        public int Price { get; set; }//precio por centavos
        [Required]
        public string Latitude{ get; set; }
        [Required]
        public string Longitude{ get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime Day { get; set; }        

    }
}
