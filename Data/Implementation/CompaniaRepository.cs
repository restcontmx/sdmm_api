using Data.Interface;
using System;
using System.Collections.Generic;
using Models.Catalogs;
using Warrior.Handlers.Enums;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Models.Auth;
using Warrior.Data;

namespace Data.Implementation
{
    public class CompaniaRepository : ICompaniaRepository
    {

        public TransactionResult create(Compania compania)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createCompania", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("razon_social", compania.razon_social));
                    command.Parameters.Add(new SqlParameter("nombre_sistema", compania.nombre_sistema));
                    command.Parameters.Add(new SqlParameter("cuenta_id", compania.cuenta.id));
                    command.Parameters.Add(new SqlParameter("categoria_id", compania.categoria.id));
                    command.Parameters.Add(new SqlParameter("user_id ", compania.user.id));
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
                    SqlCommand command = new SqlCommand("sp_deleteCompania", connection);
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

        public Compania detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_companiaDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new Compania
                    {
                        id = int.Parse(row[0].ToString()),
                        razon_social = row[1].ToString(),
                        nombre_sistema = row[2].ToString(),
                        timestamp = Convert.ToDateTime(row[3].ToString()),
                        updated = Convert.ToDateTime(row[4].ToString()),
                        user = new User
                        {
                            id = int.Parse(row[5].ToString()),
                            first_name = row[12].ToString(),
                            second_name = row[13].ToString()

                        },
                        cuenta = new Cuenta
                        {
                            id = int.Parse(row[6].ToString()),
                            nombre = row[8].ToString(),
                            numero = row[9].ToString()
                        },
                        categoria = new Categoria
                        {
                            id = int.Parse(row[7].ToString()),
                            nombre = row[10].ToString(),
                            numero = row[11].ToString()
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

        public IList<Compania> getAll()
        {
            SqlConnection connection = null;
            IList<Compania> objects = new List<Compania>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllCompania", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Compania
                        {
                            id = int.Parse(row[0].ToString()),
                            razon_social = row[1].ToString(),
                            nombre_sistema = row[2].ToString(),
                            timestamp = Convert.ToDateTime(row[3].ToString()),
                            updated = Convert.ToDateTime(row[4].ToString()),
                            user = new User
                            {
                                id = int.Parse(row[5].ToString()),
                                first_name = row[12].ToString(),
                                second_name = row[13].ToString()

                            },
                            cuenta = new Cuenta
                            {
                                id = int.Parse(row[6].ToString()),
                                nombre = row[8].ToString(),
                                numero = row[9].ToString()
                            },
                            categoria = new Categoria
                            {
                                id = int.Parse(row[7].ToString()),
                                nombre = row[10].ToString(),
                                numero = row[11].ToString()
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

        public TransactionResult update(Compania compania)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateCompania", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id ", compania.id));
                    command.Parameters.Add(new SqlParameter("razon_social", compania.razon_social));
                    command.Parameters.Add(new SqlParameter("razon_social", compania.razon_social));
                    command.Parameters.Add(new SqlParameter("nombre_sistema", compania.nombre_sistema));
                    command.Parameters.Add(new SqlParameter("cuenta_id", compania.cuenta.id));
                    command.Parameters.Add(new SqlParameter("categoria_id", compania.categoria.id));
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