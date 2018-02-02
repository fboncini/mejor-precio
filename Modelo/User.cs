using System;

namespace Modelo
{
    public class User
    {
        
        public string Name { get; set; }
        public string LastName { get; set; }
        public int DNI { get; set; }
        public string Email { get; set; }
        public Guid AccessCode { get; set; }
        public string Rol { get; set; }
        
    }
}
