﻿using Data.Interface;
using System;
using System.Collections.Generic;
using Models.Catalogs;
using Warrior.Handlers.Enums;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Warrior.Data;
using Models.Auth;

namespace Data.Implementation
{
    /// <summary>
    /// Empleado repository implementation
    /// </summary>
    public class EmpleadoRepository : IEmpleadoRepository
    {
        /// <summary>
        /// Create new object on the db
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public TransactionResult create(Empleado empleado)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createEmpleado", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("tipoempleado_id", empleado.tipo_empleado.id));
                    command.Parameters.Add(new SqlParameter("nombre", Validations.defaultString(empleado.nombre)));
                    command.Parameters.Add(new SqlParameter("ap_paterno", Validations.defaultString(empleado.ap_paterno)));
                    command.Parameters.Add(new SqlParameter("ap_materno", Validations.defaultString(empleado.ap_materno)));
                    command.Parameters.Add(new SqlParameter("compania_id", empleado.compania.id));
                    command.Parameters.Add(new SqlParameter("status", Validations.setBooleanValue(empleado.status)));
                    command.Parameters.Add(new SqlParameter("user_id", empleado.user.id));
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
                    SqlCommand command = new SqlCommand("sp_deleteEmpleado", connection);
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

        public Empleado detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_empleadoDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new Empleado
                    {
                        id = int.Parse(row[0].ToString()),
                        nombre = row[1].ToString(),
                        ap_paterno = row[2].ToString(),
                        ap_materno = row[3].ToString(),
                        compania = new Compania
                        {
                            id = int.Parse(row[4].ToString()),
                            nombre_sistema = row[5].ToString()
                        },
                        status = int.Parse(row[6].ToString()) == 0 ? false : true,
                        user = new User
                        {
                            id = int.Parse(row[7].ToString()),
                            first_name = row[14].ToString(),
                            second_name = row[15].ToString()
                        },
                        timestamp = Convert.ToDateTime(row[8].ToString()),
                        updated = Convert.ToDateTime(row[9].ToString()),
                        tipo_empleado = new TipoEmpleado
                        {
                            id = int.Parse(row[10].ToString()),
                            name = row[11].ToString(),
                            description = row[12].ToString(),
                            value = int.Parse(row[13].ToString())
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

        public IList<Empleado> getAll()
        {
            SqlConnection connection = null;
            IList<Empleado> objects = new List<Empleado>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllEmpleado", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Empleado
                        {
                            id = int.Parse(row[0].ToString()),
                            nombre = row[1].ToString(),
                            ap_paterno = row[2].ToString(),
                            ap_materno = row[3].ToString(),
                            compania = new Compania
                            {
                                id = int.Parse(row[4].ToString()),
                                nombre_sistema = row[5].ToString()
                            },
                            status = int.Parse(row[6].ToString()) == 0 ? false : true,
                            user = new User { id = int.Parse(row[7].ToString()) },
                            timestamp = Convert.ToDateTime(row[8].ToString()),
                            updated = Convert.ToDateTime(row[9].ToString()),
                            tipo_empleado = new TipoEmpleado {
                                id = int.Parse(row[10].ToString()),
                                name = row[11].ToString(),
                                description = row[12].ToString(),
                                value = int.Parse( row[13].ToString())
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

        public TransactionResult update(Empleado empleado)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateEmpleado", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("tipoempleado_id", empleado.tipo_empleado.id));
                    command.Parameters.Add(new SqlParameter("nombre", Validations.defaultString(empleado.nombre)));
                    command.Parameters.Add(new SqlParameter("ap_paterno", Validations.defaultString(empleado.ap_paterno)));
                    command.Parameters.Add(new SqlParameter("ap_materno", Validations.defaultString(empleado.ap_materno)));
                    command.Parameters.Add(new SqlParameter("compania_id", empleado.compania.id));
                    command.Parameters.Add(new SqlParameter("status", Validations.setBooleanValue(empleado.status)));
                    command.Parameters.Add(new SqlParameter("id", empleado.id));
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