using Dominio.Repositorio;
using Proyecto_CineClub.Models;
using Proyecto_CineClub.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_CineClub.Abstraction
{
    public interface IAsiento: IRepositorioGET<Asiento>
    {
        IEnumerable<AsientoPurchase> GetAsientosPorFuncion(int idFuncion);
    }
}
