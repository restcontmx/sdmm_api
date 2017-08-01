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
    public class InventarioRepository : IInventarioRepository
    {
        public TransactionResult create(Inventario inventario)
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
                    SqlCommand command = new SqlCommand("sp_deleteInventariox", connection);
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

        public Inventario detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    /*connection.Open();
                    SqlCommand command = new SqlCommand("sp_segmentoProductoDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new SegmentoProducto
                    {
                        id = int.Parse(row[0].ToString()),
                        name = row[1].ToString(),
                        description = row[2].ToString()
                    };*/
                    return new Inventario();

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

        public IList<Inventario> getAll()
        {
            SqlConnection connection = null;
            IList<SegmentoProducto> objects = new List<SegmentoProducto>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    /* connection.Open();
                     SqlCommand command = new SqlCommand("sp_getAllSegmentoProductos", connection);
                     command.CommandType = CommandType.StoredProcedure;
                     SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                     DataSet data_set = new DataSet();
                     data_adapter.Fill(data_set);
                     foreach (DataRow row in data_set.Tables[0].Rows)
                     {
                         objects.Add(new SegmentoProducto
                         {
                             id = int.Parse(row[0].ToString()),
                             name = row[1].ToString(),
                             description = row[2].ToString()
                         });
                     }
                     return objects;*/

                    return null;

                }
                catch (SqlException ex)
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return null;
                }
            }
        }

        public TransactionResult update(Inventario inventario)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    /*connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateSegmentoProducto", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("name", segmentoproducto.name));
                    command.Parameters.Add(new SqlParameter("description", Validations.defaultString(segmentoproducto.description)));
                    command.Parameters.Add(new SqlParameter("id", segmentoproducto.id));
                    command.ExecuteNonQuery();*/
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