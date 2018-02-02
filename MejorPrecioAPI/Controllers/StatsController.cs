using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using Modelo;
using Persistencia;

namespace precioMVc.Controllers
{
    [Route("api/stats")]
    public class PriceController : Controller
    {
        [HttpGet()]
        public IActionResult Get()    
        {
            var lista = new Stats().MorePrices(); 
            return this.Json(lista);
        }
    }
}