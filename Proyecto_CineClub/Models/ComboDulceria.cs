using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_CineClub.Models
{
    public class ComboDulceria
    {
        public int IdComboProducto { get; set; }
        public int IdComboDulceria { get; set; }
        public int IdProductoDulceria { get; set; }
        public int Cantidad { get; set; }
        public List<ProductoDulceria> Productos { get; set; }
    }
}