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
    public class MaquinariaRepository : IMaquinaRepository
    {
        private CuentaRepository cuentaRepo = new CuentaRepository();

        /// <summary>
        /// Create new object on the db
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public int create(Maquinaria maquina)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createMaquinaria", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("nombre", Validations.defaultString(maquina.nombre)));
                    command.Parameters.Add(new SqlParameter("tipo_id", maquina.tipo_maquinaria.id));

                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return int.Parse(row[0].ToString());

                    //command.ExecuteNonQuery();
                    //return TransactionResult.CREATED;
                }
                catch (SqlException ex)
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    if (ex.Number == 2627)
                    {
                        return 0;
                    }
                    return 0;
                }
                catch
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return 0;
                }
            }
        }


        public TransactionResult createDetalle(DetalleConsumoMaquinaria detalle)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createDetalleConsumo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("maquinaria_id", detalle.maquinaria.id));
                    command.Parameters.Add(new SqlParameter("combustible_id", detalle.combustible.id));
                    command.Parameters.Add(new SqlParameter("promedio", detalle.promedio));
                    command.Parameters.Add(new SqlParameter("tolerancia", detalle.tolerancia));
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

        public TransactionResult createCuenta(Cuenta cuenta, int maquinaria_id)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString);

            using (connection)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createCuenta", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("nombre", Validations.defaultString(cuenta.nombre)));
                    command.Parameters.Add(new SqlParameter("numero", Validations.defaultString(cuenta.numero)));
                    command.Parameters.Add(new SqlParameter("num_categoria", Validations.defaultString(cuenta.num_categoria)));
                    command.Parameters.Add(new SqlParameter("tipoP", cuenta.tipo_producto.id));
                    command.Parameters.Add(new SqlParameter("user_id", cuenta.user.id));
                    command.Parameters.Add(new SqlParameter("maquinaria_id", maquinaria_id));
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
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_deleteMaquinaria", connection);
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

        public TransactionResult deleteDetallesByIdMaquinaria(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_deleteDetallesByIdMaquinaria", connection);
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

        public Maquinaria detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_maquinariaDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new Maquinaria
                    {
                        id = int.Parse(row[0].ToString()),
                        nombre = row[1].ToString(),
                        timestamp = Convert.ToDateTime(row[2].ToString()),
                        updated = Convert.ToDateTime(row[3].ToString()),
                        tipo_maquinaria = new TipoMaquinaria
                        {
                            id = int.Parse(row[4].ToString()),
                            name = row[5].ToString()
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

        public IList<Maquinaria> getAll()
        {
            SqlConnection connection = null;
            IList<Maquinaria> objects = new List<Maquinaria>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllMaquinaria", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Maquinaria
                        {
                            id = int.Parse(row[0].ToString()),
                            nombre = row[1].ToString(),
                            timestamp = Convert.ToDateTime(row[2].ToString()),
                            updated = Convert.ToDateTime(row[3].ToString()),
                            tipo_maquinaria = new TipoMaquinaria
                            {
                                id = int.Parse(row[4].ToString()),
                                name = row[5].ToString()
                            }
                        });
                    }

                    foreach(Maquinaria m in objects)
                    {
                        m.cuentas = getAllCuentasByMaquinariaId(m.id);
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


        public IList<DetalleConsumoMaquinaria> getAllDetallesByMaquinariaId(int id)
        {
            SqlConnection connection = null;
            IList<DetalleConsumoMaquinaria> objects = new List<DetalleConsumoMaquinaria>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllDetalleByIdMaquinaria", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new DetalleConsumoMaquinaria
                        {
                            id = int.Parse(row[0].ToString()),
                            promedio = float.Parse(row[1].ToString()),
                            tolerancia = float.Parse(row[2].ToString()),
                            combustible = new Combustible
                            {
                                id = int.Parse(row[3].ToString()),
                                nombre = row[4].ToString()
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


        public IList<Cuenta> getAllCuentasByMaquinariaId(int id)
        {
            SqlConnection connection = null;
            IList<Cuenta> objects = new List<Cuenta>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllCuentasByIdMaquinaria", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Cuenta
                        {
                            id = int.Parse(row[0].ToString()),
                            nombre = row[1].ToString(),
                            num_categoria = row[2].ToString(),
                            numero = row[3].ToString(),
                            tipo_producto = new TipoProducto { id = int.Parse(row[4].ToString()) }
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
        public TransactionResult update(Maquinaria maquina)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateMaquinaria", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("nombre", Validations.defaultString(maquina.nombre)));
                    command.Parameters.Add(new SqlParameter("tipo_id", maquina.tipo_maquinaria.id));
                    command.Parameters.Add(new SqlParameter("id", maquina.id));
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


        public TransactionResult updateCuenta(Cuenta cuenta)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString);

            using (connection)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateCuenta", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("nombre", Validations.defaultString(cuenta.nombre)));
                    command.Parameters.Add(new SqlParameter("numero", Validations.defaultString(cuenta.numero)));
                    command.Parameters.Add(new SqlParameter("num_categoria", Validations.defaultString(cuenta.num_categoria)));
                    command.Parameters.Add(new SqlParameter("tipoP", cuenta.tipo_producto.id));
                    command.Parameters.Add(new SqlParameter("id", cuenta.id));
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

        public Cuenta cuentaDetail(int id)
        {
            return cuentaRepo.detail(id, 2);
        }
    }
}