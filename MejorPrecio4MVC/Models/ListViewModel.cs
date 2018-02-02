using System;
using System.Collections.Generic;


namespace MejorPrecio4MVC.Models
{
    public class ListViewModel
    {
        public List<ProductViewModel> Lista {get; set;}

        public List<UserViewModel> Users { get; set; }
        public List<string> Coordenates {get; set;}

    }
}
