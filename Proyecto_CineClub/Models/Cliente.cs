using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_CineClub.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public int IdPersona { get; set; }
        public decimal Saldo { get; set; }
    }
}