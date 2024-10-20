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
        private readonly ComboDulceriaDAO _comboDAO;

        public TicketPurchaseController()
        {
            _peliculaDAO = new PeliculasDAO();
            _comboDAO = new ComboDulceriaDAO();
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

        // Acción para mostrar los combos de dulceria
        public ActionResult Combos_Dulceria()
        {
            var listado = _comboDAO.GetAll(); 

            List<SelectListItem> selectItems = new List<SelectListItem>();

            if (listado != null && listado.Any())
            {
                foreach (var combo in listado)
                {
                    string nombresProductos = "";
                    foreach (var producto in combo.Productos)
                    {
                        nombresProductos += producto.Nombre + ", ";
                    }

                    if (nombresProductos.EndsWith(", "))
                    {
                        nombresProductos = nombresProductos.Substring(0, nombresProductos.Length - 2);
                    }

                    selectItems.Add(new SelectListItem
                    {
                        Value = combo.IdComboDulceria.ToString(),
                        Text = nombresProductos
                    });
                }
            }

            ViewBag.selectCombos = new SelectList(selectItems, "Value", "Text");

            return View(listado); 
        }

    }

}