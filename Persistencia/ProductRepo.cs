using System; 
using System.Data.SqlClient;
using System.Collections.Generic; 
using ZXing; 
using System.Drawing; 
using Modelo;


namespace Persistencia 
    {
    public class ProductRepo 
        {
            string ConStr = System.Environment.GetEnvironmentVariable("ConnectionString");
            public bool DoesTheProductExists(string Barcode) 
        {
            bool Exist = false;
            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT * FROM producto WHERE Producto.Barcode = @Barcode",conn)) 
                {
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@BarCode",Barcode);
                    Exist = cmd.ExecuteScalar() != null;

                    cmd.ExecuteNonQuery();
                    conn.Close(); 
                    
                    return Exist;
                }
            }
        }

        public void newProduct(Product prod) 
        {
            using (SqlConnection conn = new SqlConnection(ConStr)) 
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(@"INSERT into producto(BarCode, Nam, Feature) VALUES (@BarCode, @Name, @Description)",conn)) 
                {
                    cmd.Parameters.AddWithValue("@Barcode",prod.BarCode);
                    cmd.Parameters.AddWithValue("@Name",prod.Name);
                    cmd.Parameters.AddWithValue("@Description",prod.Description);
                    cmd.Connection = conn; 

                    cmd.ExecuteNonQuery();
                    conn.Close(); 
                }
            }
        }


        public List < Product > BuscarPorCodigo (long CodigoBarras) 
        {
            SqlConnection conn = new SqlConnection(ConStr); 

            SqlDataReader rdr = null; 
            List < Product > ProductosIguales = new List < Product > (); 
        
            
            try {
                // 2. Open the connection
                conn.Open(); 

                // 3. Pass the connection to a command object
                SqlCommand cmd = new SqlCommand("SELECT Producto.*, Precio.* from Producto INNER JOIN Precio ON Producto.BarCode = Precio.BarCode WHERE Producto.BarCode =" + CodigoBarras + "ORDER BY Precio.Cost", conn); 
                rdr = cmd.ExecuteReader(); 
               
                // print the CustomerID of each record

                
                
                
                while (rdr.Read()) {
                    
                    Product Product = new Product(); 
                    var nae = (Int32)rdr[1]; 
                    Product.BarCode = Convert.ToString(rdr[0]); 
                    Product.Price = Convert.ToInt32(rdr[4]); 
                    Product.Latitude = Convert.ToString(rdr[5]);
                    Product.Longitude = Convert.ToString(rdr[6]); 
                    Product.Description = Convert.ToString(rdr[2]); 
                    Product.Day = Convert.ToDateTime(rdr[8]).Date; 
                    ProductosIguales.Add(Product); 
                }
                
                }
                finally 
                {
                    // close the reader
                    if (rdr != null) {
                        rdr.Close(); 
                    }

                    // 5. Close the connection
                    if (conn != null) {
                        conn.Close(); 
                    }
                }
        /*      Historial history = new Historial();
                history.Agregar(CodigoBarras,user); */
                return ProductosIguales; 
        }


    }
}
