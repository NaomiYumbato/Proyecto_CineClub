using Dominio.Repositorio;
using Proyecto_CineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_CineClub.Abstraction
{
    public interface IPelicula : IRepositorioGET<Pelicula>
    {
    }
}