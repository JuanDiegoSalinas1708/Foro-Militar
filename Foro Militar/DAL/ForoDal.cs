
using Foro.Entities.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

public class PostDal
{
<<<<<<< HEAD
    private string ConnectionString =
        ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
=======
<<<<<<< HEAD
    private string GetConnectionString()
=======
    private string ConnectionString =
        ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    // =========================
    // INSERTAR
    // =========================
    public bool Insertar(string title, string content, string image, string country,
                         int userId, int communityId, int mainCategoryId)
>>>>>>> b8afad0 (alineacion de cositas de Front con la nueva BD)
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
>>>>>>> cf165f8 (Alineacion de algunos estilos con la nueva BD)

    // =========================
    // INSERTAR
    // =========================
    public bool Insertar(string title, string content, string image, string country,
                         int userId, int communityId, int mainCategoryId)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
<<<<<<< HEAD

=======
<<<<<<< HEAD
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
=======

>>>>>>> cf165f8 (Alineacion de algunos estilos con la nueva BD)
            string query = @"INSERT INTO Posts
                            (Title, Content, Image, Country, UserId, CommunityId, MainCategoryId)
                             VALUES
                            (@Title, @Content, @Image, @Country, @UserId, @CommunityId, @MainCategoryId)";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Title", title);
            cmd.Parameters.AddWithValue("@Content", content);
            cmd.Parameters.AddWithValue("@Image", (object)image ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Country", (object)country ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@CommunityId", communityId);
            cmd.Parameters.AddWithValue("@MainCategoryId", mainCategoryId);

            return cmd.ExecuteNonQuery() > 0;
<<<<<<< HEAD
=======
>>>>>>> b8afad0 (alineacion de cositas de Front con la nueva BD)
>>>>>>> cf165f8 (Alineacion de algunos estilos con la nueva BD)
        }
    }

    // =========================
    // OBTENER TODOS (solo activos)
    // =========================
    public List<Post> ObtenerTodos()
    {
<<<<<<< HEAD
        List<Post> lista = new List<Post>();

        using (SqlConnection conn = new SqlConnection(ConnectionString))
=======
<<<<<<< HEAD
        var lista = new List<Foro>();
        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
>>>>>>> cf165f8 (Alineacion de algunos estilos con la nueva BD)
        {
            conn.Open();

            string query = @"SELECT 
                            p.Id,
                            p.Title,
                            p.Content,
                            p.Image,
                            p.Country,
                            p.CreatedAt,
                            u.Username,
                            c.Name AS CommunityName,
                            cat.Name AS CategoryName,
                            cat.ColorHex
                        FROM Posts p
                        INNER JOIN Users u ON p.UserId = u.Id
                        INNER JOIN Communities c ON p.CommunityId = c.Id
                        INNER JOIN Categories cat ON p.MainCategoryId = cat.Id
                        WHERE p.IsDeleted = 0
                        ORDER BY p.CreatedAt DESC";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Post post = new Post
                {
<<<<<<< HEAD
=======
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
=======
        List<Post> lista = new List<Post>();

        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();

            string query = @"SELECT 
                            p.Id,
                            p.Title,
                            p.Content,
                            p.Image,
                            p.Country,
                            p.CreatedAt,
                            u.Username,
                            c.Name AS CommunityName,
                            cat.Name AS CategoryName,
                            cat.ColorHex
                        FROM Posts p
                        INNER JOIN Users u ON p.UserId = u.Id
                        INNER JOIN Communities c ON p.CommunityId = c.Id
                        INNER JOIN Categories cat ON p.MainCategoryId = cat.Id
                        WHERE p.IsDeleted = 0
                        ORDER BY p.CreatedAt DESC";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Post post = new Post
                {
>>>>>>> cf165f8 (Alineacion de algunos estilos con la nueva BD)
                    Id = (int)reader["Id"],
                    Title = reader["Title"].ToString(),
                    Content = reader["Content"].ToString(),
                    Image = reader["Image"] == DBNull.Value ? null : reader["Image"].ToString(),
                    Country = reader["Country"] == DBNull.Value ? null : reader["Country"].ToString(),
                    CreatedAt = (DateTime)reader["CreatedAt"]
                };

                lista.Add(post);
<<<<<<< HEAD
=======
>>>>>>> b8afad0 (alineacion de cositas de Front con la nueva BD)
>>>>>>> cf165f8 (Alineacion de algunos estilos con la nueva BD)
            }
        }

        return lista;
    }

<<<<<<< HEAD
=======
<<<<<<< HEAD
    public bool Actualizar(int id, string titulo, string hechos, DateTime fecha, string imagen, string pais, bool activa)
=======
>>>>>>> cf165f8 (Alineacion de algunos estilos con la nueva BD)
    // =========================
    // ACTUALIZAR
    // =========================
    public bool Actualizar(int id, string title, string content, string image,
                           string country, int mainCategoryId)
<<<<<<< HEAD
=======
>>>>>>> b8afad0 (alineacion de cositas de Front con la nueva BD)
>>>>>>> cf165f8 (Alineacion de algunos estilos con la nueva BD)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
<<<<<<< HEAD

=======
<<<<<<< HEAD
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
=======

>>>>>>> cf165f8 (Alineacion de algunos estilos con la nueva BD)
            string query = @"UPDATE Posts
                             SET Title = @Title,
                                 Content = @Content,
                                 Image = @Image,
                                 Country = @Country,
                                 MainCategoryId = @MainCategoryId,
                                 UpdatedAt = GETDATE()
                             WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Title", title);
            cmd.Parameters.AddWithValue("@Content", content);
            cmd.Parameters.AddWithValue("@Image", (object)image ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Country", (object)country ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MainCategoryId", mainCategoryId);

            return cmd.ExecuteNonQuery() > 0;
<<<<<<< HEAD
=======
>>>>>>> b8afad0 (alineacion de cositas de Front con la nueva BD)
>>>>>>> cf165f8 (Alineacion de algunos estilos con la nueva BD)
        }
    }

    // =========================
    // ELIMINAR (Soft Delete)
    // =========================
    public bool Eliminar(int id)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
<<<<<<< HEAD
=======
<<<<<<< HEAD
            const string query = "DELETE FROM Historico WHERE Id=@Id";
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                int filas = cmd.ExecuteNonQuery();
                return filas > 0;
            }
=======
>>>>>>> cf165f8 (Alineacion de algunos estilos con la nueva BD)

            string query = @"UPDATE Posts
                             SET IsDeleted = 1
                             WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() > 0;
<<<<<<< HEAD
=======
>>>>>>> b8afad0 (alineacion de cositas de Front con la nueva BD)
>>>>>>> cf165f8 (Alineacion de algunos estilos con la nueva BD)
        }
    }
}
