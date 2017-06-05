using Data.Interface;
using System;
using System.Collections.Generic;
using Models.Auth;
using Warrior.Handlers.Enums;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Data.Implementation
{
    /// <summary>
    /// user repository and authentication repository implementations
    /// </summary>
    public class UserRepository : IUserRepository, IAuthenticationRepository
    {
        /// <summary>
        /// Create new authentication model
        /// </summary>
        /// <param name="auth_model"></param>
        /// <returns></returns>
        public TransactionResult create(AuthModel auth_model)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createAuthentication", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("user_id", auth_model.user.id));
                    command.Parameters.Add(new SqlParameter("rol_id", auth_model.rol.id));
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
        /// Create object on the db
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public TransactionResult create(User user)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createUser", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("username", user.username));
                    command.Parameters.Add(new SqlParameter("password", user.password));
                    command.Parameters.Add(new SqlParameter("first_name", user.first_name));
                    command.Parameters.Add(new SqlParameter("second_name", user.second_name));
                    command.Parameters.Add(new SqlParameter("email", user.email));
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
        /// deletes object on the db with id
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
                    SqlCommand command = new SqlCommand("sp_deleteUser", connection);
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
                catch (Exception ex) {
                    if (connection != null) {
                        connection.Close();
                    }
                    return TransactionResult.ERROR;
                }
            }
        }

        /// <summary>
        /// Delete authentication
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TransactionResult deleteAuth(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_deleteAuthentication", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("user_id", id));
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
        /// retrieves object detail from the db with id
        /// </summary>
        /// <param name="id">id for primary key field on the db</param>
        /// <returns></returns>
        public User detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_userDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new User {
                        id = int.Parse( row[0].ToString() ),
                        username = row[1].ToString(),
                        password = row[2].ToString(),
                        first_name = row[3].ToString(),
                        second_name = row[4].ToString(),
                        email = row[5].ToString(),
                        timestamp = Convert.ToDateTime( row[6].ToString() ),
                        updated = Convert.ToDateTime( row[7].ToString() )
                    };

                }
                catch (Exception ex) {
                    if (connection != null) {
                        connection.Close();
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// gets all objects from the db
        /// </summary>
        /// <returns></returns>
        public IList<User> getAll()
        {
            SqlConnection connection = null;
            IList<User> objects = new List<User>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString)) {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllUsers", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows) {
                        objects.Add(new User {
                            id = int.Parse(row[0].ToString()),
                            username = row[1].ToString(),
                            password = row[2].ToString(),
                            first_name = row[3].ToString(),
                            second_name = row[4].ToString(),
                            email = row[5].ToString(),
                            timestamp = Convert.ToDateTime( row[6].ToString() ),
                            updated = Convert.ToDateTime( row[7].ToString() )
                        });
                    }
                    return objects;

                }
                catch (SqlException ex){
                    if (connection != null)
                    {
                        connection.Close();
                    }return objects;
                }
            }

        }

        /// <summary>
        /// Gets all the rols
        /// </summary>
        /// <returns>A list of rols</returns>
        public IList<Rol> getAllRols()
        {
            SqlConnection connection = null;
            IList<Rol> objects = new List<Rol>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllRols", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Rol
                        {
                            id = int.Parse( row[0].ToString() ),
                            name = row[1].ToString(),
                            description = row[2].ToString(),
                            value = int.Parse( row[3].ToString() )
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
        /// Get user by user name 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User getUserByUserName(string username)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getUserByUserName", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("username", username));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new User
                    {
                        id = int.Parse(row[0].ToString()),
                        username = row[1].ToString(),
                        password = row[2].ToString(),
                        first_name = row[3].ToString(),
                        second_name = row[4].ToString(),
                        email = row[5].ToString(),
                        timestamp = Convert.ToDateTime(row[6].ToString()),
                        updated = Convert.ToDateTime(row[7].ToString())
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
        /// updates object on the db
        /// </summary>
        /// <param name="user">object to update</param>
        /// <returns></returns>
        public TransactionResult update(User user)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString)) {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateUser", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", user.id));
                    command.Parameters.Add(new SqlParameter("username", user.username));
                    command.Parameters.Add(new SqlParameter("password", user.password));
                    command.Parameters.Add(new SqlParameter("first_name", user.first_name));
                    command.Parameters.Add(new SqlParameter("second_name", user.second_name));
                    command.Parameters.Add(new SqlParameter("email", user.email));
                    command.ExecuteNonQuery();
                    return TransactionResult.OK;
                }
                catch (SqlException ex)
                {
                    if (connection != null) {
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
                    if (connection != null) {
                        connection.Close();
                    }
                    return TransactionResult.ERROR;
                }
            }
        }

        /// <summary>
        /// Validates user existence and credentials
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public AuthModel validateUser(string username)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_authenticationByUserName", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("username", username));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new AuthModel
                    {
                        id = int.Parse( row[0].ToString() ),
                        rol = new Rol
                        {
                            id = int.Parse(row[1].ToString()),
                            name = row[2].ToString(),
                            description = row[3].ToString(),
                            value = int.Parse( row[4].ToString() )
                        },
                        user = new User
                        {
                            id = int.Parse(row[5].ToString()),
                            username = row[6].ToString(),
                            password = row[7].ToString(),
                            first_name = row[8].ToString(),
                            second_name = row[9].ToString(),
                            email = row[10].ToString(),
                            timestamp = Convert.ToDateTime(row[11].ToString()),
                            updated = Convert.ToDateTime(row[12].ToString())
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
    }
}