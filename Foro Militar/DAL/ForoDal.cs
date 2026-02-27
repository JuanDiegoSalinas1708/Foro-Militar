using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ForoDal
{
    private string GetConnectionString()
    {
        var cs = ConfigurationManager.ConnectionStrings["DefaultConnection"];
        if (cs == null || string.IsNullOrWhiteSpace(cs.ConnectionString))
        {
            throw new InvalidOperationException(
                "No se encontró la cadena de conexión 'DefaultConnection' en el fichero de configuración. " +
                "Comprueba web.config / app.config y que exista <connectionStrings> con la entrada 'DefaultConnection'.");
        }
        return cs.ConnectionString;
    }

    public bool Insertar(string titulo, string hechos, DateTime fecha, string imagen, string pais, bool activa)
    {
        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            conn.Open();
            const string query = "INSERT INTO Historico (Titulo,Hechos,Fecha,Imagen,Pais,Activa) VALUES (@Titulo,@Hechos,@Fecha,@Imagen,@Pais,@Activa)";
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Titulo", (object)titulo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Hechos", (object)hechos ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Fecha", fecha);
                cmd.Parameters.AddWithValue("@Imagen", (object)imagen ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Pais", (object)pais ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Activa", activa);

                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }
    }

    public List<Foro> ObtenerTodos()
    {
        var lista = new List<Foro>();
        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            conn.Open();
            const string query = "SELECT Id,Titulo,Hechos,Fecha,Imagen,Pais,Activa FROM Historico";
            using (var cmd = new SqlCommand(query, conn))
            using (var reader = cmd.ExecuteReader())
            {
                int ordId = reader.GetOrdinal("Id");
                int ordTitulo = reader.GetOrdinal("Titulo");
                int ordHechos = reader.GetOrdinal("Hechos");
                int ordFecha = reader.GetOrdinal("Fecha");
                int ordImagen = reader.GetOrdinal("Imagen");
                int ordPais = reader.GetOrdinal("Pais");
                int ordActiva = reader.GetOrdinal("Activa");

                while (reader.Read())
                {
                    var foro = new Foro
                    {
                        Id = reader.GetInt32(ordId),
                        Titulo = reader.IsDBNull(ordTitulo) ? null : reader.GetString(ordTitulo),
                        Hechos = reader.IsDBNull(ordHechos) ? null : reader.GetString(ordHechos),
                        Fecha = reader.GetDateTime(ordFecha),
                        Imagen = reader.IsDBNull(ordImagen) ? null : reader.GetString(ordImagen),
                        Pais = reader.IsDBNull(ordPais) ? null : reader.GetString(ordPais),
                        Activa = reader.GetBoolean(ordActiva)
                    };
                    lista.Add(foro);
                }
            }
        }
        return lista;
    }

    public bool Actualizar(int id, string titulo, string hechos, DateTime fecha, string imagen, string pais, bool activa)
    {
        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            conn.Open();
            const string query = @"UPDATE Historico 
                             SET Titulo=@Titulo, Hechos=@Hechos, Fecha=@Fecha, Imagen=@Imagen, Pais=@Pais, Activa=@Activa 
                             WHERE Id=@Id";
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Titulo", (object)titulo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Hechos", (object)hechos ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Fecha", fecha);
                cmd.Parameters.AddWithValue("@Imagen", (object)imagen ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Pais", (object)pais ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Activa", activa);

                int filas = cmd.ExecuteNonQuery();
                return filas > 0;
            }
        }
    }

    public bool Eliminar(int id)
    {
        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            conn.Open();
            const string query = "DELETE FROM Historico WHERE Id=@Id";
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                int filas = cmd.ExecuteNonQuery();
                return filas > 0;
            }
        }
    }
}