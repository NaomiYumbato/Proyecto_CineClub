using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_CineClub.Models.ViewModels
{
    public class ConfirmarSeleccionViewModel
    {
        public int IdCliente { get; set; }
        public int IdFuncion { get; set; }
        public int Piso { get; set; }
        public int IdCine { get; set; }
        public int? IdComboSeleccionado { get; set; }
        public IEnumerable<AsientoPurchase> AsientosSeleccionados { get; set; }        
    }



}