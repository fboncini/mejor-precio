using System;

namespace Modelo
{
    public class Product
    {
        public string Name { get; set; }
        public string BarCode { get; set; }
        public int Price { get; set; }//precio por centavos
        public string Latitude{ get; set; }
        public string Longitude{ get; set; }
        public string Description { get; set; }
        public DateTime Day { get; set; }
    }
}
