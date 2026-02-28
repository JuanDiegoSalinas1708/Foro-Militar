using Foro.Entities.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

public class PostDal
{
    private string ConnectionString =
        ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    // =========================
    // INSERTAR
    // =========================
    public bool Insertar(string title, string content, string image, string country,
                         int userId, int communityId, int mainCategoryId)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();

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
        }
    }

    // =========================
    // OBTENER TODOS (solo activos)
    // =========================
    public List<Post> ObtenerTodos()
    {
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
                    Id = (int)reader["Id"],
                    Title = reader["Title"].ToString(),
                    Content = reader["Content"].ToString(),
                    Image = reader["Image"] == DBNull.Value ? null : reader["Image"].ToString(),
                    Country = reader["Country"] == DBNull.Value ? null : reader["Country"].ToString(),
                    CreatedAt = (DateTime)reader["CreatedAt"]
                };

                lista.Add(post);
            }
        }

        return lista;
    }

    // =========================
    // ACTUALIZAR
    // =========================
    public bool Actualizar(int id, string title, string content, string image,
                           string country, int mainCategoryId)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();

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

            string query = @"UPDATE Posts
                             SET IsDeleted = 1
                             WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}