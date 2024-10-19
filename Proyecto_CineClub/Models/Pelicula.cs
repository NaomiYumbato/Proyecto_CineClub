using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_CineClub.Models
{
    public class Pelicula
    {
        public int IdPelicula { get; set; }
        public int IdSala { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Genero { get; set; }
        public string Duracion { get; set; }    
        public DateTime FechaInicioCartelera { get; set; }
        public DateTime FechaFinCartelera { get; set; }
        public string RutaImagen { get; set; }
    }
}