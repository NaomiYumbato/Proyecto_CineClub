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
    public class ComboDulceriaDAO:IComboDulceria
    {
        private readonly string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public IEnumerable<ComboDulceria> GetAll()
        {
            var combosDict = new Dictionary<int, ComboDulceria>();
            var sp = "usp_ObtenerCombosConProductos";

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
                        int comboId = Convert.ToInt32(dr["id_combo_dulceria"]);

                        if (!combosDict.ContainsKey(comboId))
                        {
                            combosDict[comboId] = new ComboDulceria()
                            {
                                IdComboDulceria = comboId,
                                IdComboProducto = Convert.ToInt32(dr["id_combo_producto"]),
                                Productos = new List<ProductoDulceria>()
                            };
                        }

                        var producto = new ProductoDulceria()
                        {
                            Nombre = Convert.ToString(dr["NombreProducto"]),
                            Precio = Convert.ToDouble(dr["PrecioProducto"])
                        };

                        int cantidad = Convert.ToInt32(dr["cantidad"]);

                        for (int i = 0; i < cantidad; i++)
                        {
                            combosDict[comboId].Productos.Add(producto);
                        }
                    }

                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener combos de dulcería: " + ex.Message + " (posiblemente una columna faltante)", ex);
            }

            return combosDict.Values.ToList();
        }
    }
}