using System;
using System.Collections.Generic;
using System.Net.Mail;
using Modelo;
using Persistencia;

namespace Negocio
{
    public class AccessUser
    {
       public bool Verifica(User user)
       {
           //le llegan un usuario del controller
           //lo manda al repo
            var exist = new UserRepo().ExistsUser(user);
           //recibe si el usuario existe o no
           if(exist)
           {
               //user existe
               return true;
           }
           else
           {
               //lo manda a permitir
               
               return false;   
            }   
       } 
       public bool VerificaReg(User user)
       {
           //le llegan un usuario del controller
           //lo manda al repo
            var exist = new UserRepo().ExistsUserReg(user);
           //recibe si el usuario existe o no
           if(exist)
           {
               //user existe
               return true;
           }
           else
           {
               //lo manda a permitir
               
               return false;   
            }   
       } 
       public string VerifyRol(User user)
       {
           var rol = new UserRepo().TypeUser(user);
           return rol;
       }
       public void Permitir(User user)
       {
           //le llega un un usuario no creado
           //llama al repo y lo manda aguardar
           new UserRepo().AgragarUser(user);
           //recibe un "usuario guardado"
           System.Console.WriteLine(user.Name + " " +" se ha creado");
           //llama a email
            sendEmail(user);
       }
        public void sendEmail(User user)
        {
            //recime el email
            //recibe  trsfcontraseña
            //llama a correos
            //le manda un email
            Correos Cr = new Correos();
            
            using( var mnsj = new MailMessage())
            {
                mnsj.To.Add(new MailAddress(user.Email));
                mnsj.Subject = "Hola Mundo";
                mnsj.From = new MailAddress("maquitos9555l@gmail.com", "f b");
                mnsj.Body = "  Mensaje de Prueba \n\n Enviado desde C#\n\n *VER EL ARCHIVO ADJUNTO*";

                // Enviar 
                Cr.MandarCorreo(mnsj);
            }
                
                
        }
        public void transformarContraseña(){
            //recibe la contraseña
            //la transforma en un codigo de acceso
            //duda en si se guarda¿preguntar?
        }

        public List<User> Pendientes ()
        {
            var lis = new UserRepo().UserPendiente();
            return lis;

        }

         public List<User> AllUser ()
        {
            var lis = new UserRepo().ListarUsuarios();
            return lis;

        }
        public void cam(string email)
        {
            new UserRepo().cambiado(email);
        }

         public void Denegado(string email)
        {
            new UserRepo().Borrar(email);
        }
    }
}