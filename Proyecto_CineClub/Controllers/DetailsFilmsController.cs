using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_CineClub.Data;
using Proyecto_CineClub.Models;

namespace Proyecto_CineClub.Controllers
{
    public class DetailsFilmsController : Controller
    {
        private readonly PeliculasDAO _peliculaDAO;

        public DetailsFilmsController()
        {
            _peliculaDAO = new PeliculasDAO();
        }
        // GET: DetailsFilms

        private Pelicula BuscarPelicula(int id)
        {
            var listadoPeliculas = _peliculaDAO.GetAll() ?? new List<Pelicula>();
            return listadoPeliculas.FirstOrDefault(x => x.IdPelicula == id);
        }
        public ActionResult DetailsFilms(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            Pelicula pelicula = BuscarPelicula(id.Value);

            return View(pelicula);
        }
    }
}
