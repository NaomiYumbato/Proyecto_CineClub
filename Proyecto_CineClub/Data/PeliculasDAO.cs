using Proyecto_CineClub.Abstraction;
using Proyecto_CineClub.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_CineClub.Data
{
    public class PeliculasDAO : IPelicula
    {
        private readonly string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public IEnumerable<Pelicula> GetAll()
        {
            var listado = new List<Pelicula>();
            var sp = "usp_get_obtenerPeliculas";

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
                        Pelicula pelicula = new Pelicula()
                        {
                            IdPelicula = Convert.ToInt32(dr["id_pelicula"]),
                            IdSala = Convert.ToInt32(dr["id_sala"]),
                            Titulo = Convert.ToString(dr["titulo"]),
                            Descripcion = Convert.ToString(dr["descripcion"]),
                            Genero = Convert.ToInt32(dr["id_genero_pelicula"]),
                            Duracion = Convert.ToString(dr["duracion"]),
                            FechaInicioCartelera = Convert.ToDateTime(dr["fec_inicio_cartelera"]),
                            FechaFinCartelera = Convert.ToDateTime(dr["fec_fin_cartelera"]),
                            RutaImagen = Convert.ToString(dr["ruta_imagen"])
                        };
                        listado.Add(pelicula);
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

        public IEnumerable<Pelicula> BuscarPorTitulo(string titulo)
        {
            var listado = new List<Pelicula>();
            var sp = "usp_get_PeliculasPorTitulo";

            try
            {
                using (SqlConnection cone = new SqlConnection(cadena))
                {
                    cone.Open();

                    SqlCommand cmd = new SqlCommand(sp, cone);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Agrega el parámetro para el título
                    cmd.Parameters.AddWithValue("@titulo", titulo);

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Pelicula pelicula = new Pelicula()
                        {
                            IdPelicula = Convert.ToInt32(dr["id_pelicula"]),
                            Titulo = Convert.ToString(dr["titulo"]),
                            RutaImagen = Convert.ToString(dr["ruta_imagen"])
                        };
                        listado.Add(pelicula);
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