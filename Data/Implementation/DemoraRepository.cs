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
    public class DemoraRepository : IDemoraRepository
    {
        public TransactionResult create(Demora demora)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createDemora", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("nombre", Validations.defaultString(demora.nombre)));
                    command.Parameters.Add(new SqlParameter("status", demora.status));
                    command.Parameters.Add(new SqlParameter("user_id", demora.user.id));
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
                    SqlCommand command = new SqlCommand("sp_deleteDemora", connection);
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

        public Demora detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_demoraDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new Demora
                    {
                        id = int.Parse(row[0].ToString()),
                        nombre = row[1].ToString(),
                        status = int.Parse(row[2].ToString()) == 0 ? false : true,
                        timestamp = Convert.ToDateTime(row[3].ToString()),
                        updated = Convert.ToDateTime(row[4].ToString()),
                        user = new User
                        {
                            id = int.Parse(row[5].ToString()),
                            first_name = row[6].ToString(),
                            second_name = row[7].ToString()
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

        public IList<Demora> getAll()
        {
            SqlConnection connection = null;
            IList<Demora> objects = new List<Demora>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllDemora", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Demora
                        {
                            id = int.Parse(row[0].ToString()),
                            nombre = row[1].ToString(),
                            status = int.Parse(row[2].ToString()) == 0 ? false : true,
                            timestamp = Convert.ToDateTime(row[3].ToString()),
                            updated = Convert.ToDateTime(row[4].ToString()),
                            user = new User
                            {
                                id = int.Parse(row[5].ToString()),
                                first_name = row[6].ToString(),
                                second_name = row[7].ToString()
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

        public TransactionResult update(Demora demora)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateDemora", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("nombre", Validations.defaultString(demora.nombre)));
                    command.Parameters.Add(new SqlParameter("status", demora.status));
                    command.Parameters.Add(new SqlParameter("id", demora.id));
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