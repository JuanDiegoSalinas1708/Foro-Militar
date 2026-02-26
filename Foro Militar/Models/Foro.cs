using System;
using System.Runtime.Serialization;

public class Foro
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Hechos { get; set; }

    public DateTime Fecha { get; set; }
    public string Imagen { get; set; }
    public string Pais { get; set; }
    public bool Activa { get; set; }
}