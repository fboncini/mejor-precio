using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Modelo;

namespace Persistencia
{
    public class UserRepo
    {
       
        public void AgragarUser (User user)
        {
            string ConStr = Environment.GetEnvironmentVariable("ConnectionString");
            using(var conn = new SqlConnection(ConStr))
            {
                conn.Open();
                using(var command = conn.CreateCommand())
                {
                    command.CommandText = @"INSERT Usuario (Nombre,Lastname, Dni,Email, Rol) VALUES(@Name,@LastName,@DNI,@Email, 'Pendiente')";
                    
                    command.Parameters.AddWithValue("@DNI", user.DNI);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.ExecuteNonQuery();    
                }
                conn.Close();
            }
        }
        public bool ExistsUser(User user)
        {
            string ConStr = Environment.GetEnvironmentVariable("ConnectionString");
            using(var conn = new SqlConnection(ConStr))
            {
                conn.Open();
                using(var comm = conn.CreateCommand())
                {
                    comm.CommandType = CommandType.Text;
                    comm.CommandText=@"(SELECT * FROM Usuario WHERE Dni = @DNI AND Email = @Email)";
                    comm.Parameters.AddWithValue("@DNI", user.DNI);
                    comm.Parameters.AddWithValue("@Email", user.Email);
                        
                    bool exist = comm.ExecuteScalar() != null;
                    conn.Close();
                    return exist;    
                }
            }
        }
        public bool ExistsUserReg(User user)
        {
            string ConStr = Environment.GetEnvironmentVariable("ConnectionString");
            using(var conn = new SqlConnection(ConStr))
            {
                conn.Open();
                using(var comm = conn.CreateCommand())
                {
                    comm.CommandType = CommandType.Text;
                    comm.CommandText=@"(SELECT * FROM Usuario WHERE Dni = @DNI OR Email = @Email)";
                    comm.Parameters.AddWithValue("@DNI", user.DNI);
                    comm.Parameters.AddWithValue("@Email", user.Email);
                        
                    bool exist = comm.ExecuteScalar() != null;
                    conn.Close();
                    return exist;    
                }
            }
        }



        public string TypeUser(User user){
            string ConStr = Environment.GetEnvironmentVariable("ConnectionString");
            string role = "";
              using(var conn = new SqlConnection(ConStr))
            {
                conn.Open();
                SqlDataReader rdr = null;
                using(var comm = conn.CreateCommand())
                {
                    comm.CommandType = CommandType.Text;
                    comm.CommandText=@"(SELECT * FROM Usuario WHERE Dni = @DNI)";
                    comm.Parameters.AddWithValue("@DNI", user.DNI);
                    
                    using (rdr = comm.ExecuteReader())
                    {
                        while(rdr.Read())
                        {
                            role = (string)rdr[6];
                        }
                    }    
                }
            }
            return role;
        }

        public List<User> UserPendiente()
        {
            var UserPendient = new List<User>();
            string ConStr = Environment.GetEnvironmentVariable("ConnectionString");
            using (var con = new SqlConnection(ConStr)){
                con.Open();
                SqlDataReader rdr = null;
                using(var Command = con.CreateCommand()){
                    Command.CommandText = "SELECT Usuario.* from Usuario  WHERE Usuario.Rol = 'Pendiente'";
                    using(rdr = Command.ExecuteReader()){
                        while (rdr.Read()){
                            User pend = new User();
                            pend.Name = Convert.ToString(rdr[1]);
                            pend.LastName = Convert.ToString(rdr[2]);
                            pend.DNI = Convert.ToInt32(rdr[3]);
                            pend.Email = Convert.ToString(rdr[4]);
                            UserPendient.Add(pend);   
                        }
                    }
                }
                return UserPendient;
            }
        }

        public void cambiado (string email)
        {
            string ConStr = Environment.GetEnvironmentVariable("ConnectionString");
            using (var con = new SqlConnection(ConStr)){
                con.Open();
                using(var Command = con.CreateCommand()){
                    Command.CommandText = "UPDATE Usuario SET Rol = 'user'  WHERE Usuario.Email = @mail";
                    Command.Parameters.AddWithValue("@mail" ,email);
                    Command.ExecuteNonQuery();    

                }
            }

        }

         public void Borrar (string email)
        {
            string ConStr = Environment.GetEnvironmentVariable("ConnectionString");
            using (var con = new SqlConnection(ConStr)){
                con.Open();
                using(var Command = con.CreateCommand()){
                    Command.CommandText = "DELETE FROM Usuario WHERE Usuario.Email = @mail";
                    Command.Parameters.AddWithValue("@mail" ,email);
                    Command.ExecuteNonQuery();    

                }
            }

        }

        public List<User> ListarUsuarios()
        {
            var Usuarios = new List<User>();
            string ConStr = Environment.GetEnvironmentVariable("ConnectionString");
            using (var con = new SqlConnection(ConStr)){
                con.Open();
                SqlDataReader rdr = null;
                using(var Command = con.CreateCommand()){
                    Command.CommandText = "SELECT Usuario.* from Usuario  WHERE Usuario.Rol = 'User'";
                    using(rdr = Command.ExecuteReader()){
                        while (rdr.Read()){
                            User pend = new User();
                            pend.Name = Convert.ToString(rdr[1]);
                            pend.LastName = Convert.ToString(rdr[2]);
                            pend.DNI = Convert.ToInt32(rdr[3]);
                            pend.Email = Convert.ToString(rdr[4]);
                            Usuarios.Add(pend);   
                        }
                    }
                }
                return Usuarios;
            }
        }

    }

}
