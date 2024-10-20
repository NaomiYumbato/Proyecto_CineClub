using Proyecto_CineClub.Models;
using Proyecto_CineClub.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_CineClub.Controllers
{
    public class AuthController : Controller
    {
        public static readonly string CADENA = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        private Persona GetPersona(string correo, string clave)
        {
            var procedure = "usp_login_auth";
            Persona personaAuth = null; // Declarar aquí para que sea accesible en el return

            using (SqlConnection con = new SqlConnection(CADENA))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(procedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@clave", clave);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    int IdPersona = dr.GetInt32(dr.GetOrdinal("id_persona"));
                    string PrimerNombre = dr.GetString(dr.GetOrdinal("primer_nombre"));
                    string SegundoNombre = dr.GetString(dr.GetOrdinal("segundo_nombre"));
                    string PrimerApellido = dr.GetString(dr.GetOrdinal("primer_apellido"));
                    string SegundoApellido = dr.GetString(dr.GetOrdinal("segundo_apellido"));
                    string Dni = dr.GetString(dr.GetOrdinal("dni"));
                    string Celular = dr.GetString(dr.GetOrdinal("celular"));
                    string Correo = dr.GetString(dr.GetOrdinal("correo"));
                    DateTime FechaNacimiento = dr.GetDateTime(dr.GetOrdinal("fec_nacimiento"));                
                    string Usuario = dr.GetString(dr.GetOrdinal("usuario"));
                    string Clave = dr.GetString(dr.GetOrdinal("clave"));

                    personaAuth = new Persona
                    {
                        IdPersona = IdPersona,
                        PrimerNombre = PrimerNombre,
                        SegundoNombre = SegundoNombre,
                        PrimerApellido = PrimerApellido,
                        SegundoApellido = SegundoApellido,
                        Dni = Dni,
                        Celular = Celular,
                        Correo = Correo,
                        FechaNacimiento = FechaNacimiento,                       
                        Usuario = Usuario,
                        Clave = Clave
                    };
                }
                dr.Close();
            }
            return personaAuth;
        }

        //Metodo para registrar una persona y un cliente
        public string RegisterUser(string primerNombre, string segundoNombre, string primerApellido, string segundoApellido, string dni, string celular, string correo, DateTime fechaNacimiento, string usuario, string clave)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CADENA))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_register_user", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@primer_nombre", primerNombre);
                        cmd.Parameters.AddWithValue("@segundo_nombre", (object)segundoNombre ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@primer_apellido", primerApellido);
                        cmd.Parameters.AddWithValue("@segundo_apellido", (object)segundoApellido ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DNI", dni);
                        cmd.Parameters.AddWithValue("@celular", celular);
                        cmd.Parameters.AddWithValue("@correo", correo);
                        cmd.Parameters.AddWithValue("@fec_nacimiento", fechaNacimiento);
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@clave", clave);

                        cmd.ExecuteNonQuery();
                    }
                }
                return "Registro exitoso";
            }
            catch (Exception ex)
            {
                return $"Error al registrar: {ex.Message}";
            }
        }


        // GET: Auth

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login, string returnUrl)
        {
            if (string.IsNullOrEmpty(login.correo))
            {
                ModelState.AddModelError("correo", "El correo es obligatorio.");
            }

            if (string.IsNullOrEmpty(login.clave))
            {
                ModelState.AddModelError("clave", "La contraseña es obligatoria.");
            }

            if (!ModelState.IsValid)
            {
                return View(login);
            }

            Persona auth = GetPersona(login.correo, login.clave);
            if (auth != null)
            {
                Session["usuario"] = auth;
                TempData["mensaje"] = "Registro correcto";

                // Si la URL de retorno no está vacía y es una URL local, redirige a ella.
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                // Si no hay URL de retorno, redirige a la página de inicio.
                return RedirectToAction("Index", "ListFilms");
            }

            ModelState.AddModelError("", "Correo o contraseña incorrectos.");
            return View(login);
        }



        [HttpGet]
        public ActionResult Register()
        {
            return View(new Persona());
        }

        [HttpPost]
        public ActionResult Register(Persona register)
        {
            // Validar los campos obligatorios
            if (string.IsNullOrEmpty(register.PrimerNombre))
            {
                ModelState.AddModelError("PrimerNombre", "El primer nombre es obligatorio.");
            }
            if (string.IsNullOrEmpty(register.PrimerApellido))
            {
                ModelState.AddModelError("PrimerApellido", "El primer apellido es obligatorio.");
            }
            if (string.IsNullOrEmpty(register.Dni))
            {
                ModelState.AddModelError("Dni", "El DNI es obligatorio.");
            }
            if (string.IsNullOrEmpty(register.Celular))
            {
                ModelState.AddModelError("Celular", "El celular es obligatorio.");
            }
            if (string.IsNullOrEmpty(register.Correo))
            {
                ModelState.AddModelError("Correo", "El correo es obligatorio.");
            }
            if (string.IsNullOrEmpty(register.Usuario))
            {
                ModelState.AddModelError("Usuario", "El usuario es obligatorio.");
            }
            if (string.IsNullOrEmpty(register.Clave))
            {
                ModelState.AddModelError("Clave", "La contraseña es obligatoria.");
            }

            if (!ModelState.IsValid)
            {
                return View(register);
            }

            string mensaje = RegisterUser(
                register.PrimerNombre,
                register.SegundoNombre,
                register.PrimerApellido,
                register.SegundoApellido,
                register.Dni,
                register.Celular,
                register.Correo,
                register.FechaNacimiento, 
                register.Usuario,
                register.Clave);

            ViewBag.mensaje = mensaje;
            return View(register);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session["usuario"] = null;
            return RedirectToAction("Index", "ListFilms");
        }
    }
}