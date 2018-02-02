using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistencia;
using Modelo;
using Negocio;
using MejorPrecio4MVC.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace MejorPrecio4MVC.Controllers
{
    //[Route("api/[controller]")]
    [Route("[controller]")]
    public class RegisterController : Controller
    {
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


        // POST api/User/
        [HttpPost("SignUp")]
        public async Task<IActionResult> DateUser(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login","Inicio");
            }else{
                var responder = new AccessUser().VerificaReg(Traducir(user));
                if (responder)
                {
                    //devolver pagina de inicio sesion
                    return RedirectToAction("Login","Inicio");
                }else{
                    //mensaje
                    //mandar a la siguien pag
                    new AccessUser().Permitir(Traducir(user));
                    user.Rol = "pendiente";

                    var EmailClaim = new Claim(ClaimTypes.Email, user.Email);
                    var RolClaim = new Claim(ClaimTypes.Role, user.Rol);
                    var identity = new ClaimsIdentity(new [] {EmailClaim, RolClaim}, "cookie");
                    var principal = new ClaimsPrincipal(identity);

                    await this.HttpContext.SignInAsync(principal);
                    

                    return RedirectToAction("Upload","Product");
                }
            }
        }    
    }
}