using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Modelo;

namespace Persistencia
{
    public class Count{
        public List<(String,int)> CountPrices()
        {
            var lista = new List<(String,int)>();  
            SqlDataReader rdr = null;
            string ConStr = Environment.GetEnvironmentVariable("ConnectionString");
            using (var con = new SqlConnection(ConStr)){
                con.Open();
                using(var Command = con.CreateCommand()){
                    Command.CommandText =   @"SELECT 
                                                Producto.Nam, 
                                                COUNT(Precio.*) as NUM 
                                            FROM 
                                                Precio 
                                            INNER JOIN 
                                                Producto 
                                            ON 
                                                Producto.BarCode = Precio.BarCode 
                                            WHERE Precio.day BETWEEN DATEADD(year,-1,GETDATE()) and GETDATE() 
                                            GROUP BY 
                                                Nam
                                            ORDER BY
                                                NUM DESC";
                    using(rdr = Command.ExecuteReader()){
                        while (rdr.Read()){
                        string Nombre = (string)rdr[0];
                        int Cantidad = (int)rdr[1];
                        lista.Add((Nombre,Cantidad));
                        }
                    }
                }
            }
            return lista;
        }
    }
}