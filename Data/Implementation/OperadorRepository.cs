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
    /// <summary>
    /// Empleado repository implementation
    /// </summary>
    public class OperadorRepository : IOperadorRepository
    {
        /// <summary>
        /// Create new object on the db
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public TransactionResult create(Operador operador)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createOperador", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("nombre", Validations.defaultString(operador.nombre)));
                    command.Parameters.Add(new SqlParameter("ap_paterno", Validations.defaultString(operador.ap_paterno)));
                    command.Parameters.Add(new SqlParameter("ap_materno", Validations.defaultString(operador.ap_materno)));
                    command.Parameters.Add(new SqlParameter("compania_id", operador.compania.id));
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
                    SqlCommand command = new SqlCommand("sp_deleteOperador", connection);
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

        public Operador detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_operadorDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new Operador
                    {
                        id = int.Parse(row[0].ToString()),
                        nombre = row[1].ToString(),
                        ap_paterno = row[2].ToString(),
                        ap_materno = row[3].ToString(),
                        timestamp = Convert.ToDateTime(row[4].ToString()),
                        updated = Convert.ToDateTime(row[5].ToString()),
                        compania = new Compania
                        {
                            id = int.Parse(row[6].ToString()),
                            nombre_sistema = row[7].ToString()
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

        public IList<Operador> getAll()
        {
            SqlConnection connection = null;
            IList<Operador> objects = new List<Operador>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllOperadores", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Operador
                        {
                            id = int.Parse(row[0].ToString()),
                            nombre = row[1].ToString(),
                            ap_paterno = row[2].ToString(),
                            ap_materno = row[3].ToString(),
                            timestamp = Convert.ToDateTime(row[4].ToString()),
                            updated = Convert.ToDateTime(row[5].ToString()),
                            compania = new Compania
                            {
                                id = int.Parse(row[6].ToString()),
                                nombre_sistema = row[7].ToString()
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

        public TransactionResult update(Operador operador)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateOperador", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("nombre", Validations.defaultString(operador.nombre)));
                    command.Parameters.Add(new SqlParameter("ap_paterno", Validations.defaultString(operador.ap_paterno)));
                    command.Parameters.Add(new SqlParameter("ap_materno", Validations.defaultString(operador.ap_materno)));
                    command.Parameters.Add(new SqlParameter("compania_id", operador.compania.id));
                    command.Parameters.Add(new SqlParameter("id", operador.id));
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