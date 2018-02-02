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
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace MejorPrecio4MVC.Controllers
{
    [Route("[Controller]")]
    public class InicioController : Controller
    {
        private User Traduce(UserViewModel user)
        {
            return new User()
            {
                Name = user.Name,
                LastName = user.LastName,
                DNI = user.DNI,
                Email = user.Email
            };
        }
        
        public IActionResult Login()
        {
            return View();
        }
        
         // Post User/
        [HttpPost("Login")]
        public async Task<IActionResult> AddLogin([FromForm]UserViewModel user)
        {

            if (!ModelState.IsValid)
            {
                return View("Login");
            }else{
                    var usermodel = Traduce(user);
                    var access = new AccessUser().Verifica(usermodel);
                    if (access)
                    {
                        //preguntar su rol
                        string rol = new AccessUser().VerifyRol(usermodel);
                        var EmailClaim = new Claim(ClaimTypes.Email, user.Email);
                        var RolClaim = new Claim(ClaimTypes.Role, rol);
                        var identity = new ClaimsIdentity(new [] {EmailClaim, RolClaim}, "cookie");
                        var principal = new ClaimsPrincipal(identity);

                        await this.HttpContext.SignInAsync(principal);
                        //si es admin redireccion viewrol
                        if(rol == "admin")
                        {
                            //mandar a su pag    
                            return RedirectToAction("Waiting","Admin");    
                        }else{    
                            return RedirectToAction("Upload","Product");
                        }
                    
                    }else{
                        //escribio mal algo
                        //no existe
                        //recargar la misma pagina pero vacia
                        return RedirectToAction("Login","Inicio");
                    }
            }  
            
            
        }

    }

}