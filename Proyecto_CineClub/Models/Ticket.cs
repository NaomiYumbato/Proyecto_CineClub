using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_CineClub.Models
{
    public class Ticket
    {
        public int IdTicket { get; set; }        
        public int IdCliente { get; set; }
        public int IdSala { get; set; }        
        public DateTime FechaCompra { get; set; }
        public int Piso { get; set; }
        public double Monto { get; set; }
    }
}