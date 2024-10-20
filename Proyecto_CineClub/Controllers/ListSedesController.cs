using Proyecto_CineClub.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_CineClub.Controllers
{
    public class ListSedesController : Controller
    {
        private readonly SedeDAO _sedeDAO;

        public ListSedesController()
        {
            _sedeDAO = new SedeDAO();
        }

        // GET: ListSedes
        public ActionResult Sedes()
        {
            // Obtener la lista de sedes
            var sedes = _sedeDAO.GetAll(); 
            return View(sedes); 
        }
    }
}
