using System;
using Modelo;
using System.Data.SqlClient;


namespace Persistencia
{
    public class AddPrice
    {
         public void newPrice (Product price)
        {   
            string ConStr = Environment.GetEnvironmentVariable("ConnectionString");
            using (var con = new SqlConnection(ConStr)){
                
                con.Open();
                using(var Command = con.CreateCommand()){
                    Command.CommandText =  @"
                        INSERT into 
                            precio (Cost, Latitud, Longitud, Barcode, Day)
                        VALUES
                            (@Cost,@Latitud, @Longitud, @BarCode,@Day)";
                    Command.Parameters.AddWithValue("@Cost" , price.Price);
                    Command.Parameters.AddWithValue("@Latitud" , price.Latitude);
                    Command.Parameters.AddWithValue("@Longitud" , price.Longitude);
                    Command.Parameters.AddWithValue("@BarCode" , price.BarCode);
                    Command.Parameters.AddWithValue("@Day", DateTime.Today);
                    Command.ExecuteNonQuery();
                }            

            }
            
        }
    }
}
