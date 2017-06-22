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
using Warrior.Data;
using Models.Auth;

namespace Data.Implementation
{
    public class ValeRepository : IValeRepository
    {
        public int create(Vale vale)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createVale", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("turno", Validations.defaultString(vale.turno)));
                    command.Parameters.Add(new SqlParameter("lugar", Validations.defaultString(vale.lugar)));
                    command.Parameters.Add(new SqlParameter("compania_id", vale.compania.id));
                    command.Parameters.Add(new SqlParameter("polvorero_id", vale.polvorero.id));
                    command.Parameters.Add(new SqlParameter("cargador1_id", vale.cargador1.id));
                    command.Parameters.Add(new SqlParameter("cargador2_id", vale.cargador2.id));
                    command.Parameters.Add(new SqlParameter("user_id", vale.user.id));
                    command.Parameters.Add(new SqlParameter("cuenta_id", vale.cuenta.id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return int.Parse(row[0].ToString());
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

        public TransactionResult createDetalle(DetalleVale detalle)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createDetalleVale", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("vale_id", detalle.vale.id));
                    command.Parameters.Add(new SqlParameter("producto_id", detalle.producto.id));
                    command.Parameters.Add(new SqlParameter("cantidad", detalle.cantidad));
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

        public TransactionResult createRegistroDetalle(RegistroDetalle registro)
        {
            bool changePro = false;

            if(registro.folio.Length > 16)
            {
                changePro = true;
            }
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    SqlCommand command;
                    connection.Open();
                    if (changePro)
                    {
                        command = new SqlCommand("sp_createRegistroDetalleBulto", connection);
                    }
                    else
                    {
                        command = new SqlCommand("sp_createRegistroDetalle", connection);
                    }
                    
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("detallevale_id", registro.detallevale.id));
                    command.Parameters.Add(new SqlParameter("folio", registro.folio));
                    command.Parameters.Add(new SqlParameter("user_id", registro.user.id));
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


        public TransactionResult createRegistroDetalleOver(RegistroDetalle registro)
        {

            string folioCaja = registro.folio.Substring(16, 30);

            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    SqlCommand command;
                    connection.Open();
                    command = new SqlCommand("sp_createRegistroDetalleOver", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("detallevale_id", registro.detallevale.id));
                    command.Parameters.Add(new SqlParameter("folio", registro.folio));
                    command.Parameters.Add(new SqlParameter("codigoCaja", folioCaja));
                    command.Parameters.Add(new SqlParameter("user_id", registro.user.id));
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
                    SqlCommand command = new SqlCommand("sp_deleteVale", connection);
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

        public Vale detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_valeDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new Vale
                    {
                        id = int.Parse(row[0].ToString()),
                        turno = row[1].ToString(),
                        lugar = row[2].ToString(),
                        user = new User
                        {
                            id = int.Parse(row[3].ToString())
                        ,

                            first_name = row[15].ToString(),
                            second_name = row[16].ToString()


                        },
                        timestamp = Convert.ToDateTime(row[4].ToString()),
                        updated = Convert.ToDateTime(row[5].ToString()),
                        compania = new Proveedor
                        {
                            id = int.Parse(row[6].ToString()),
                            nombre_comercial = row[7].ToString()
                        },
                        polvorero = new Empleado
                        {
                            id = int.Parse(row[8].ToString()),
                            nombre = row[9].ToString(),
                            ap_paterno = row[10].ToString(),
                            ap_materno = row[11].ToString()
                        },
                        cargador1 = new Empleado { id = int.Parse(row[12].ToString()) },
                        cargador2 = new Empleado { id = int.Parse(row[13].ToString()) },
                        cuenta = new Cuenta { id = int.Parse(row[14].ToString()) },
                        active = int.Parse(row[17].ToString())
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

        public IList<Vale> getAll()
        {
            SqlConnection connection = null;
            IList<Vale> objects = new List<Vale>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllVale", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {

                        objects.Add(new Vale
                        {
                            id = int.Parse(row[0].ToString()),
                            turno = row[1].ToString(),
                            lugar = row[2].ToString(),
                            user = new User { id = int.Parse(row[3].ToString()) },
                            timestamp = Convert.ToDateTime(row[4].ToString()),
                            updated = Convert.ToDateTime(row[5].ToString()),
                            compania = new Proveedor
                            {
                                id = int.Parse(row[6].ToString()),
                                nombre_comercial = row[7].ToString()
                            },
                            polvorero = new Empleado
                            {
                                id = int.Parse(row[8].ToString()),
                                nombre = row[9].ToString(),
                                ap_paterno = row[10].ToString(),
                                ap_materno = row[11].ToString()
                            },
                            cargador1 = new Empleado { id = int.Parse(row[12].ToString()) },
                            cargador2 = new Empleado { id = int.Parse(row[13].ToString()) },
                            cuenta = new Cuenta { id = int.Parse(row[14].ToString()) },
                            active = int.Parse(row[15].ToString())
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

        public IList<DetalleVale> getAllDetalles(int vale_id)
        {
            SqlConnection connection = null;
            IList<DetalleVale> objects = new List<DetalleVale>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllDetalleValeByVale", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("vale_id", vale_id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new DetalleVale
                        {
                            id = int.Parse(row[0].ToString()),
                            producto = new Producto {
                                id = int.Parse(row[1].ToString()),
                                nombre = row[2].ToString()
                            },
                            cantidad = int.Parse(row[3].ToString()),
                            vale = new Vale { id = int.Parse(row[4].ToString()) }
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

        public IList<RegistroDetalle> getAllRegistersByDetalle(int detalle_id)
        {
            SqlConnection connection = null;
            IList<RegistroDetalle> objects = new List<RegistroDetalle>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllRegistroDetalleByDetalleValeId", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("detallevale_id", detalle_id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new RegistroDetalle
                        {
                            id = int.Parse(row[0].ToString()),
                            folio = row[1].ToString(),
                            detallevale = new DetalleVale { id = int.Parse( row[3].ToString() ) },
                            user = new User { id = int.Parse(row[4].ToString()) },
                            timestamp = Convert.ToDateTime(row[5].ToString()),
                            updated = Convert.ToDateTime(row[6].ToString())
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

        public IList<RegistroDetalle> getAllRegistersOverByDetalle(int detalle_id)
        {
            SqlConnection connection = null;
            IList<RegistroDetalle> objects = new List<RegistroDetalle>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllRegistroDetalleOverByDetalleValeId", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("detallevale_id", detalle_id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new RegistroDetalle
                        {
                            id = int.Parse(row[0].ToString()),
                            folio = row[1].ToString(),
                            detallevale = new DetalleVale { id = int.Parse(row[3].ToString()) },
                            user = new User { id = int.Parse(row[4].ToString()) },
                            timestamp = Convert.ToDateTime(row[5].ToString()),
                            updated = Convert.ToDateTime(row[6].ToString())
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
        /// 
        /// </summary>
        /// <param name="vale"></param>
        /// <returns></returns>
        public IList<RegistroDetalle> getAllRegistersByFolioCaja(string folioCaja)
        {
            int folioIni = int.Parse(folioCaja.Substring(9, 6));
            int folioFin = int.Parse(folioCaja.Substring(15, 6));
            SqlConnection connection = null;
            IList<RegistroDetalle> objects = new List<RegistroDetalle>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllRegistroDetalleByFolioCaja", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("folioCaja", folioCaja));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);

                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        RegistroDetalle rgd = new RegistroDetalle
                        {
                            id = int.Parse(row[0].ToString()),
                            folio = row[1].ToString(),
                            detallevale = new DetalleVale { id = int.Parse(row[3].ToString()) },
                            user = new User { id = int.Parse(row[4].ToString()) },
                            timestamp = Convert.ToDateTime(row[5].ToString()),
                            updated = Convert.ToDateTime(row[6].ToString())
                        };

                        int folioS = int.Parse(rgd.folio.Substring(10, 6));

                        if (folioS <= folioFin && folioS >= folioIni)
                        {
                            objects.Add(rgd);
                        }

                    }

                    SqlCommand command2 = new SqlCommand("sp_getAllRegistroDetaOverlleByFolioCaja", connection);
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.Parameters.Add(new SqlParameter("folioCaja", folioCaja));
                    SqlDataAdapter data_adapter2 = new SqlDataAdapter(command2);
                    DataSet data_set2 = new DataSet();
                    data_adapter2.Fill(data_set2);
                    foreach (DataRow row in data_set2.Tables[0].Rows)
                    {
                        RegistroDetalle rgd = new RegistroDetalle
                        {
                            id = int.Parse(row[0].ToString()),
                            folio = row[1].ToString(),
                            detallevale = new DetalleVale { id = int.Parse(row[3].ToString()) },
                            user = new User { id = int.Parse(row[4].ToString()) },
                            timestamp = Convert.ToDateTime(row[5].ToString()),
                            updated = Convert.ToDateTime(row[6].ToString())
                        };

                        objects.Add(rgd);
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
        /// 
        /// </summary>
        /// <param name="vale"></param>
        /// <returns></returns>
        public IList<RegistroDetalle> getAllRegistersSacos()
        {
            SqlConnection connection = null;
            IList<RegistroDetalle> objects = new List<RegistroDetalle>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllRegistroSacos", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        RegistroDetalle rgd = new RegistroDetalle
                        {
                            id = int.Parse(row[0].ToString()),
                            folio = row[1].ToString(),
                            detallevale = new DetalleVale { id = int.Parse(row[3].ToString()) },
                            user = new User { id = int.Parse(row[4].ToString()) },
                            timestamp = Convert.ToDateTime(row[5].ToString()),
                            updated = Convert.ToDateTime(row[6].ToString())
                        };

                        objects.Add(rgd);
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

        public TransactionResult update(Vale vale)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateVale", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id ", vale.id));
                    command.Parameters.Add(new SqlParameter("compania_id", vale.compania.id));
                    command.Parameters.Add(new SqlParameter("turno", Validations.defaultString(vale.turno)));
                    command.Parameters.Add(new SqlParameter("lugar", Validations.defaultString(vale.lugar)));
                    command.Parameters.Add(new SqlParameter("polvorero_id", vale.polvorero.id));
                    command.Parameters.Add(new SqlParameter("cargador1_id", vale.cargador1.id));
                    command.Parameters.Add(new SqlParameter("cargador2_id", vale.cargador2.id));
                    command.Parameters.Add(new SqlParameter("cuenta_id", vale.cuenta.id));
                    command.Parameters.Add(new SqlParameter("active", vale.active));
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

        public TransactionResult updateStatus(Vale vale)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateValeStatus", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id ", vale.id));
                    command.Parameters.Add(new SqlParameter("active", vale.active));
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


        public TransactionResult deleteRegistroDetalle(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_deleteRegistroDetalle", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("idDetalleVale", id));
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

        public TransactionResult deleteDetalleVale(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_deleteDetalleVale", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("idVale", id));
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
    }
}