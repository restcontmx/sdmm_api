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

        public TransactionResult create(Compania compania, int sistema)
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
                    SqlCommand command = new SqlCommand("sp_createCompania", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("razon_social", compania.razon_social));
                    command.Parameters.Add(new SqlParameter("nombre_sistema", compania.nombre_sistema));
                    command.Parameters.Add(new SqlParameter("user_id ", compania.user.id));

                    if(sistema == 1)
                    {
                        command.Parameters.Add(new SqlParameter("cuenta_id", compania.cuenta.id));
                        command.Parameters.Add(new SqlParameter("cuenta_propia", compania.cuenta_propia));
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
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
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

        public Compania detail(int id, int sistema)
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
                    if (sistema == 1)
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("sp_companiaDetail", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("id", id));
                        SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                        DataSet data_set = new DataSet();
                        data_adapter.Fill(data_set);
                        DataRow row = data_set.Tables[0].Rows[0];

                        int cuentaPropia = int.Parse(row[7].ToString());

                        if (cuentaPropia != 0)
                        {
                            return new Compania
                            {
                                id = int.Parse(row[0].ToString()),
                                razon_social = row[1].ToString(),
                                nombre_sistema = row[2].ToString(),
                                timestamp = Convert.ToDateTime(row[3].ToString()),
                                updated = Convert.ToDateTime(row[4].ToString()),
                                cuenta_propia = int.Parse(row[5].ToString()),
                                user = new User
                                {
                                    id = int.Parse(row[6].ToString()),
                                    first_name = row[11].ToString(),
                                    second_name = row[12].ToString()

                                },
                                cuenta = new Cuenta
                                {
                                    id = int.Parse(row[7].ToString()),
                                    nombre = row[8].ToString(),
                                    numero = row[9].ToString(),
                                    num_categoria = row[10].ToString()
                                }
                            };
                        }
                        else
                        {
                            return new Compania
                            {
                                id = int.Parse(row[0].ToString()),
                                razon_social = row[1].ToString(),
                                nombre_sistema = row[2].ToString(),
                                timestamp = Convert.ToDateTime(row[3].ToString()),
                                updated = Convert.ToDateTime(row[4].ToString()),
                                cuenta_propia = int.Parse(row[5].ToString()),
                                user = new User
                                {
                                    id = int.Parse(row[6].ToString()),
                                    first_name = row[11].ToString(),
                                    second_name = row[12].ToString()

                                },
                                cuenta = new Cuenta()
                            };
                        }
                    }
                    else if (sistema == 2)
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
                                first_name = row[6].ToString(),
                                second_name = row[7].ToString()

                            },
                            cuenta = new Cuenta()
                        };
                    }else
                    {
                        return null;
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

        public IList<Compania> getAll(int sistema)
        {
            SqlConnection connection = null;
            IList<Compania> objects = new List<Compania>();

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
                    if (sistema == 1)
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("sp_getAllCompania", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                        DataSet data_set = new DataSet();
                        data_adapter.Fill(data_set);


                        foreach (DataRow row in data_set.Tables[0].Rows)
                        {
                            int cuentaPropia = int.Parse(row[7].ToString());

                            if (cuentaPropia != 0)
                            {
                                objects.Add(new Compania
                                {
                                    id = int.Parse(row[0].ToString()),
                                    razon_social = row[1].ToString(),
                                    nombre_sistema = row[2].ToString(),
                                    timestamp = Convert.ToDateTime(row[3].ToString()),
                                    updated = Convert.ToDateTime(row[4].ToString()),
                                    cuenta_propia = int.Parse(row[5].ToString()),
                                    user = new User
                                    {
                                        id = int.Parse(row[6].ToString()),
                                        first_name = row[11].ToString(),
                                        second_name = row[12].ToString()

                                    },
                                    cuenta = new Cuenta
                                    {
                                        id = int.Parse(row[7].ToString()),
                                        nombre = row[8].ToString(),
                                        numero = row[9].ToString(),
                                        num_categoria = row[10].ToString()
                                    }
                                });
                            }
                            else
                            {
                                objects.Add(new Compania
                                {
                                    id = int.Parse(row[0].ToString()),
                                    razon_social = row[1].ToString(),
                                    nombre_sistema = row[2].ToString(),
                                    timestamp = Convert.ToDateTime(row[3].ToString()),
                                    updated = Convert.ToDateTime(row[4].ToString()),
                                    cuenta_propia = int.Parse(row[5].ToString()),
                                    user = new User
                                    {
                                        id = int.Parse(row[6].ToString()),
                                        first_name = row[11].ToString(),
                                        second_name = row[12].ToString()

                                    },
                                    cuenta = new Cuenta()
                                });
                            }
                        }

                    }
                    else if (sistema == 2)
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
                                    first_name = row[6].ToString(),
                                    second_name = row[7].ToString()

                                },
                                cuenta = new Cuenta()
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

        public TransactionResult update(Compania compania, int sistema)
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
                    SqlCommand command = new SqlCommand("sp_updateCompania", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id ", compania.id));
                    command.Parameters.Add(new SqlParameter("razon_social", compania.razon_social));
                    command.Parameters.Add(new SqlParameter("nombre_sistema", compania.nombre_sistema));

                    if (sistema == 1)
                    {
                        command.Parameters.Add(new SqlParameter("cuenta_id", compania.cuenta.id));
                        command.Parameters.Add(new SqlParameter("cuenta_propia", compania.cuenta_propia));
                    }
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