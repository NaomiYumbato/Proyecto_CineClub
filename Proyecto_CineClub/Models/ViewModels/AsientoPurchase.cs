using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_CineClub.Models.ViewModels
{
    public class AsientoPurchase
    {
        public int IdAsiento { get; set; }
        public int IdSala { get; set; }
        public bool Disponibilidad { get; set; }
    }
}