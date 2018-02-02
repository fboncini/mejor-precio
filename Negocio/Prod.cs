using System;
using Modelo;
using Persistencia;
using System.Collections.Generic;

namespace Negocio
{
    public class Prod
    {
        public void AddProductAndSendFer(Product product)
        {
            this.AddProduct(product);
            new Call().SendProductFer(product);
        }
        public void AddProduct(Product product) 
        {
            if (String.IsNullOrWhiteSpace(product.Name)) {
                throw new Exception("El nombre es requerido"); 
            }

            if (String.IsNullOrWhiteSpace(product.BarCode)) {
                throw new Exception("El Codigo de barras es requerido"); 
            }
            //if (ListAvailableProducts().Exists(x=> x ==product))
            var repo = new ProductRepo(); 
            repo.newProduct(product); 

        }
    }
}
