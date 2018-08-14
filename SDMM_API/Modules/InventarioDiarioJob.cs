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

                //Crea el objeto log para el inventario
                Log logObject = new Log();
                logObject.message = "Inserción iniciada en InventarioDiarioJob a las " + DateTime.Now.ToString();
                logObject.source = "JOB";
                createLog(logObject);

                foreach (Producto p in productos)
                {
                    if (checkCorteInventario(p.id))
                    {
                        createIventarioDiario(p.id);
                    }
                }

                closeCajasPasadas();
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

                    //Crea el Log del Error
                    Log logObject = new Log();
                    logObject.message = "Error en inserción de id " + id.ToString() + " con mensaje: " + ex.Message;
                    logObject.source = "JOB, excepción SQL";
                    createLog(logObject);

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
                    //Crea el Log del Error
                    Log logObject = new Log();
                    logObject.message = "Error en inserción de id " + id.ToString() + " con mensaje: " + ex.Message;
                    logObject.source = "JOB, excepción general";
                    createLog(logObject);

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

        //Hace el cierre de cajas antiguas
        public TransactionResult closeCajasPasadas()
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_closeCajasPasadas", connection);
                    command.CommandType = CommandType.StoredProcedure;
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

        //Revisa si ya existe un corte de inventario de un producto
        public bool checkCorteInventario(int producto_id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    int result = 0;
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_existeCorteInventario", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("producto_id", producto_id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];

                    result = int.Parse(row[0].ToString());

                    if (result == 1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (SqlException ex)
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    if (ex.Number == 2627)
                    {
                        return false;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return false;
                }
            }
        }

        //Create Log en la tabla
        public TransactionResult createLog(Log log)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createLogs", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("message", log.message));
                    command.Parameters.Add(new SqlParameter("source", log.source));
                    command.ExecuteNonQuery();
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
                catch
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return TransactionResult.ERROR;
                }
            }
        }
    }
}
