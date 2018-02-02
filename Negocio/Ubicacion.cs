using System;
using System.Net;
using System.IO;
using Modelo;
using Newtonsoft.Json;

namespace Negocio
{
    public class Ubicacion
    {
        public string Distance (Product product){
            
            string latitud = product.Latitude+"";
            string longitud = product.Longitude +"";
            string lat = latitud.Replace(',','.')+",";
            string lon = longitud.Replace(',','.');
            string url = "https://maps.googleapis.com/maps/api/distancematrix/json?origins=-34.6183466,-58.4256697&destinations="+lat + lon + "&key=AIzaSyAUz6mUnhrqFpjsLzpMNzSi4NKMNLQ7LT8";
            
            WebRequest request = WebRequest.Create(url);

            WebResponse response = request.GetResponse();

            Stream data = response.GetResponseStream();

            StreamReader reader = new StreamReader(data);

            // json-formatted string from maps api
            string responseFromServer = reader.ReadToEnd();
            var e = responseFromServer.IndexOf("text");
            var c = responseFromServer.Substring(e + 9);
            var f = c.IndexOf('"');
            var distance = c.Substring(0,f);
            if (distance == "stination_addresses"){
                return "no se puede calcular la distancia";
            }
            
            return $"La distancia desde lagash es {distance}" ;
        
        }
        
    }
    
}




