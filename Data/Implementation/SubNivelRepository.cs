﻿using Data.Interface;
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
    public class SubNivelRepository : ISubNivelRepository
    {
        public TransactionResult create(SubNivel subnivel)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createSubNivel", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("status", subnivel.status?1:0));
                    command.Parameters.Add(new SqlParameter("nombre", Validations.defaultString(subnivel.nombre)));
                    command.Parameters.Add(new SqlParameter("user_id", subnivel.user.id));
                    command.Parameters.Add(new SqlParameter("nivel_id", subnivel.nivel.id));
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
                    SqlCommand command = new SqlCommand("sp_deleteSubNivel", connection);
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

        public SubNivel detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_subNivelDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new SubNivel
                    {
                        id = int.Parse(row[0].ToString()),
                        nombre = row[1].ToString(),
                        status = int.Parse(row[2].ToString()) == 0 ? false : true,
                        user = new User { id = int.Parse(row[3].ToString()) },
                        timestamp = Convert.ToDateTime(row[4].ToString()),
                        updated = Convert.ToDateTime(row[5].ToString()),
                        nivel = new Nivel
                        {
                            id = int.Parse(row[6].ToString()),
                            nombre = row[7].ToString(),
                            codigo = row[8].ToString(),
                            user = new User { id = int.Parse(row[9].ToString()) },
                            timestamp = Convert.ToDateTime(row[10].ToString()),
                            updated = Convert.ToDateTime(row[11].ToString())
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

        public IList<SubNivel> getAll()
        {
            SqlConnection connection = null;
            IList<SubNivel> objects = new List<SubNivel>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllSubNivel", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new SubNivel
                        {
                            id = int.Parse(row[0].ToString()),
                            nombre = row[1].ToString(),
                            status = int.Parse(row[2].ToString()) == 0 ? false : true,
                            user = new User { id = int.Parse(row[3].ToString())},
                            timestamp = Convert.ToDateTime(row[4].ToString()),
                            updated = Convert.ToDateTime(row[5].ToString()),
                            nivel = new Nivel
                            {
                                id = int.Parse( row[6].ToString() ),
                                nombre = row[7].ToString(),
                                codigo = row[8].ToString(),
                                user = new User { id = int.Parse(row[9].ToString())},
                                timestamp = Convert.ToDateTime(row[10].ToString()),
                                updated = Convert.ToDateTime(row[11].ToString())
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

        public TransactionResult update(SubNivel subnivel)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateSubNivel", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("status", subnivel.status ? 1 : 0));
                    command.Parameters.Add(new SqlParameter("nombre", Validations.defaultString(subnivel.nombre)));
                    command.Parameters.Add(new SqlParameter("id", subnivel.id));
                    command.Parameters.Add(new SqlParameter("user_id", subnivel.user.id));
                    command.Parameters.Add(new SqlParameter("nivel_id", subnivel.nivel.id));
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


        public IList<string> getNombresLugares()
        {
            SqlConnection connection = null;
            IList<string> objects = new List<string>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllSubNivel", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(row[8].ToString() + "-" + row[7].ToString() + "-" + row[1].ToString());
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
    }
}