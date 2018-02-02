using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Persistencia
{
    public class HistoruRepo
    {
        string ConStr = System.Environment.GetEnvironmentVariable("ConnectionString");

        public void UpdateHistory(string BarCode, int UserId)
        {
            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT * FROM Usuario WHERE Usuario.UserId = @UserId",conn))
                {

                }


                
                using (SqlCommand cmd = new SqlCommand(@"INSERT INTO SearchHistory(Name,BarCode,Accountant,guianguid) VALUES(@name,@barcode,)",conn))
                {

                }
            }
        }
    }
}