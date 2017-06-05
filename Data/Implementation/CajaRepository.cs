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
    /// <summary>
    /// Caja repository implementation
    /// </summary>
    public class CajaRepository : ICajaRepository
    {
        /// <summary>
        /// Create object on the db
        /// </summary>
        /// <param name="caja"></param>
        /// <returns></returns>
        public TransactionResult create(Caja caja)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createCaja", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("folio_ini", Validations.defaultString(caja.folio_ini)));
                    command.Parameters.Add(new SqlParameter("folio_fin", Validations.defaultString(caja.folio_fin)));
                    command.Parameters.Add(new SqlParameter("codigo", Validations.defaultString(caja.codigo)));
                    command.Parameters.Add(new SqlParameter("cantidad", caja.cantidad));
                    command.Parameters.Add(new SqlParameter("producto_id", caja.producto.id));
                    //command.Parameters.Add(new SqlParameter("active", Validations.setBooleanValue(caja.active)));
                    command.Parameters.Add(new SqlParameter("user_id", caja.user.id));
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

        /// <summary>
        /// Delete object by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TransactionResult delete(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_deleteCaja", connection);
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

        /// <summary>
        /// Retrieve object from the db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Caja detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_cajaDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new Caja
                    {
                        id = int.Parse(row[0].ToString()),
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
        /// <summary>
        /// Get all objects
        /// </summary>
        /// <returns></returns>
        public IList<Caja> getAll()
        {
            SqlConnection connection = null;
            IList<Caja> objects = new List<Caja>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllCaja", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Caja
                        {
                            id = int.Parse(row[0].ToString()),
                            codigo = row[1].ToString(),
                            folio_ini = row[2].ToString(),
                            folio_fin = row[3].ToString(),
                            cantidad = int.Parse(row[4].ToString()),
                            producto = new Producto { id = int.Parse(row[5].ToString()) },
                            user = new Models.Auth.User { id = int.Parse(row[6].ToString()) },
                            active = int.Parse(row[7].ToString()) == 1 ? true : false,
                            timestamp = Convert.ToDateTime(row[8].ToString()),
                            updated = Convert.ToDateTime(row[9].ToString())
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

        /// <summary>
        /// Update object on the db
        /// </summary>
        /// <param name="caja"></param>
        /// <returns></returns>
        public TransactionResult update(Caja caja)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateCaja", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("folio_ini", Validations.defaultString(caja.folio_ini)));
                    command.Parameters.Add(new SqlParameter("folio_fin", Validations.defaultString(caja.folio_fin)));
                    command.Parameters.Add(new SqlParameter("codigo", Validations.defaultString(caja.codigo)));
                    command.Parameters.Add(new SqlParameter("cantidad", caja.cantidad));
                    command.Parameters.Add(new SqlParameter("producto_id", caja.producto.id));
                    command.Parameters.Add(new SqlParameter("active", Validations.setBooleanValue(caja.active)));
                    command.Parameters.Add(new SqlParameter("id", caja.id));

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