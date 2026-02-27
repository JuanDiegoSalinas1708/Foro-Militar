using System;
using System.Web.Http;

[RoutePrefix("api/posts")]
public class PostController : ApiController
{
    // =========================
    // CREAR POST
    // =========================
    [HttpPost]
    public IHttpActionResult Post([FromBody] Post post)
    {
        if (post == null)
            return BadRequest("Datos inválidos");

        if (string.IsNullOrEmpty(post.Title) || string.IsNullOrEmpty(post.Content))
            return BadRequest("Title y Content son obligatorios");

        PostDal dal = new PostDal();

        bool resultado = dal.Insertar(
            post.Title,
            post.Content,
            post.Image,
            post.Country,
            post.UserId,
            post.CommunityId,
            post.MainCategoryId
        );

        if (!resultado)
            return BadRequest("No se pudo crear el post");

        return Ok("Post creado correctamente");
    }

    // =========================
    // OBTENER TODOS
    // =========================
    [HttpGet]
    public IHttpActionResult Get()
    {
        PostDal dal = new PostDal();
        var posts = dal.ObtenerTodos();

        return Ok(posts);
    }

    // =========================
    // ACTUALIZAR
    // =========================
    [HttpPut]
    public IHttpActionResult Put(int id, [FromBody] Post post)
    {
        if (post == null)
            return BadRequest("Datos inválidos");

        PostDal dal = new PostDal();

        bool actualizado = dal.Actualizar(
            id,
            post.Title,
            post.Content,
            post.Image,
            post.Country,
            post.MainCategoryId
        );

        if (!actualizado)
            return NotFound();

        return Ok("Post actualizado correctamente");
    }

    // =========================
    // ELIMINAR (Soft Delete)
    // =========================
    [HttpDelete]
    public IHttpActionResult Delete(int id)
    {
        PostDal dal = new PostDal();
        bool eliminado = dal.Eliminar(id);

        if (!eliminado)
            return NotFound();

        return Ok("Post eliminado correctamente");
    }
}