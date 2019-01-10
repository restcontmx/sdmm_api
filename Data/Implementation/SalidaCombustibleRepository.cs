using Data.Interface;
using System;
using System.Collections.Generic;
using Models.Catalogs;
using Warrior.Handlers.Enums;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Warrior.Data;
using Models.Auth;


namespace Data.Implementation
{
    public class SalidaCombustibleRepository : ISalidaCombustibleRepository
    {
        /// <summary>
        /// Create new object on the db
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public int create(SalidaCombustible salida)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createSalidaCombustible", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("odometro", salida.odometro));
                    command.Parameters.Add(new SqlParameter("foto", salida.foto));
                    command.Parameters.Add(new SqlParameter("turno", salida.turno));
                    command.Parameters.Add(new SqlParameter("maquinaria_id", salida.maquinaria.id));
                    command.Parameters.Add(new SqlParameter("compania_id", salida.compania.id));
                    command.Parameters.Add(new SqlParameter("operador_id", salida.operador.id));
                    command.Parameters.Add(new SqlParameter("subnivel_id", salida.subnivel.id));
                    command.Parameters.Add(new SqlParameter("usuario_id", salida.despachador.id));
                    command.Parameters.Add(new SqlParameter("fecha", salida.timestamp));

                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return int.Parse(row[0].ToString());
                }
                catch (SqlException ex)
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    if (ex.Number == 2627)
                    {
                        return 0;
                    }
                    return 0;
                }
                catch
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return 0;
                }
            }
        }


        public TransactionResult createDetalle(DetalleSalidaCombustible detalle)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createDetalleSalidaCombustible", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("litros_surtidos", detalle.litros_surtidos));
                    command.Parameters.Add(new SqlParameter("salida_combustible_id", detalle.salida_combustible.id));
                    command.Parameters.Add(new SqlParameter("tanque_id", detalle.tanque.id));
                    command.Parameters.Add(new SqlParameter("pipa_id", detalle.pipa.id));
                    command.Parameters.Add(new SqlParameter("combustible_id", detalle.combustible.id));
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
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_deleteSalidaCombustible", connection);
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

        public TransactionResult deleteDetallesByIdSalida(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_deleteDetallesByIdSalida", connection);
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

        public SalidaCombustible detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_salidaCombustibleDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new SalidaCombustible
                    {
                        id = int.Parse(row[0].ToString()),
                        odometro = int.Parse(row[1].ToString()),
                        foto = row[2].ToString(),
                        turno = int.Parse(row[3].ToString()),
                        timestamp = Convert.ToDateTime(row[4].ToString()),
                        updated = Convert.ToDateTime(row[5].ToString()),
                        maquinaria = new Maquinaria
                        {
                            id = int.Parse(row[6].ToString()),
                            nombre = row[7].ToString()
                        },
                        compania = new Compania
                        {
                            id = int.Parse(row[8].ToString()),
                            nombre_sistema = row[9].ToString()
                        },
                        operador = new Operador
                        {
                            id = int.Parse(row[10].ToString()),
                            nombre = row[11].ToString(),
                            ap_paterno = row[12].ToString(),
                            ap_materno = row[13].ToString()
                        },
                        subnivel = new SubNivel
                        {
                            id = int.Parse(row[14].ToString()),
                            nombre = row[15].ToString(),
                            nivel = new Nivel
                            {
                                codigo = row[16].ToString(),
                                nombre = row[17].ToString()
                            }
                        },
                        despachador = new User
                        {
                            id = int.Parse(row[18].ToString()),
                            first_name = row[19].ToString(),
                            second_name = row[20].ToString()
                        }
                    };
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

        public IList<SalidaCombustible> getAll()
{
            int id = 0;
            SqlConnection connection = null;
            IList<SalidaCombustible> objects = new List<SalidaCombustible>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllSalidasCombustible", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        id = int.Parse(row[0].ToString());
                        objects.Add(new SalidaCombustible
                        {
                            id = int.Parse(row[0].ToString()),
                            odometro = int.Parse(row[1].ToString()),
                            turno = int.Parse(row[3].ToString()),
                            timestamp = Convert.ToDateTime(row[4].ToString()),
                            updated = Convert.ToDateTime(row[5].ToString()),
                            maquinaria = new Maquinaria
                            {
                                id = int.Parse(row[6].ToString()),
                                nombre = row[7].ToString()
                            },
                            compania = new Compania
                            {
                                id = int.Parse(row[8].ToString()),
                                nombre_sistema = row[9].ToString()
                            },
                            operador = new Operador
                            {
                                id = int.Parse(row[10].ToString()),
                                nombre = row[11].ToString(),
                                ap_paterno = row[12].ToString(),
                                ap_materno = row[13].ToString()
                            },
                            subnivel = new SubNivel
                            {
                                id = int.Parse(row[14].ToString()),
                                nombre = row[15].ToString(),
                                nivel = new Nivel
                                {
                                    codigo = row[16].ToString(),
                                    nombre = row[17].ToString()
                                }
                            },
                            despachador = new User
                            {
                                id = int.Parse(row[18].ToString()),
                                first_name = row[19].ToString(),
                                second_name = row[20].ToString()
                            }
                        });
                    }
                    return objects;

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message + id.ToString());
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return objects;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + id.ToString());
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return objects;
                }
            }
        }


        public IList<DetalleSalidaCombustible> getAllDetallesBySalidaId(int id)
        {
            SqlConnection connection = null;
            IList<DetalleSalidaCombustible> objects = new List<DetalleSalidaCombustible>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllDetallesBySalidaId", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new DetalleSalidaCombustible
                        {
                            litros_surtidos = float.Parse(row[0].ToString()),
                            tanque = new Tanque
                            {
                                id = int.Parse(row[1].ToString()),
                                nombre = row[2].ToString()
                            },
                            pipa = new Pipa
                            {
                                id = int.Parse(row[3].ToString()),
                                nombre = row[4].ToString()
                            },
                            combustible = new Combustible
                            {
                                id = int.Parse(row[5].ToString()),
                                nombre = row[6].ToString()
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

        public TransactionResult update(SalidaCombustible salida)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateSalidaCombustible", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("odometro", salida.odometro));
                    command.Parameters.Add(new SqlParameter("foto", salida.foto));
                    command.Parameters.Add(new SqlParameter("turno", salida.turno));
                    command.Parameters.Add(new SqlParameter("maquinaria_id", salida.maquinaria.id));
                    command.Parameters.Add(new SqlParameter("compania_id", salida.compania.id));
                    command.Parameters.Add(new SqlParameter("operador_id", salida.operador.id));
                    command.Parameters.Add(new SqlParameter("subnivel_id", salida.subnivel.id));
                    command.Parameters.Add(new SqlParameter("usuario_id", salida.despachador.id));
                    command.Parameters.Add(new SqlParameter("id", salida.id));
                    command.ExecuteNonQuery();
                    return TransactionResult.OK;
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

        public bool checkExists(SalidaCombustible salida)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    int auxInsert = 0;
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_checkExistsSalida", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("odometro", salida.odometro));
                    command.Parameters.Add(new SqlParameter("turno", salida.turno));
                    command.Parameters.Add(new SqlParameter("maquinaria_id", salida.maquinaria.id));
                    command.Parameters.Add(new SqlParameter("compania_id", salida.compania.id));
                    command.Parameters.Add(new SqlParameter("operador_id", salida.operador.id));
                    command.Parameters.Add(new SqlParameter("subnivel_id", salida.subnivel.id));
                    command.Parameters.Add(new SqlParameter("fecha", salida.timestamp));

                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];

                    auxInsert = int.Parse(row[0].ToString());

                    if (auxInsert == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
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
                catch
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return false;
                }
            }
        }
    }
}