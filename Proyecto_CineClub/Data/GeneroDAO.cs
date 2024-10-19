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
    public class GeneroDAO:IGenero
    {
        private readonly string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public IEnumerable<GeneroPelicula> GetAll()
        {
            var listado = new List<GeneroPelicula>();
            var sp = "usp_get_GeneroPeliculas";

            try
            {
                using (SqlConnection cone = new SqlConnection(cadena))
                {
                    cone.Open();

                    SqlCommand cmd = new SqlCommand(sp, cone);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        GeneroPelicula genero = new GeneroPelicula()
                        {
                            IdGeneroPelicula = Convert.ToInt32(dr["id_genero_pelicula"]),
                            Nombre = Convert.ToString(dr["nombre"])
                        };
                        listado.Add(genero);
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