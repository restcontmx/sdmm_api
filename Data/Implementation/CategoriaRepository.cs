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
    public class CategoriaRepository : ICategoriaRepository
    {
        public TransactionResult create(Categoria categoria)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createCategoria", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("nivel_id", categoria.nivel.id));
                    command.Parameters.Add(new SqlParameter("procesominero_id", categoria.procesominero.id));
                    command.Parameters.Add(new SqlParameter("nombre", Validations.defaultString( categoria.nombre )));
                    command.Parameters.Add(new SqlParameter("user_id", categoria.user.id));
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
                    SqlCommand command = new SqlCommand("sp_deleteCategoria", connection);
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

        public Categoria detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_categoriaDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new Categoria
                    {
                        id = int.Parse(row[0].ToString()),
                        nombre = row[1].ToString(),
                        user = new User { id = int.Parse(row[2].ToString()) },
                        timestamp = Convert.ToDateTime(row[3].ToString()),
                        updated = Convert.ToDateTime(row[4].ToString()),
                        nivel = new Nivel
                        {
                            id = int.Parse(row[5].ToString()),
                            nombre = row[6].ToString(),
                            codigo = row[7].ToString()
                        },
                        procesominero = new ProcesoMinero
                        {
                            id = int.Parse(row[8].ToString()),
                            nombre = row[9].ToString(),
                            codigo = row[10].ToString()
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

        public IList<Categoria> getAll()
        {
            SqlConnection connection = null;
            IList<Categoria> objects = new List<Categoria>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllCategoria", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Categoria
                        {
                            id = int.Parse(row[0].ToString()),
                            nombre = row[1].ToString(),
                            user = new User { id = int.Parse(row[2].ToString()) },
                            timestamp = Convert.ToDateTime(row[3].ToString()),
                            updated = Convert.ToDateTime(row[4].ToString()),
                            nivel = new Nivel {
                                id = int.Parse(row[5].ToString()),
                                nombre = row[6].ToString(),
                                codigo = row[7].ToString()
                            },
                            procesominero = new ProcesoMinero {
                                id = int.Parse(row[8].ToString()),
                                nombre = row[9].ToString(),
                                codigo = row[10].ToString()
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

        public TransactionResult update(Categoria categoria)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateCategoria", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", categoria.id));
                    command.Parameters.Add(new SqlParameter("nivel_id", categoria.nivel.id));
                    command.Parameters.Add(new SqlParameter("procesominero_id", categoria.procesominero.id));
                    command.Parameters.Add(new SqlParameter("nombre", Validations.defaultString(categoria.nombre)));
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