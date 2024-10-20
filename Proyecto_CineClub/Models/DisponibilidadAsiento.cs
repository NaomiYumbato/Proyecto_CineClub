using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_CineClub.Models
{
    public class DisponibilidadAsiento
    {
        public int IdAsiento { get; set; }
        public int IdFuncion { get; set; }
        public bool Disponibilidad { get; set; }
    }
}