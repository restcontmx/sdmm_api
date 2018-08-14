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
    public class FichaEntregaRepository : IFichaEntregaRepository
    {
        /// <summary>
        /// Create new object on the db
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public int create(FichaEntregaRecepcion ficha)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createFichaEntrega", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("usuario_entrega_id", ficha.despachador_entrega.id));
                    command.Parameters.Add(new SqlParameter("usuario_recibe_id", ficha.despachador_recibe.id));

                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return int.Parse(row[0].ToString());

                    //command.ExecuteNonQuery();
                    //return TransactionResult.CREATED;
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


        public TransactionResult createDetalle(DetalleFichaEntregaRecepcion detalle)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createDetalleFichaEntrega", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("litros", detalle.litros));
                    command.Parameters.Add(new SqlParameter("tanque_id", detalle.tanque.id));
                    command.Parameters.Add(new SqlParameter("pipa_id", detalle.pipa.id));
                    command.Parameters.Add(new SqlParameter("ficha_entrega_id", detalle.ficha.id));
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
                    SqlCommand command = new SqlCommand("sp_deleteFichaEntrega", connection);
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

        public TransactionResult deleteDetallesByIdFicha(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_deleteDetallesByIdFicha", connection);
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

        public FichaEntregaRecepcion detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_fichaEntregaRecepcionDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new FichaEntregaRecepcion
                    {
                        id = int.Parse(row[0].ToString()),
                        timestamp = Convert.ToDateTime(row[1].ToString()),
                        updated = Convert.ToDateTime(row[2].ToString()),
                        despachador_entrega = new User
                        {
                            id = int.Parse(row[3].ToString()),
                            first_name = row[4].ToString(),
                            second_name = row[5].ToString()
                        },
                        despachador_recibe = new User
                        {
                            id = int.Parse(row[7].ToString()),
                            first_name = row[8].ToString(),
                            second_name = row[9].ToString()
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

        public IList<FichaEntregaRecepcion> getAll()
        {
            SqlConnection connection = null;
            IList<FichaEntregaRecepcion> objects = new List<FichaEntregaRecepcion>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllFichaEntregaRecepcion", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new FichaEntregaRecepcion
                        {
                            id = int.Parse(row[0].ToString()),
                            timestamp = Convert.ToDateTime(row[1].ToString()),
                            updated = Convert.ToDateTime(row[2].ToString()),
                            despachador_entrega = new User
                            {
                                id = int.Parse(row[3].ToString()),
                                first_name = row[4].ToString(),
                                second_name = row[5].ToString()
                            },
                            despachador_recibe = new User
                            {
                                id = int.Parse(row[7].ToString()),
                                first_name = row[8].ToString(),
                                second_name = row[9].ToString()
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


        public IList<DetalleFichaEntregaRecepcion> getAllDetallesByFichaId(int id)
        {
            SqlConnection connection = null;
            IList<DetalleFichaEntregaRecepcion> objects = new List<DetalleFichaEntregaRecepcion>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllDetallesByFichaId", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new DetalleFichaEntregaRecepcion
                        {
                            litros = float.Parse(row[0].ToString()),
                            tanque = new Tanque
                            {
                                id = int.Parse(row[1].ToString()),
                                nombre = row[2].ToString(),
                                combustible = new Combustible
                                {
                                    id = int.Parse(row[3].ToString()),
                                    nombre = row[4].ToString()
                                }
                            },
                            pipa = new Pipa
                            {
                                id = int.Parse(row[5].ToString()),
                                nombre = row[6].ToString(),
                                placas = row[7].ToString()
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

        public TransactionResult update(FichaEntregaRecepcion ficha)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateFichaEntrega", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("usuario_entrega_id", ficha.despachador_entrega.id));
                    command.Parameters.Add(new SqlParameter("usuario_recibe_id", ficha.despachador_recibe.id));
                    command.Parameters.Add(new SqlParameter("id", ficha.id));
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
    }
}