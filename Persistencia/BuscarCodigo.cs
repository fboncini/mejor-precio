using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Modelo;

namespace Persistencia
{
    public class BuscarCodigo
    {
        public List<Product> BuscarPorCodigo(string CodigoBarras)
        {

            SqlDataReader rdr = null;
            List<Product> ProductosIguales = new List<Product>();
            string ConStr = Environment.GetEnvironmentVariable("ConnectionString");
            using (var con = new SqlConnection(ConStr)){
                con.Open();
                using(var Command = con.CreateCommand()){
                    Command.CommandText = "SELECT Producto.*, Precio.* from Producto INNER JOIN Precio ON Producto.BarCode = Precio.BarCode WHERE Producto.BarCode = @BarCode ORDER BY Precio.Cost";
                    Command.Parameters.AddWithValue("@BarCode" , CodigoBarras);
                    using(rdr = Command.ExecuteReader()){
                        while (rdr.Read()){
                            Product Product = new Product();
                            Product.Name = Convert.ToString(rdr[1]);
                            Product.BarCode = Convert.ToString(rdr[0]);
                            Product.Price = Convert.ToInt32(rdr[4]);
                            Product.Latitude = Convert.ToString(rdr[5]);
                            Product.Longitude = Convert.ToString(rdr[6]);
                            Product.Description = Convert.ToString(rdr[2]);
                            Product.Day = Convert.ToDateTime(rdr[8]).Date;
                            ProductosIguales.Add(Product);
                        }
                    }
                
                }
            }
            return ProductosIguales;
        }
        public Product SearchProduct(string CodigoBarras)
        {

            SqlDataReader rdr = null;
            Product product = new Product();
            string ConStr = Environment.GetEnvironmentVariable("ConnectionString");
            using (var con = new SqlConnection(ConStr)){
                con.Open();
                using(var Command = con.CreateCommand()){
                    Command.CommandText = "SELECT Producto.* from Producto WHERE Producto.BarCode = @BarCode";
                    Command.Parameters.AddWithValue("@BarCode" , CodigoBarras);
                    using(rdr = Command.ExecuteReader()){
                        while (rdr.Read()){
                            
                            product.Name = Convert.ToString(rdr[1]);
                            product.BarCode = Convert.ToString(rdr[0]);                        
                            product.Description = Convert.ToString(rdr[2]);
                        }
                    }
                
                }
            }
            return product;
        }
    }
}