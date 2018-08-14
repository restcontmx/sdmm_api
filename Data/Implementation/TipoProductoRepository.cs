using Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.Catalogs;
using Warrior.Handlers.Enums;
using System.Data.SqlClient;
using System.Configuration;
using Warrior.Data;
using System.Data;

namespace Data.Implementation
{
    public class TipoProductoRepository : ITipoProductoRepository
    {
        public TransactionResult create(TipoProducto tipoproducto, int sistema)
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
                    SqlCommand command = new SqlCommand("sp_createTipoProducto", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("name", tipoproducto.name));
                    command.Parameters.Add(new SqlParameter("description", Validations.defaultString(tipoproducto.description)));

                    if (sistema == 1)
                    {
                        command.Parameters.Add(new SqlParameter("value", tipoproducto.value));
                    }
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
                    SqlCommand command = new SqlCommand("sp_deleteTipoProducto", connection);
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

        public TipoProducto detail(int id, int sistema)
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
                    SqlCommand command = new SqlCommand("sp_tipoProductoDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];

                    if (sistema == 1)
                    {
                        return new TipoProducto
                        {
                            id = int.Parse(row[0].ToString()),
                            name = row[1].ToString(),
                            description = row[2].ToString(),
                            value = int.Parse(row[3].ToString())
                        };
                    }
                    else if (sistema == 2)
                    {
                        return new TipoProducto
                        {
                            id = int.Parse(row[0].ToString()),
                            name = row[1].ToString(),
                            description = row[2].ToString()
                        };
                    }
                    else
                    {
                        return new TipoProducto();
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

        public IList<TipoProducto> getAll(int sistema)
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

            IList<TipoProducto> objects = new List<TipoProducto>();

            using (connection)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllTipoProductos", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {

                        if (sistema == 1)
                        {
                            objects.Add(new TipoProducto
                            {
                                id = int.Parse(row[0].ToString()),
                                name = row[1].ToString(),
                                description = row[2].ToString(),
                                value = int.Parse(row[3].ToString())
                            });
                        }
                        else if (sistema == 2)
                        {
                            objects.Add(new TipoProducto
                            {
                                id = int.Parse(row[0].ToString()),
                                name = row[1].ToString(),
                                description = row[2].ToString()
                            });
                        }
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

        public TransactionResult update(TipoProducto tipoproducto, int sistema)
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
                    SqlCommand command = new SqlCommand("sp_updateTipoProducto", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("name", tipoproducto.name));
                    command.Parameters.Add(new SqlParameter("description", Validations.defaultString(tipoproducto.description)));

                    if (sistema == 1)
                    {
                        command.Parameters.Add(new SqlParameter("value", tipoproducto.value));
                    }

                    command.Parameters.Add(new SqlParameter("id", tipoproducto.id));
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