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
    public class SalaDAO : ISala
    {
        private readonly string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public IEnumerable<Sala> GetAll()
        {
            var listado = new List<Sala>();
            var sp = " usp_get_Salas";

            try
            {
                using (SqlConnection cone = new SqlConnection(cadena))
                {
                    cone.Open();

                    SqlCommand cmd = new SqlCommand(sp, cone);

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Sala sala = new Sala()
                        {
                            IdSala = Convert.ToInt32(dr["id_sala"]),
                            IdCine = Convert.ToInt32(dr["id_cine"]),  
                            Enumeracion = Convert.ToInt32(dr["enumeracion"]), 
                            Capacidad = Convert.ToInt32(dr["capacidad_sala"]), 
                            Piso = Convert.ToInt32(dr["piso_sala"])  
                        };
                        listado.Add(sala);

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