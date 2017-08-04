using Data.Interface;
using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Warrior.Data;
using Warrior.Handlers.Enums;
namespace Data.Implementation
{
    public class InventarioRepository : IInventarioRepository
    {
        public TransactionResult create(Inventario inventario)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createInvetario", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("producto_id", inventario.producto.id));
                    command.Parameters.Add(new SqlParameter("cantidad", inventario.cantidad));
                    command.Parameters.Add(new SqlParameter("turno", inventario.turno));
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

        public TransactionResult createIventarioDiario()
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createInvetario", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.Add(new SqlParameter("producto_id", inventario.producto.id));
                    
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

        public TransactionResult delete(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_deleteInventario", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    command.ExecuteNonQuery();
                    return TransactionResult.DELETED;
                }
                catch (SqlException ex)
                {
                    if (connection != null)
                    {
                        connection.Close();
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

        public Inventario detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    /*connection.Open();
                    SqlCommand command = new SqlCommand("sp_segmentoProductoDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new SegmentoProducto
                    {
                        id = int.Parse(row[0].ToString()),
                        name = row[1].ToString(),
                        description = row[2].ToString()
                    };*/
                    return new Inventario();

                }
                catch (Exception ex)
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return null;
                }
            }
        }

        public IList<InfoInventario> getAll(string DateCheck)
        {
            SqlConnection connection = null;
            IList<InfoInventario> objects = new List<InfoInventario>();

            IList<Producto> productos = getAllProductos();

            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    foreach (Producto p in productos)
                    {
                        InfoInventario auxInfoInventario = new InfoInventario();

                        connection.Open();


                        //Selecciona la existencia inicial del turno 1
                        SqlCommand command = new SqlCommand("sp_getExistenciaInicial", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("fecha", DateCheck));
                        command.Parameters.Add(new SqlParameter("producto_id", p.id));
                        SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                        DataSet data_set = new DataSet();
                        data_adapter.Fill(data_set);
                        foreach (DataRow row in data_set.Tables[0].Rows)
                        {
                            auxInfoInventario.existenciaInicialT1 = int.Parse(row[2].ToString());
                        }

                        //Selecciona los datos del Turno 1
                        SqlCommand command2 = new SqlCommand("sp_getInfoInventario", connection);
                        command2.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.Add(new SqlParameter("turno", 1));
                        command2.Parameters.Add(new SqlParameter("fecha", DateCheck));
                        command2.Parameters.Add(new SqlParameter("producto_id", p.id));
                        SqlDataAdapter data_adapter2 = new SqlDataAdapter(command2);
                        DataSet data_set2 = new DataSet();
                        data_adapter2.Fill(data_set2);
                        foreach (DataRow row in data_set2.Tables[0].Rows)
                        {
                            auxInfoInventario.producto = p;
                            auxInfoInventario.unidad = row[0].ToString();
                            auxInfoInventario.entradasT1 = int.Parse(row[1].ToString());
                            auxInfoInventario.salidasT1 = int.Parse(row[2].ToString());
                            auxInfoInventario.devolucionesT1 = int.Parse(row[3].ToString());
                        }


                        //Selecciona los datos del Turno 2
                        SqlCommand command3 = new SqlCommand("sp_getInfoInventario", connection);
                        command3.CommandType = CommandType.StoredProcedure;
                        command3.Parameters.Add(new SqlParameter("turno", 2));
                        command3.Parameters.Add(new SqlParameter("fecha", DateCheck));
                        command3.Parameters.Add(new SqlParameter("producto_id", p.id));
                        SqlDataAdapter data_adapter3 = new SqlDataAdapter(command3);
                        DataSet data_set3 = new DataSet();
                        data_adapter3.Fill(data_set3);
                        foreach (DataRow row in data_set3.Tables[0].Rows)
                        {
                            auxInfoInventario.existenciaInicialT2 = (auxInfoInventario.existenciaInicialT1 - (auxInfoInventario.salidasT1 + auxInfoInventario.devolucionesT1));
                            auxInfoInventario.salidasT2 = int.Parse(row[2].ToString());
                            auxInfoInventario.devolucionesT2 = int.Parse(row[3].ToString());
                            auxInfoInventario.reservado = int.Parse(row[4].ToString());
                        }

                        //Existencia final
                        auxInfoInventario.existenciaFinal = ((auxInfoInventario.existenciaInicialT1 +
                                                               auxInfoInventario.entradasT1 +
                                                               auxInfoInventario.devolucionesT1 +
                                                               auxInfoInventario.devolucionesT2) - (auxInfoInventario.salidasT1 +
                                                                                                     auxInfoInventario.salidasT2));

                        objects.Add(auxInfoInventario);

                        connection.Close();
                    }

                    return objects;

                }
                catch (SqlException ex)
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return null;
                }
            }
        }

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
                            proveedor = new Proveedor
                            {
                                id = int.Parse(row[6].ToString()),
                                nombre_comercial = row[15].ToString()
                            },
                            segmento = new SegmentoProducto
                            {
                                id = int.Parse(row[7].ToString()),
                                name = row[16].ToString()
                            },
                            user = new Models.Auth.User { id = int.Parse(row[8].ToString()) },
                            timestamp = Convert.ToDateTime(row[9].ToString()),
                            updated = Convert.ToDateTime(row[10].ToString()),
                            tipo_producto = new TipoProducto
                            {
                                id = int.Parse(row[11].ToString()),
                                name = row[12].ToString(),
                                description = row[13].ToString(),
                                value = int.Parse(row[14].ToString())
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