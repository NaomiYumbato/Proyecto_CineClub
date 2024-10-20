using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_CineClub.Models
{
    public class DisponibilidadAsiento
    {
        public int IdFuncion { get; set; }
        public int IdSala { get; set; }
        public int IdPelicula { get; set; }
        public DateTime InicioFuncion { get; set; }
        public DateTime FinFuncion { get; set; }
    }
}