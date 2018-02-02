using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistencia;
using Modelo;
using Negocio;
using System.Drawing;
using System.IO;
using MejorPrecio4MVC.Models;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace MejorPrecio4MVC.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private ProductViewModel Traducir(Product product)
        {
            var prod = new ProductViewModel(); 
            prod.Name=product.Name;
            prod.BarCode=product.BarCode;
            prod.Price=product.Price;
            prod.Latitude=product.Latitude;
            prod.Longitude=product.Longitude;
            prod.Description=product.Description;
            prod.Day=product.Day;

            return prod;
        }
        
        // GET 
        private Product Traducir(ProductViewModel product)
        {
            var prod = new Product(); 
            prod.Name=product.Name;
            prod.BarCode=product.BarCode;
            prod.Price=product.Price;
            prod.Latitude=product.Latitude;
            prod.Longitude=product.Longitude;
            prod.Description=product.Description;
            prod.Day=product.Day;

            return prod;
        }

        [HttpGet("Upload")]
        public IActionResult Upload()
        {
            return View();
        }
 
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return IsItAdded(filePath);
        }

        // POST values
        [HttpPost("IsItAdded")]
        public IActionResult IsItAdded(string filePath)
        {
            var BarCode = new ReadImage().GetBarCodeFromToTheImage(filePath);

            var Exist = new ProductRepo().DoesTheProductExists(BarCode);
            
            if (BarCode== null) 
            {
                return this.Json($"No se pudo tener el Codigo de barras, imagen borrosa, intentelo de nuevo");
            }
            if (Exist)
            {
                return this.RedirectToAction("BestPrice","Price",new {id = BarCode});
            }
            else
            {
                return this.RedirectToAction("LoadPrice","Price",new {BarCode = BarCode});
            }
        } 

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values/5
        //llega el producto y precio
        [HttpPost("addProduct")]
        public IActionResult GetProduct([FromForm]ProductViewModel product)
        {
            var prod = this.Traducir(product);
            
            //manda al negocio de mati y fer
            new Prod().AddProductAndSendFer(prod);

            return this.StatusCode(201);

        }

        [HttpPost("UploadImage")]
        
            public ActionResult UploadImage (string image)
            {
                return this.Json("hhola");
            }
        
    }
}
