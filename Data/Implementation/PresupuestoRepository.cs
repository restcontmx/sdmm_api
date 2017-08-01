using Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.Catalogs;
using Warrior.Handlers.Enums;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Warrior.Data;
using Models.Auth;

namespace Data.Implementation
{
    public class PresupuestoRepository : IPresupuestoRepository
    {
        public TransactionResult create(Presupuesto presupuesto)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createPresupuesto", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("producto_id", presupuesto.producto.id));
                    command.Parameters.Add(new SqlParameter("presupuesto", presupuesto.presupuesto));
                    command.Parameters.Add(new SqlParameter("stock", presupuesto.stock));
                    command.Parameters.Add(new SqlParameter("year", presupuesto.year));
                    command.Parameters.Add(new SqlParameter("user_id", presupuesto.user.id));
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
                    SqlCommand command = new SqlCommand("sp_deletePresupuesto", connection);
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

        public Presupuesto detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_presupuestoDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new Presupuesto
                    {
                        id = int.Parse(row[0].ToString()),
                        presupuesto = decimal.Parse(row[1].ToString()),
                        stock = int.Parse(row[2].ToString()),
                        year = int.Parse(row[3].ToString()),
                        user = new User
                        {
                            id = int.Parse(row[4].ToString()),
                            first_name = row[13].ToString(),
                            second_name = row[14].ToString()
                        },
                        timestamp = Convert.ToDateTime(row[5].ToString()),
                        updated = Convert.ToDateTime(row[6].ToString()),
                        producto = new Producto
                        {
                            id = int.Parse(row[7].ToString()),
                            tipo_producto = new TipoProducto { id = int.Parse(row[8].ToString()) },
                            codigo = row[9].ToString(),
                            nombre = row[10].ToString(),
                            costo = decimal.Parse(row[11].ToString()),
                            peso = decimal.Parse(row[12].ToString())
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

        public IList<Presupuesto> getAll()
        {
            SqlConnection connection = null;
            IList<Presupuesto> objects = new List<Presupuesto>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllPresupuesto", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Presupuesto
                        {
                            id = int.Parse(row[0].ToString()),
                            presupuesto = decimal.Parse(row[1].ToString()),
                            stock = int.Parse(row[2].ToString()),
                            year = int.Parse(row[3].ToString()),
                            user = new User { id = int.Parse(row[4].ToString()) },
                            timestamp = Convert.ToDateTime(row[5].ToString()),
                            updated = Convert.ToDateTime(row[6].ToString()),
                            producto = new Producto
                            {
                                id = int.Parse(row[7].ToString()),
                                tipo_producto = new TipoProducto { id = int.Parse(row[8].ToString()) },
                                codigo = row[9].ToString(),
                                nombre = row[10].ToString(),
                                costo = decimal.Parse(row[11].ToString()),
                                peso = decimal.Parse(row[12].ToString())
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

        public TransactionResult update(Presupuesto presupuesto)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updatePresupuesto", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("producto_id", presupuesto.producto.id));
                    command.Parameters.Add(new SqlParameter("presupuesto", presupuesto.presupuesto));
                    command.Parameters.Add(new SqlParameter("stock", presupuesto.stock));
                    command.Parameters.Add(new SqlParameter("year", presupuesto.year));
                    command.Parameters.Add(new SqlParameter("id", presupuesto.id));
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