using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Configuration;

public class ForoDal
{
    private string ConnectionString =
        ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    public bool Insertar(string titulo, string hechos, DateTime fecha, string imagen,string pais,bool activa)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            string query = "INSERT INTO Historico (Titulo,Hechos,Fecha,Imagen,pais,activa) VALUES (@Titulo,@Hechos,@Fecha,@Imagen,@Pais,@Activa)";
            SqlCommand cmd = new SqlCommand(query, conn);

            
            cmd.Parameters.AddWithValue("@Titulo", (object)titulo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Hechos", (object)hechos ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Fecha", fecha);
            cmd.Parameters.AddWithValue("@Imagen", (object)imagen ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Pais", (object)pais ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Activa",activa);

            int filasAfectadas = cmd.ExecuteNonQuery();
            return filasAfectadas > 0;
        }
    }

    public List<Foro> ObtenerTodos()
    {
        List<Foro> lista = new List<Foro>();
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            string query = "SELECT Id,Titulo,Hechos,Fecha,Imagen,Pais,Activa FROM Historico";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Foro foro = new Foro
                {
                    Id = (int)reader["Id"],
                    Titulo = reader["Titulo"].ToString(),
                    Hechos = reader["Hechos"].ToString(),
                    Fecha = (DateTime)reader["Fecha"],
                    Imagen = reader["Imagen"].ToString(),
                    Pais = reader["Pais"].ToString(),
                    Activa = (bool)reader["Activa"]

                };
                lista.Add(foro);
            }
        }
        return lista;
    }

    public bool Actualizar(int id, string titulo, string hechos, DateTime fecha, string imagen,string pais,bool activa)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            string query = @"UPDATE Historico 
                             SET Titulo=@Titulo, Hechos=@Hechos, Fecha=@Fecha, Imagen=@Imagen, Pais=@Pais, Activa=@Activa 
                             WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Id", id);
            
            cmd.Parameters.AddWithValue("@Titulo", (object)titulo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Hechos", (object)hechos ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Fecha", fecha);
            cmd.Parameters.AddWithValue("@Imagen", (object)imagen ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Pais",(object)pais ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Activa", activa);

            int filas = cmd.ExecuteNonQuery();
            return filas > 0;
        }
    }

    public bool Eliminar(int id)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            string query = "DELETE FROM Historico WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(query, conn);

            // ✅ Fix: faltaba el "@" en el nombre del parámetro
            cmd.Parameters.AddWithValue("@Id", id);

            int filas = cmd.ExecuteNonQuery();
            return filas > 0;
        }
    }
}