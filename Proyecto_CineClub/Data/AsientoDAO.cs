using Proyecto_CineClub.Abstraction;
using Proyecto_CineClub.Models;
using Proyecto_CineClub.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_CineClub.Data
{
    public class AsientoDAO : IAsiento
    {
        private readonly string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public IEnumerable<Asiento> GetAll()
        {
            throw new NotImplementedException();
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
    }
}