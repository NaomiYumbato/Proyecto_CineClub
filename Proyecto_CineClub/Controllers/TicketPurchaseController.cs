using Proyecto_CineClub.Data;
using Proyecto_CineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;

using System.Web.Mvc;
using Proyecto_CineClub.Models.ViewModels;
using System.Data;
using Proyecto_CineClub.Abstraction;

namespace Proyecto_CineClub.Controllers
{
    public class TicketPurchaseController : Controller
    {
        private readonly string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        private readonly PeliculasDAO _peliculaDAO;
        private readonly ComboDulceriaDAO _comboDAO;

        public TicketPurchaseController()
        {
            _peliculaDAO = new PeliculasDAO();
            _comboDAO = new ComboDulceriaDAO();
        }
        public IEnumerable<Funcion> GetFuncionesDisponiblesPorPelicula(int id_pelicula)
        {
            var listado = new List<Funcion>();
            var sp = "usp_FuncionesPorPelicula";
            try
            {
                using (SqlConnection cone = new SqlConnection(cadena))
                {
                    cone.Open();
                    SqlCommand cmd = new SqlCommand(sp, cone);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pelicula", id_pelicula);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Funcion funcion = new Funcion()
                        {
                            IdFuncion = Convert.ToInt32(dr["id_funcion"]),
                            IdSala = Convert.ToInt32(dr["id_sala"]),
                            IdPelicula = Convert.ToInt32(dr["id_pelicula"]),
                            InicioFuncion = Convert.ToDateTime(dr["inicio_funcion"]),
                            FinFuncion = Convert.ToDateTime(dr["fin_funcion"])
                        };
                        listado.Add(funcion);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las funciones por película", ex);
            }
            return listado;
        }


        public IEnumerable<AsientoPurchase> GetAsientosPorFuncion(int idFuncion)
        {
            var listado = new List<AsientoPurchase>();
            var sp = "usp_ObtenerAsientosPorFuncion";
            try
            {
                using (SqlConnection cone = new SqlConnection(cadena))
                {
                    cone.Open();
                    SqlCommand cmd = new SqlCommand(sp, cone);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_funcion", idFuncion);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        AsientoPurchase asiento = new AsientoPurchase()
                        {
                            IdAsiento = Convert.ToInt32(dr["id_asiento"]),
                            IdSala = Convert.ToInt32(dr["id_sala"]),
                            Disponibilidad = Convert.ToBoolean(dr["disponibilidad"])
                        };
                        listado.Add(asiento);
                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return listado;
        }

        public Sala GetSalaPorId(int idSala)
        {
            Sala sala = null;
            var query = "usp_GetSalaById";
            using (var connection = new SqlConnection(cadena))
            {
                var cmd = new SqlCommand(query, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@id_sala", idSala);
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sala = new Sala
                        {
                            IdSala = Convert.ToInt32(reader["id_sala"]),
                            IdCine = Convert.ToInt32(reader["id_cine"]),
                            Piso = Convert.ToInt32(reader["piso_sala"]),
                            Enumeracion = reader["enumeracion"].ToString(),
                            Capacidad_Sala = reader["capacidad_sala"].ToString()
                        };
                    }
                }
            }
            return sala;
        }


        public Cliente GetClientePorIdPersona(int idPersona)
        {
            Cliente cliente = null;
            var query = "usp_GetClienteByIdPersona";

            using (var connection = new SqlConnection(cadena))
            {
                var cmd = new SqlCommand(query, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@id_persona", idPersona);

                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cliente = new Cliente
                        {
                            IdCliente = Convert.ToInt32(reader["id_cliente"]),
                            IdPersona = Convert.ToInt32(reader["id_persona"]),
                            Saldo = Convert.ToDecimal(reader["saldo"]),
                        };
                    }
                }
            }

            return cliente;
        }

        public string EjecutarRegistrarCompra(int idCliente, int idFuncion, int piso, int idCine, int? idComboDulceria, int[] asientosSeleccionados)
        {
            using (var connection = new SqlConnection(cadena))
            {
                var cmd = new SqlCommand("usp_RegistrarCompra", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                cmd.Parameters.AddWithValue("@id_funcion", idFuncion);
                cmd.Parameters.AddWithValue("@piso", piso);
                cmd.Parameters.AddWithValue("@id_cine", idCine);
                cmd.Parameters.AddWithValue("@id_combo_dulceria", idComboDulceria.HasValue ? idComboDulceria.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@asientosSeleccionados", string.Join(",", asientosSeleccionados));

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return "Compra realizada con éxito.";
                }
                catch (Exception ex)
                {
                    return $"Error al realizar la compra: {ex.Message}";
                }
            }
        }

        public Funcion GetFuncionPorId(int id_funcion)
        {
            Funcion funcion = null;
            var query = "usp_GetFuncionById"; // Procedimiento almacenado que creamos antes
            using (var connection = new SqlConnection(cadena))
            {
                var cmd = new SqlCommand(query, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@id_funcion", id_funcion);

                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        funcion = new Funcion
                        {
                            IdFuncion = Convert.ToInt32(reader["id_funcion"]),
                            IdSala = Convert.ToInt32(reader["id_sala"]),
                            IdPelicula = Convert.ToInt32(reader["id_pelicula"]),
                            InicioFuncion = Convert.ToDateTime(reader["inicio_funcion"]),
                            FinFuncion = Convert.ToDateTime(reader["fin_funcion"]),
                            Precio = Convert.ToDecimal(reader["precio"])
                        };
                    }
                }
            }

            return funcion;
        }


        [HttpGet]
        public ActionResult FuncionesDisponibles(int id_pelicula)
        {
            var funciones = GetFuncionesDisponiblesPorPelicula(id_pelicula);
            ViewBag.id_pelicula = id_pelicula;
            return View(funciones);
        }
        //Luego el usuario selecciona la funcion y carga una vista con los asientos disponibles y los asientos ocupados en rojo

        [HttpGet]
        public ActionResult SeleccionarAsientos(int id_funcion)
        {
            var asientos = GetAsientosPorFuncion(id_funcion);
            ViewBag.id_funcion = id_funcion;
            return View(asientos);
        }
        [HttpPost]
        public ActionResult SeleccionarAsientos(int id_funcion, int[] asientosSeleccionados)
        {
            if (asientosSeleccionados == null || asientosSeleccionados.Length == 0)
            {
                ViewBag.ErrorMessage = "Por favor, seleccione al menos un asiento.";
                return RedirectToAction("SeleccionarAsientos", new { id_funcion });
            }
            // Redirigir a ConfirmarSeleccion pasando los asientos seleccionados y el id_funcion
            return RedirectToAction("ConfirmarSeleccion", new { asientosSeleccionados = string.Join(",", asientosSeleccionados), id_funcion });
        }
        [HttpGet]
        public ActionResult ConfirmarSeleccion(string asientosSeleccionados, int id_funcion)
        {
            var asientos = asientosSeleccionados.Split(',').Select(int.Parse).ToArray();
            if (asientos.Length == 0)
            {
                ViewBag.ErrorMessage = "Por favor, seleccione al menos un asiento.";
                return RedirectToAction("SeleccionarAsientos", new { id_funcion });
            }
            Persona usuario = Session["usuario"] as Persona;
            if (usuario == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            var asientosDetalles = GetAsientosPorFuncion(id_funcion)
                                    .Where(a => asientos.Contains(a.IdAsiento))
                                    .Select(a => new AsientoPurchase
                                    {
                                        IdAsiento = a.IdAsiento,
                                        IdSala = a.IdSala,
                                        Disponibilidad = a.Disponibilidad
                                    }).ToList();
            var funcion = GetFuncionPorId(id_funcion);
            var viewModel = new ConfirmarSeleccionViewModel
            {
                AsientosSeleccionados = asientosDetalles,
                IdFuncion = id_funcion,
                IdCliente = (Session["usuario"] as Persona).IdPersona,
                IdCine = GetSalaPorId(funcion.IdSala).IdCine,
                Piso = GetSalaPorId(funcion.IdSala).Piso,
                IdComboSeleccionado = null
            };

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult ConfirmarSeleccion(int IdCliente, int IdFuncion, int Piso, int IdCine, int? IdComboSeleccionado, int[] asientosSeleccionados)
        {
            Persona usuario = Session["usuario"] as Persona;
            if (usuario == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            string resultado = EjecutarRegistrarCompra(IdCliente, IdFuncion, Piso, IdCine, IdComboSeleccionado, asientosSeleccionados);
            ViewBag.Mensaje = resultado;
            var viewModel = new ConfirmarSeleccionViewModel
            {
                IdCliente = IdCliente,
                IdFuncion = IdFuncion,
                Piso = Piso,
                IdCine = IdCine,
                IdComboSeleccionado = IdComboSeleccionado,
                AsientosSeleccionados = GetAsientosPorFuncion(IdFuncion).Where(a => asientosSeleccionados.Contains(a.IdAsiento)).ToList(),

            };
            return View(viewModel);
        }

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
