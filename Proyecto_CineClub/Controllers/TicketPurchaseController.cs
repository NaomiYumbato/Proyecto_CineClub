using Proyecto_CineClub.Data;
using Proyecto_CineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Proyecto_CineClub.Controllers
{
    public class TicketPurchaseController : Controller
    {
        private readonly PeliculasDAO _peliculaDAO;

        public TicketPurchaseController()
        {
            _peliculaDAO = new PeliculasDAO();
        }

        //[HttpGet]
        //public ActionResult ComprarTicket(int id)
        //{
        //    Persona usuario = Session["usuario"] as Persona;
        //    if (usuario != null)
        //    {
        //        var pelicula = _peliculaDAO.GetAll().FirstOrDefault(x => x.IdPelicula == id);
        //        var asientosDisponibles = GetAvailableSeats(id); // Supongamos que tienes un método para obtener asientos disponibles

        //        var viewModel = new TicketPurchaseViewModel
        //        {
        //            Pelicula = pelicula,
        //            AsientosDisponibles = asientosDisponibles,
        //            Usuario = usuario
        //        };

        //        return View(viewModel);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "Auth");
        //    }
        //}

        //[HttpPost]
        //public ActionResult ComprarTicket(TicketPurchaseViewModel model)
        //{
        //    Persona usuario = Session["usuario"] as Persona;
        //    if (usuario != null)
        //    {
        //        // Lógica para manejar la compra de tickets usando el procedimiento almacenado
        //        string mensaje = ComprarTicketProcedimiento(model);
        //        ViewBag.mensaje = mensaje;
        //        return View(model);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "Auth");
        //    }
        //}

        //private List<Asiento> GetAvailableSeats(int idPelicula)
        //{
        //    // Implementar la lógica para obtener los asientos disponibles para la película
        //}

        //private string ComprarTicketProcedimiento(TicketPurchaseViewModel model)
        //{
        //    // Implementar la lógica para el procedimiento de compra de ticket
        //}

    }

}