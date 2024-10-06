using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_CineClub.Models
{
    public class Asiento
    {
        public int IdAsiento { get; set; }
        public int IdSala { get; set; }
        public bool Disponibilidad { get; set; }
    }
}