using Proyecto_CineClub.Abstraction;
using Proyecto_CineClub.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_CineClub.Data
{
    public class FuncionDAO : IFuncion
    {
        private readonly string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public IEnumerable<Funcion> GetAll()
        {
            throw new NotImplementedException();
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
    }

}