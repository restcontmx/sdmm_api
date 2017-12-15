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
    public class AbastecimientoRepository : IAbastecimientoRepository
    {
        /// <summary>
        /// Create new object on the db
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public int create(AbastecimientoPipa abastecimiento)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createAbastecimiento", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("pipa_id", abastecimiento.pipa.id));
                    command.Parameters.Add(new SqlParameter("user_id", abastecimiento.despachador.id));

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


        public TransactionResult createDetalle(DetalleAbastecimientoPipa detalle)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createDetalleAbastecimientoPipa", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("tanque_id", detalle.tanque.id));
                    command.Parameters.Add(new SqlParameter("pipa_id", detalle.pipa.id));
                    command.Parameters.Add(new SqlParameter("abastecimiento_id", detalle.abastecimiento.id));
                    command.Parameters.Add(new SqlParameter("recibo", detalle.foto_recibo));
                    command.Parameters.Add(new SqlParameter("litros", detalle.litros));
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
                    SqlCommand command = new SqlCommand("sp_deleteAbastecimiento", connection);
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

        public TransactionResult deleteDetallesByIdAbastecimiento(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_eleteDetallesByIdAbastecimiento", connection);
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

        public AbastecimientoPipa detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_abastecimientoDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new AbastecimientoPipa
                    {
                        id = int.Parse(row[0].ToString()),
                        timestamp = Convert.ToDateTime(row[1].ToString()),
                        updated = Convert.ToDateTime(row[2].ToString()),
                        pipa = new Pipa
                        {
                            id = int.Parse(row[3].ToString()),
                            nombre = row[4].ToString(),
                            placas = row[5].ToString()
                        },
                        despachador = new User
                        {
                            id = int.Parse(row[6].ToString()),
                            first_name = row[7].ToString(),
                            second_name = row[8].ToString()
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

        public IList<AbastecimientoPipa> getAll()
        {
            SqlConnection connection = null;
            IList<AbastecimientoPipa> objects = new List<AbastecimientoPipa>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllAbastecimiento", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new AbastecimientoPipa
                        {
                            id = int.Parse(row[0].ToString()),
                            timestamp = Convert.ToDateTime(row[1].ToString()),
                            updated = Convert.ToDateTime(row[2].ToString()),
                            pipa = new Pipa
                            {
                                id = int.Parse(row[3].ToString()),
                                nombre = row[4].ToString(),
                                placas = row[5].ToString()
                            },
                            despachador = new User
                            {
                                id = int.Parse(row[6].ToString()),
                                first_name = row[7].ToString(),
                                second_name = row[8].ToString()
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


        public IList<DetalleAbastecimientoPipa> getAllDetallesByAbastecimientoId(int id)
        {
            SqlConnection connection = null;
            IList<DetalleAbastecimientoPipa> objects = new List<DetalleAbastecimientoPipa>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllDetallesByAbastecimientoId", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new DetalleAbastecimientoPipa
                        {
                            litros = int.Parse(row[0].ToString()),
                            foto_recibo = row[1].ToString(),
                            abastecimiento = new AbastecimientoPipa
                            {
                                id = int.Parse(row[2].ToString()),
                                timestamp = Convert.ToDateTime(row[3].ToString()),
                                updated = Convert.ToDateTime(row[4].ToString()),
                            },
                            pipa =  new Pipa
                            {
                                id = int.Parse(row[5].ToString()),
                                nombre = row[6].ToString(),
                                placas = row[7].ToString()
                            },
                            tanque = new Tanque
                            {
                                id = int.Parse(row[8].ToString()),
                                nombre = row[9].ToString(),
                                combustible = new Combustible
                                {
                                    id = int.Parse(row[10].ToString()),
                                    nombre = row[11].ToString()
                                }
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

        public TransactionResult update(AbastecimientoPipa abastecimiento)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateAbastecimiento", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("pipa_id", abastecimiento.pipa.id));
                    command.Parameters.Add(new SqlParameter("user_id", abastecimiento.despachador.id));
                    command.Parameters.Add(new SqlParameter("id", abastecimiento.id));
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