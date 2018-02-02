using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistencia;
using Modelo;
using Negocio;

namespace MejorPrecioAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        /* [HttpPost]
        public IActionResult Post([FromBody]string ubicImage)
        {
/*             product.BarCode=2;
            product.Coordinates=(14,3);
            product.Day=DateTime.Today.Date;
            product.Description="algo";
            product.Name="algoCopo";
            product.Price=20; 

            new Negocio.Prod().IfIsPossibleAddProduct(ubicImage);

            return this.StatusCode(201);
        } */

        // POST api/values
/*         [HttpPost("IsItAdded")]
        public IActionResult GetImage()
        {
            //var Exist = new ReadImage().isItAddedToTheDatabase(this.Request.Body);

            return this.Json(Exist);
            //this.Json(Exist);
        } */

        // POST api/values/5
        [HttpPost("addProduct")]
        public IActionResult GetProduct([FromBody]Product product)
        {
            new Prod().AddProductAndSendFer(product);

            return this.StatusCode(201);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
