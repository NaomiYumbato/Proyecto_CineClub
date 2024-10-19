using Proyecto_CineClub.Data;
using Proyecto_CineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_CineClub.Controllers
{
    public class ListFilmsController : Controller
    {
        private readonly SedeDAO _sedeDAO;
        private readonly PeliculasDAO _peliculaDAO;
        private readonly SalaDAO _salaDAO;
        private readonly GeneroDAO _generoDAO;

        public ListFilmsController()
        {
            _sedeDAO = new SedeDAO();
            _peliculaDAO = new PeliculasDAO();
            _salaDAO = new SalaDAO();
            _generoDAO = new GeneroDAO();
        }

        // Acción para mostrar la lista de películas en la cartelera
        public ActionResult Index()
        {
            var listado = _sedeDAO.GetAll();
            var listadoPeliculas = _peliculaDAO.GetAll() ?? new List<Pelicula>();

            // Obtener solo los IDs de las películas
            var idsPeliculas = listadoPeliculas.Select(p => p.IdPelicula).ToList();
            ViewBag.idsPeliculas = idsPeliculas;

            // Validar listado de sedes
            ViewBag.selectSede = (listado != null && listado.Any())
                ? new SelectList(listado, "IdCine", "Sede")
                : new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");

            return View(listadoPeliculas);
        }

        // Acción para buscar películas por sede y fecha
        public ActionResult BuscarPorSedeYFecha(int? idSede, DateTime? fecha)
        {
            var listadoPeliculas = _peliculaDAO.GetAll() ?? new List<Pelicula>();
            var listadoSalas = _salaDAO.GetAll() ?? new List<Sala>();
            var listadoCines = _sedeDAO.GetAll() ?? new List<Cine>();

            // Filtrar películas por sede y fecha
            var peliculasFiltradas = (from pelicula in listadoPeliculas
                                      join sala in listadoSalas on pelicula.IdSala equals sala.IdSala
                                      where (idSede == null || sala.IdCine == idSede) &&
                                            (fecha == null || (pelicula.FechaInicioCartelera <= fecha && pelicula.FechaFinCartelera >= fecha))
                                      select pelicula).ToList();

            // Crear el SelectList para las sedes
            ViewBag.selectSede = new SelectList(listadoCines, "IdCine", "Sede");

            // Limpiar los campos para la búsqueda
            ViewBag.IdSede = null;
            ViewBag.Fecha = null;

            return View(peliculasFiltradas);
        }

        // Acción para buscar películas por título o genero
        public ActionResult Cartelera(string titulo, int? idGenero)
        {
            var listadoGeneros = _generoDAO.GetAll(); // Obtener los géneros desde la base de datos.

            // Pasar directamente el listado de géneros a la vista.
            ViewBag.ListGenero = listadoGeneros;

            var peliculas = _peliculaDAO.GetAll();

            // Filtrar por título si se proporciona.
            if (!string.IsNullOrWhiteSpace(titulo))
            {
                peliculas = peliculas.Where(p => p.Titulo.Contains(titulo)).ToList();
            }

            // Filtrar por género si se proporciona.
            if (idGenero.HasValue)
            {
                peliculas = peliculas.Where(p => p.Genero == idGenero.Value).ToList();
            }

            ViewBag.Peliculas = peliculas;
            ViewBag.titulo = string.Empty;
            ViewBag.idGenero = idGenero;

            return View("Cartelera");
        }
    }
}
