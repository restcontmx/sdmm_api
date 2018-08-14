using Data.Interface;
using System;
using System.Collections.Generic;
using Models.Catalogs;
using Warrior.Handlers.Enums;
using Warrior.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Models.Auth;

namespace Data.Implementation
{
    public class CuentaRepository : ICuentaRepository
    {
        public TransactionResult create(Cuenta cuenta, int sistema)
        {
            SqlConnection connection = null;

            if (sistema == 1)
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString);
            }
            else if (sistema == 2)
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString);
            }

            using (connection)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createCuenta", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("nombre", Validations.defaultString(cuenta.nombre)));
                    command.Parameters.Add(new SqlParameter("numero", Validations.defaultString(cuenta.numero)));
                    command.Parameters.Add(new SqlParameter("num_categoria", Validations.defaultString(cuenta.num_categoria)));
                    command.Parameters.Add(new SqlParameter("user_id", cuenta.user.id));
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

        public TransactionResult delete(int id, int sistema)
        {
            SqlConnection connection = null;

            if (sistema == 1)
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString);
            }
            else if (sistema == 2)
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString);
            }

            using (connection)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_deleteCuenta", connection);
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

        public Cuenta detail(int id, int sistema)
        {
            SqlConnection connection = null;

            if (sistema == 1)
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString);
            }
            else if (sistema == 2)
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString);
            }

            using (connection)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_cuentaDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    if (sistema == 1)
                    {
                        return new Cuenta
                        {
                            id = int.Parse(row[0].ToString()),
                            nombre = row[1].ToString(),
                            num_categoria = row[2].ToString(),
                            numero = row[3].ToString(),
                            user = new User { id = int.Parse(row[4].ToString()) },
                            timestamp = Convert.ToDateTime(row[5].ToString()),
                            updated = Convert.ToDateTime(row[6].ToString())
                        };
                    }
                    else if (sistema == 2)
                    {
                        return new Cuenta
                        {
                            id = int.Parse(row[0].ToString()),
                            nombre = row[1].ToString(),
                            num_categoria = row[2].ToString(),
                            numero = row[3].ToString(),
                            tipo_producto = new TipoProducto { id = int.Parse(row[4].ToString()) },
                            user = new User { id = int.Parse(row[5].ToString()) },
                            timestamp = Convert.ToDateTime(row[6].ToString()),
                            updated = Convert.ToDateTime(row[7].ToString())
                        };
                    }
                    else
                    {
                        return null;
                    }

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

        public IList<Cuenta> getAll(int sistema)
        {
            IList<Cuenta> objects = new List<Cuenta>();

            SqlConnection connection = null;

            if (sistema == 1)
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString);
            }
            else if (sistema == 2)
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString);
            }

            using (connection)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllCuenta", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Cuenta
                        {
                            id = int.Parse(row[0].ToString()),
                            nombre = row[1].ToString(),
                            num_categoria = row[2].ToString(),
                            numero = row[3].ToString(),
                            user = new User { id = int.Parse(row[4].ToString()) },
                            timestamp = Convert.ToDateTime(row[5].ToString()),
                            updated = Convert.ToDateTime(row[6].ToString())
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

        public TransactionResult update(Cuenta cuenta, int sistema)
        {
            SqlConnection connection = null;

            if (sistema == 1)
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString);
            }
            else if (sistema == 2)
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString);
            }

            using (connection)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateCuenta", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("nombre", Validations.defaultString(cuenta.nombre)));
                    command.Parameters.Add(new SqlParameter("numero", Validations.defaultString(cuenta.numero)));
                    command.Parameters.Add(new SqlParameter("num_categoria", Validations.defaultString(cuenta.num_categoria)));
                    command.Parameters.Add(new SqlParameter("id", cuenta.id));
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