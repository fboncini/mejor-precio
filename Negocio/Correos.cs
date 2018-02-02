using System;
using System.Collections;
using System.Net;
using System.Net.Mail;


namespace Negocio
{
    public class Correos
    {
        SmtpClient server = new SmtpClient()
        {
            Host= "smtp.gmail.com", Port= 587, UseDefaultCredentials = false,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Credentials = new NetworkCredential ("marquitos9555l@gmail.com", "$fideosRoqueros3741$"),
            EnableSsl = true

        };
        
        public void MandarCorreo(MailMessage mensaje)
        {
            server.Send(mensaje);
        }

    }
}
