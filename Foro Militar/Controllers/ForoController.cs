using System;
using System.Collections.Generic;
using System.Web.Http;

[RoutePrefix("api/Foro")]
public class ForoController : ApiController
{
    [HttpPost]
    public IHttpActionResult Post([FromBody] Foro foro) 
    {
        if (foro == null)
            return BadRequest("Datos Invalidos");

        // ✅ Validación extra para evitar nulls
        if (string.IsNullOrEmpty(foro.Titulo) || string.IsNullOrEmpty(foro.Hechos))
            return BadRequest("Titulo y Hechos son obligatorios");

        ForoDal dal = new ForoDal();
        bool resultado = dal.Insertar(foro.Titulo, foro.Hechos, foro.Fecha, foro.Imagen, foro.Pais, foro.Activa);

        if (!resultado)
            return BadRequest("No se pudo agregar tu informacion");

        return Ok(resultado);
    }

    [HttpGet]
    public IHttpActionResult Get()
    {
        ForoDal dal = new ForoDal();
        var foro = dal.ObtenerTodos();
        return Ok(foro);
    }

    [HttpPut]
    public IHttpActionResult Put(int id, [FromBody] Foro foro)  
    {
        if (foro == null)
            return BadRequest("Datos invalidos");

        ForoDal dal = new ForoDal();
        bool actualizado = dal.Actualizar(id, foro.Titulo, foro.Hechos, foro.Fecha, foro.Imagen,foro.Pais,foro.Activa);

        if (!actualizado)
            return NotFound();

        return Ok("Registro Actualizado");
    }

    [HttpDelete]
    public IHttpActionResult Delete(int id)
    {
        ForoDal dal = new ForoDal();
        bool eliminado = dal.Eliminar(id);

        if (!eliminado)
            return NotFound();

        return Ok("Registro Eliminado");
    }
}