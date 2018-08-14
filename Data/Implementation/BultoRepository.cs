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
    public class BultoRepository : IBultoRepository
    {
        /// <summary>
        /// Create object on the db
        /// </summary>
        /// <param name="caja"></param>
        /// <returns></returns>
        public TransactionResult create(Bulto bulto)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    int auxInsert = 0;
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createBulto", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("codigo", Validations.defaultString(bulto.codigo)));
                    command.Parameters.Add(new SqlParameter("producto_id", bulto.producto.id));
                    command.Parameters.Add(new SqlParameter("user_id", bulto.user.id));
                    //command.ExecuteNonQuery();


                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];

                    auxInsert = int.Parse(row[0].ToString());

                    if(auxInsert == 1)
                    {
                        return TransactionResult.CREATED;
                    }
                    else
                    {
                        return TransactionResult.EXISTS;
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
                    SqlCommand command = new SqlCommand("sp_deleteBulto", connection);
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
        public Bulto detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_bultoDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new Bulto
                    {
                        id = int.Parse(row[0].ToString()),
                        codigo = row[1].ToString(),
                        producto =  new Producto { id = int.Parse(row[2].ToString()) },
                        user =  new Models.Auth.User { id = int.Parse(row[3].ToString()) },
                        active = (int.Parse(row[4].ToString()) == 1) ? true : false,
                        timestamp = Convert.ToDateTime(row[5].ToString()),
                        updated = Convert.ToDateTime(row[6].ToString())
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
        public IList<Bulto> getAll()
        {
            SqlConnection connection = null;
            IList<Bulto> objects = new List<Bulto>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllBulto", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Bulto
                        {
                            id = int.Parse(row[0].ToString()),
                            codigo = row[1].ToString(),
                            producto = new Producto { id = int.Parse(row[2].ToString()) },
                            user = new Models.Auth.User { id = int.Parse(row[3].ToString()) },
                            active = (int.Parse(row[4].ToString()) == 1) ? true : false,
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



        public TransactionResult createInventario(Inventario inventario)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createInventario", connection);
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
    }
}