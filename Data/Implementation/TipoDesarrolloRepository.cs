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
    public class TipoDesarrolloRepository : ITipoDesarrolloRepository
    {
        public TransactionResult create(TipoDesarrollo tipo_des)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createTipoDesarrollo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("nombre", tipo_des.nombre));
                    command.Parameters.Add(new SqlParameter("description", Validations.defaultString(tipo_des.description)));
                    command.Parameters.Add(new SqlParameter("status", tipo_des.status));
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
                    SqlCommand command = new SqlCommand("sp_deleteTipoDesarrollo", connection);
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

        public TipoDesarrollo detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_tipoDesarrolloDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new TipoDesarrollo
                    {
                        id = int.Parse(row[0].ToString()),
                        nombre = row[1].ToString(),
                        description = row[2].ToString(),
                        status = int.Parse(row[3].ToString()) == 0 ? false : true,
                        timestamp = Convert.ToDateTime(row[4].ToString()),
                        updated = Convert.ToDateTime(row[5].ToString())
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

        public IList<TipoDesarrollo> getAll()
        {
            SqlConnection connection = null;
            IList<TipoDesarrollo> objects = new List<TipoDesarrollo>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllTipoDesarrollo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new TipoDesarrollo
                        {
                            id = int.Parse(row[0].ToString()),
                            nombre = row[1].ToString(),
                            description = row[2].ToString(),
                            status = int.Parse(row[3].ToString()) == 0 ? false : true,
                            timestamp = Convert.ToDateTime(row[4].ToString()),
                            updated = Convert.ToDateTime(row[5].ToString())
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

        public TransactionResult update(TipoDesarrollo tipo_des)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateTipoDesarrollo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("nombre", Validations.defaultString(tipo_des.nombre)));
                    command.Parameters.Add(new SqlParameter("description", Validations.defaultString(tipo_des.description)));
                    command.Parameters.Add(new SqlParameter("status", tipo_des.status));
                    command.Parameters.Add(new SqlParameter("id", tipo_des.id));
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