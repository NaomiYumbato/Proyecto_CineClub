using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_CineClub.Models
{
    public class Funcion
    {
        public int IdFuncion { get; set; }
        public int IdSala { get; set; }
        public int IdPelicula { get; set; }

        public decimal Precio { get; set; }
        public DateTime InicioFuncion { get; set; }
        public DateTime FinFuncion { get; set; }
    }
}