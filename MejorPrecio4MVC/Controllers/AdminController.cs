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
    public class AdminController : Controller
    {
        [Route("[controller]")]
        private User Traducir(UserViewModel user)
       {
           return new User()
           {
               Name = user.Name,
               LastName = user.LastName,
               DNI = user.DNI,
               Email = user.Email
           };
       }
       private UserViewModel Traducir(User user)
       {
           return new UserViewModel()
           {
               Name = user.Name,
               LastName = user.LastName,
               DNI = user.DNI,
               Email = user.Email
           };
       }

        [Authorize]
        public IActionResult Waiting()
        {
           var pendi =  new AccessUser().Pendientes();
           var userview = new ListViewModel();
            userview.Users = new List<UserViewModel>();
                for (int i = 0; i<pendi.Count;i++)
                {
                    userview.Users.Add(Traducir(pendi[i]));
                }
            return View("Waiting",userview);

        }
        [Authorize]
        [HttpGet("admin/Aceptar/{Email}")]
        public IActionResult Aceptar(string Email)
        {
            new AccessUser().cam(Email);
            return RedirectToAction("waiting");
        }
        
         [Authorize]
        [HttpGet("admin/Denegar/{Email}")]
        public IActionResult Denegar(string Email)
        {
            new AccessUser().Denegado(Email);
            return RedirectToAction("waiting");
        }
        
         [Authorize]
        public IActionResult Existentes()
        {
           var usuarios =  new AccessUser().AllUser();
           var userview = new ListViewModel();
            userview.Users = new List<UserViewModel>();
                for (int i = 0; i<usuarios.Count;i++)
                {
                    userview.Users.Add(Traducir(usuarios[i]));
                }
            return View("Existentes",userview);

        }
         [Authorize]
        [HttpGet("admin/Eliminar/{Email}")]
        public IActionResult Eliminar(string Email)
        {
            new AccessUser().Denegado(Email);
            return RedirectToAction("Existentes");
        }
    }
}