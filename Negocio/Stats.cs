using System;
using System.Collections.Generic;
using Modelo;
using Persistencia;

namespace Negocio{
    public class Stats{
        public List<(String,int)> MorePrices(){

            var lista = new Count().CountPrices();            
            return lista;            
        }
    }
}
