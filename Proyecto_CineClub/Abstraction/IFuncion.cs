using Dominio.Repositorio;
using Proyecto_CineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_CineClub.Abstraction
{
    public interface IFuncion:IRepositorioGET<Funcion>
    {
        IEnumerable<Funcion> GetFuncionesDisponiblesPorPelicula(int id_pelicula);

    }
}
