using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using Modelo;
using Persistencia;
using MejorPrecio4MVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace MejorPrecio4MVC.Controllers
{
    
    [Route("[controller]")]
    public class PriceController : Controller
    {
        private ProductViewModel Traducir(Product product)
       {
           return new ProductViewModel()
           {
               Name = product.Name,
               BarCode = product.BarCode,
               Price = product.Price,
               Latitude = product.Latitude,
               Longitude = product.Longitude,
               Description = product.Description,
               Day = product.Day
           };
       }
       private Product Traducir(ProductViewModel product)
       {
           return new Product()
           {
               Name = product.Name,
               BarCode = product.BarCode,
               Price = product.Price,
               Latitude = product.Latitude,
               Longitude = product.Longitude,
               Description = product.Description,
               Day = product.Day
           };
       }

        [Authorize(Roles="user,admin")]
        [HttpGet("LoadPrice/{BarCode}")]
        public IActionResult LoadPrice(string BarCode)
        {   
            var prod = new BuscarCodigo().SearchProduct(BarCode);
            prod.BarCode = BarCode;
            return this.View(Traducir(prod));
        }




        [Authorize(Roles="user,admin")]
        // POST MejorPrecioAPI/PriceController
        [HttpPost("LoadPrice/{BarCode}")]
        public IActionResult LoadPrice([FromForm] ProductViewModel product)
        {   
            
            if (ModelState.IsValid)
            {
                bool Exist = new ReadImage().BarCodeExist(product.BarCode);

                if (Exist)
                {
                    new Price().newPrice(Traducir(product));
                }
                else
                {
                    new Prod().AddProduct(Traducir(product));
                    new Price().newPrice(Traducir(product));
                }

                return RedirectToAction("BestPrice","Price", new {id = product.BarCode});
                
            }
            else
            {
                return this.View(product);
            }
        }

    
    

    
        [HttpGet("BestPrice/{id}")]
        public IActionResult BestPrice(string id)    
        {
            var productos = new BuscarCodigo().BuscarPorCodigo(id);
            if (productos != null)
            {
                var mp = new Price().MejorPrecio(productos);
                var ub = new Ubicacion().Distance(mp);
                ViewData["Ubicacion"] = $@"{ub}";
                ViewData["Maps"]= id;
                return View(Traducir(mp));
            }
            else
            {
                return this.StatusCode(404);
            }        
        }
        [HttpGet("maps/{id}")]
        public IActionResult maps(string id)    
        {
            var productos = new BuscarCodigo().BuscarPorCodigo(id);
            if (productos != null)
            {
                var mp = new Price().mejoresPrecios(productos); 
                var listView = new ListViewModel();
                listView.Lista = new List<ProductViewModel>();
                for (int i = 0; i<mp.Count;i++)
                {
                    listView.Lista.Add(Traducir(productos[i]));
                }
                
                listView.Coordenates= new Price().GetLocations(mp);

                return this.View(listView);
            }
            else
            {
                return this.StatusCode(404);
            }
        }
         [HttpGet("Coordinates/{id}")]
        public IActionResult Coordinates(string id)    
        {
            var productos = new BuscarCodigo().BuscarPorCodigo(id);
            if (productos != null)
            {
                var mp = new Price().mejoresPrecios(productos); 
                /* var listView = new ListProductViewModel(); */
                var Lista = new List<ProductViewModel>();
                for (int i = 0; i<mp.Count;i++)
                {
                    productos[i].Latitude = productos[i].Latitude.Replace(',','.');
                    productos[i].Longitude = productos[i].Longitude.Replace(',','.');
                    Lista.Add(Traducir(productos[i]));
                }
                return this.Json(Lista);
            }
            else
            {
                return this.StatusCode(404);
            }
        }    
    }
}



