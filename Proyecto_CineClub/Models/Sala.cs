using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_CineClub.Models
{
    public class Sala
    {
        public int IdSala {get; set; }
        public int IdCine { get; set; }
        public int Enumeracion { get; set; }
        public int Capacidad { get; set; }
        public int Piso { get; set; }

        // Propiedad de navegación
        public virtual Cine Cine { get; set; }
    }
}