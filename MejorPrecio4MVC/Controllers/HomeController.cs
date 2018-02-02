using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MejorPrecio4MVC;
using Negocio;
using Persistencia;
using Modelo;
using MejorPrecio4MVC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MejorPrecio4MVC.Controllers
{
    public class HomeController : Controller
    {

       public IActionResult Index ()
       {
           if (!User.Identity.IsAuthenticated)
           {
               return View();
           }
           else
           {
                var claims = User.Claims.Select(claim => new {claim.Value}).ToArray()[1].Value;

                switch (claims)
                {
                    case "admin":
                        return RedirectToAction("Waiting","Admin");
                    default:
                        return RedirectToAction("Upload","Product");
                }
           }
       }

    }
}