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
using Models.Auth;
using Warrior.Data;

namespace Data.Implementation
{
    /// <summary>
    /// Proveedor repository implementation
    /// </summary>
    public class ProveedorRepository : IProveedorRepository
    {
        /// <summary>
        /// Create new object on the db
        /// </summary>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        public TransactionResult create(Proveedor proveedor)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createProveedor", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("razon_social", Validations.defaultString( proveedor.razon_social )));
                    command.Parameters.Add(new SqlParameter("nombre_comercial", proveedor.nombre_comercial));
                    command.Parameters.Add(new SqlParameter("rfc", proveedor.rfc));
                    command.Parameters.Add(new SqlParameter("codigo_proveedor", proveedor.codigo_proveedor));
                    command.Parameters.Add(new SqlParameter("permiso_sedena", proveedor.permiso_sedena));
                    command.Parameters.Add(new SqlParameter("calle", Validations.defaultString( proveedor.calle )));
                    command.Parameters.Add(new SqlParameter("no_ext", proveedor.no_ext));
                    command.Parameters.Add(new SqlParameter("no_int", proveedor.no_int));
                    command.Parameters.Add(new SqlParameter("colonia", Validations.defaultString( proveedor.colonia )));
                    command.Parameters.Add(new SqlParameter("cp", proveedor.cp));
                    command.Parameters.Add(new SqlParameter("localidad", Validations.defaultString( proveedor.localidad )));
                    command.Parameters.Add(new SqlParameter("ciudad", Validations.defaultString( proveedor.ciudad )));
                    command.Parameters.Add(new SqlParameter("estado", Validations.defaultString(proveedor.estado)));
                    command.Parameters.Add(new SqlParameter("user_id", proveedor.user.id));
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
                    SqlCommand command = new SqlCommand("sp_deleteProveedor", connection);
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
        /// Retrieve object by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Proveedor detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_proveedorDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new Proveedor
                    {
                        id = int.Parse(row[0].ToString()),
                        razon_social = row[1].ToString(),
                        nombre_comercial = row[2].ToString(),
                        rfc = row[3].ToString(),
                        codigo_proveedor = row[4].ToString(),
                        permiso_sedena = row[5].ToString(),
                        calle = row[6].ToString(),
                        no_ext = int.Parse(row[7].ToString()),
                        no_int = int.Parse(row[8].ToString()),
                        colonia = row[9].ToString(),
                        cp = int.Parse(row[10].ToString()),
                        localidad = row[11].ToString(),
                        ciudad = row[12].ToString(),
                        estado = row[13].ToString(),
                        user = new User { id = int.Parse(row[14].ToString()) },
                        timestamp = Convert.ToDateTime(row[15].ToString()),
                        updated = Convert.ToDateTime(row[16].ToString())
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
        /// Get all objects from the db
        /// </summary>
        /// <returns></returns>
        public IList<Proveedor> getAll()
        {
            SqlConnection connection = null;
            IList<Proveedor> objects = new List<Proveedor>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllProveedores", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Proveedor
                        {
                            id = int.Parse(row[0].ToString()),
                            razon_social = row[1].ToString(),
                            nombre_comercial = row[2].ToString(),
                            rfc = row[3].ToString(),
                            codigo_proveedor = row[4].ToString(),
                            permiso_sedena = row[5].ToString(),
                            calle = row[6].ToString(),
                            no_ext = int.Parse(row[7].ToString()),
                            no_int = int.Parse(row[8].ToString()),
                            colonia = row[9].ToString(),
                            cp = int.Parse( row[10].ToString() ),
                            localidad = row[11].ToString(),
                            ciudad = row[12].ToString(),
                            estado = row[13].ToString(),
                            user = new User {  id = int.Parse(row[14].ToString()) },
                            timestamp = Convert.ToDateTime(row[15].ToString()),
                            updated = Convert.ToDateTime(row[16].ToString())
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
        /// <param name="proveedor"></param>
        /// <returns></returns>
        public TransactionResult update(Proveedor proveedor)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateProveedor", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", proveedor.id));
                    command.Parameters.Add(new SqlParameter("razon_social", Validations.defaultString(proveedor.razon_social)));
                    command.Parameters.Add(new SqlParameter("nombre_comercial", proveedor.nombre_comercial));
                    command.Parameters.Add(new SqlParameter("rfc", proveedor.rfc));
                    command.Parameters.Add(new SqlParameter("codigo_proveedor", proveedor.codigo_proveedor));
                    command.Parameters.Add(new SqlParameter("permiso_sedena", proveedor.permiso_sedena));
                    command.Parameters.Add(new SqlParameter("calle", Validations.defaultString(proveedor.calle)));
                    command.Parameters.Add(new SqlParameter("no_ext", proveedor.no_ext));
                    command.Parameters.Add(new SqlParameter("no_int", proveedor.no_int));
                    command.Parameters.Add(new SqlParameter("colonia", Validations.defaultString(proveedor.colonia)));
                    command.Parameters.Add(new SqlParameter("cp", proveedor.cp));
                    command.Parameters.Add(new SqlParameter("localidad", Validations.defaultString(proveedor.localidad)));
                    command.Parameters.Add(new SqlParameter("ciudad", Validations.defaultString(proveedor.ciudad)));
                    command.Parameters.Add(new SqlParameter("estado", Validations.defaultString(proveedor.estado)));
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