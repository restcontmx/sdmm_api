using Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.Catalogs;
using Warrior.Handlers.Enums;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Data.Implementation
{
    public class ProductoRepository : IProductoRepository
    {
        public TransactionResult create(Producto producto)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createProducto", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("presentacion", producto.presentacion));
                    command.Parameters.Add(new SqlParameter("codigo", producto.codigo));
                    command.Parameters.Add(new SqlParameter("nombre", producto.nombre));
                    command.Parameters.Add(new SqlParameter("cantidad", producto.cantidad));
                    command.Parameters.Add(new SqlParameter("costo", producto.costo));
                    command.Parameters.Add(new SqlParameter("peso", producto.peso));
                    command.Parameters.Add(new SqlParameter("modo", producto.modo));
                    command.Parameters.Add(new SqlParameter("tipoproducto_id", producto.tipo_producto.id));
                    command.Parameters.Add(new SqlParameter("user_id", producto.user.id));
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
                    SqlCommand command = new SqlCommand("sp_deleteProducto", connection);
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

        public Producto detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_productoDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new Producto
                    {
                        id = int.Parse(row[0].ToString()),
                        presentacion = row[1].ToString(),
                        codigo = row[2].ToString(),
                        nombre = row[3].ToString(),
                        cantidad = int.Parse(row[4].ToString()),
                        costo = decimal.Parse(row[5].ToString()),
                        peso = decimal.Parse(row[6].ToString()),
                        modo = row[7].ToString(),
                        user = new Models.Auth.User { id = int.Parse(row[8].ToString()) },
                        timestamp = Convert.ToDateTime(row[9].ToString()),
                        updated = Convert.ToDateTime(row[10].ToString()),
                        tipo_producto = new TipoProducto
                        {
                            id = int.Parse(row[11].ToString()),
                            name = row[12].ToString(),
                            description = row[13].ToString(),
                            value = int.Parse(row[14].ToString())
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

        public IList<Producto> getAll()
        {
            SqlConnection connection = null;
            IList<Producto> objects = new List<Producto>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllProducto", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Producto
                        {
                            id = int.Parse(row[0].ToString()),
                            presentacion = row[1].ToString(),
                            codigo = row[2].ToString(),
                            nombre = row[3].ToString(),
                            cantidad = int.Parse(row[4].ToString()),
                            costo = decimal.Parse(row[5].ToString()),
                            peso = decimal.Parse(row[6].ToString()),
                            modo = row[7].ToString(),
                            user = new Models.Auth.User { id = int.Parse(row[8].ToString()) },
                            timestamp = Convert.ToDateTime(row[9].ToString()),
                            updated = Convert.ToDateTime(row[10].ToString()),
                            tipo_producto = new TipoProducto
                            {
                                id = int.Parse(row[11].ToString()),
                                name = row[12].ToString(),
                                description = row[13].ToString(),
                                value = int.Parse(row[14].ToString())
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

        public TransactionResult update(Producto producto)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateProducto", connection);
                    command.Parameters.Add(new SqlParameter("presentacion", producto.presentacion));
                    command.Parameters.Add(new SqlParameter("codigo", producto.codigo));
                    command.Parameters.Add(new SqlParameter("nombre", producto.nombre));
                    command.Parameters.Add(new SqlParameter("cantidad", producto.cantidad));
                    command.Parameters.Add(new SqlParameter("costo", producto.costo));
                    command.Parameters.Add(new SqlParameter("peso", producto.peso));
                    command.Parameters.Add(new SqlParameter("modo", producto.modo));
                    command.Parameters.Add(new SqlParameter("tipoproducto_id", producto.tipo_producto.id));
                    command.Parameters.Add(new SqlParameter("id", producto.id));
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