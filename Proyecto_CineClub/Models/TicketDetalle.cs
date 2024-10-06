using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_CineClub.Models
{
    public class TicketDetalle
    {
        public int IdTicketDetalle { get; set; }
        public int IdTicket { get; set; }
        public int IdAsiento { get; set; }
    }
}