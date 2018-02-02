using System;
using System.Collections.Generic;
using Modelo;
using Persistencia;

namespace Negocio
{
    public class Price
    {
       public List<Product> DeleteOld(List<Product> Lista)
        {

            int i = 0;
            while (i < Lista.Count)
            {
                if (Lista[i].Day < (DateTime.Today).AddDays(-30))
                {

                    Lista.RemoveAt(i);
                }
                else
                {
                    i++;
                }

            }
            return Lista;
        }

        public List<Product> mejoresPrecios(List<Product> ListaOrdenada)
        {
            List<Product> result = new List<Product>();
            ListaOrdenada = DeleteOld(ListaOrdenada);
            for (int i = 0; i < 10 && i < ListaOrdenada.Count; i++)
            {
               result.Add(ListaOrdenada[i]);
            }
            return result;
        }

        public List<string> GetLocations(List<Product> ListaDeProductos)
        {
            var Coordenates = new List<string>();
            for (int i = 0; i < ListaDeProductos.Count; i++)
            {
                Coordenates.Add("{lat: "+ListaDeProductos[i].Latitude);
                Coordenates.Add("lng: "+ListaDeProductos[i].Longitude+"}");
            }
            return Coordenates; 
        }

        public Product MejorPrecio(List<Product> ListaOrdenada)
        {
            Product pro = new Product();
            ListaOrdenada = DeleteOld(ListaOrdenada);
            if (ListaOrdenada.Count == 0)
                {
                
                }
                else{
                pro = ListaOrdenada[0];
                }
            return pro;
        }

        public void newPrice (Product product){
                var ap = new AddPrice();
                ap.newPrice(product);
            
        }
    }
}
