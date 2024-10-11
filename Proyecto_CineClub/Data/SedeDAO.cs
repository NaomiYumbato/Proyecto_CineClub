using Dominio.Repositorio;
using Proyecto_CineClub.Abstraction;
using Proyecto_CineClub.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_CineClub.Data
{
    public class SedeDAO : ISede
    {
        private readonly string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public IEnumerable<Cine> GetAll()
        {
            var listado = new List<Cine>();
            var sp = "usp_get_obtenerSedes";

            try
            {
                using (SqlConnection cone = new SqlConnection(cadena))
                {
                    cone.Open();

                    SqlCommand cmd = new SqlCommand(sp, cone);

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Cine sede = new Cine()
                        {
                            IdCine = Convert.ToInt32(dr["id_cine"]),
                            Sede = Convert.ToString(dr["sede"]),
                            Direccion = Convert.ToString(dr["direccion"])
                        };
                        listado.Add(sede);
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
    }
}