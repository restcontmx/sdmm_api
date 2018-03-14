using Quartz;
using System;
using Models.Catalogs;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using Warrior.Handlers.Enums;

namespace SDMM_API.Modules
{
    public class InventarioDiarioJob : IJob
    {

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                IList<Producto> productos = getAllProductos();
               
                foreach (Producto p in productos)
                {
                    createIventarioDiario(p.id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        
        }

        //Hace el registro
        public TransactionResult createIventarioDiario(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createExistenciaInventario", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("producto_id", id));
                    command.ExecuteNonQuery();
                    connection.Close();
                    return TransactionResult.CREATED;
                }
                catch (SqlException ex)
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    if (ex.Number == 2627)
                    {
                        return TransactionResult.EXISTS;
                    }
                    return TransactionResult.NOT_PERMITTED;
                }
                catch (Exception ex)
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return TransactionResult.ERROR;
                }
            }
        }

        //Regresa todos los productos
        public IList<Producto> getAllProductos()
        {
            SqlConnection connection = null;
            IList<Producto> objects = new List<Producto>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllProducto", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Producto
                        {
                            id = int.Parse(row[0].ToString()),
                            codigo = row[1].ToString(),
                            nombre = row[2].ToString(),
                            costo = decimal.Parse(row[3].ToString()),
                            peso = decimal.Parse(row[4].ToString()),
                            revision = int.Parse(row[5].ToString()),
                            cantidad_caja_promedio = int.Parse(row[6].ToString()),
                            rango_caja_cierre = int.Parse(row[7].ToString()),
                            timestamp = Convert.ToDateTime(row[8].ToString()),
                            updated = Convert.ToDateTime(row[9].ToString()),
                            tipo_producto = new TipoProducto
                            {
                                id = int.Parse(row[10].ToString()),
                                name = row[11].ToString(),
                                description = row[12].ToString(),
                                value = int.Parse(row[13].ToString())
                            },
                            proveedor = new Proveedor
                            {
                                id = int.Parse(row[14].ToString()),
                                nombre_comercial = row[15].ToString()
                            },
                            segmento = new SegmentoProducto
                            {
                                id = int.Parse(row[16].ToString()),
                                name = row[17].ToString()
                            }
                        });
                    }
                    return objects;

                }
                catch (SqlException ex)
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return objects;
                }
            }
        }
    }
}
