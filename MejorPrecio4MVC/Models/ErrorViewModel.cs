using System;

namespace MejorPrecio4MVC.Models
{
    public class ErrorViewModel
    {
        
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}