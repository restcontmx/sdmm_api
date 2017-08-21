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
    public class DevolucionRepository : IDevolucionRepository
    {

        public int createP(Devolucion devolucion)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand("sp_createDevolucion", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("compania_id", devolucion.compania.id));
                    command.Parameters.Add(new SqlParameter("motivo", Validations.defaultString(devolucion.motivo)));
                    command.Parameters.Add(new SqlParameter("turno", devolucion.turno));
                    command.Parameters.Add(new SqlParameter("user_id", devolucion.user.id));
                    command.Parameters.Add(new SqlParameter("vale_id", devolucion.vale.id));
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


        //Información acerca de la caja a devolver
        public DetalleDevByCaja getDetalleByCaja(string folio)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getDetailsByFolioCaja", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("folioC", folio));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new DetalleDevByCaja
                    {
                        nombreP = row[0].ToString(),
                        empresa = row[1].ToString(),
                        vale = new Vale { id =  int.Parse(row[2].ToString()) }
                        
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

        public IList<RegistroDetalleDev> detail(int id)
        {
            SqlConnection connection = null;
            IList<RegistroDetalleDev> objects = new List<RegistroDetalleDev>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllRegistroDetalleDevByIdDevolucion", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("idDev", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new RegistroDetalleDev
                        {
                            id = int.Parse(row[0].ToString()),
                            folio = row[1].ToString(),
                            tipodev = int.Parse(row[3].ToString()),
                            observaciones = row[4].ToString(),
                            producto = new Producto
                            {
                                nombre = row[6].ToString()
                            },
                            devolucion = new Devolucion
                            {
                                id = int.Parse(row[5].ToString()),
                                compania = new Compania
                                {
                                    nombre_sistema = row[7].ToString()
                                    
                                },
                                timestamp = Convert.ToDateTime(row[8].ToString())
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

        

        public IList<Devolucion> getAll()
        {
            SqlConnection connection = null;
            IList<Devolucion> objects = new List<Devolucion>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllDevolucion", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Devolucion
                        {
                            id = int.Parse(row[0].ToString()),
                            compania = new Compania
                            {
                                id = int.Parse(row[1].ToString()),
                                nombre_sistema = row[6].ToString()
                            },
                            motivo = row[2].ToString(),
                            turno = int.Parse(row[3].ToString()),
                            vale = new Vale
                            {
                                id = int.Parse(row[4].ToString())
                            },
                            user = new User
                            {
                                first_name = row[7].ToString(),
                                second_name = row[8].ToString()
                            },
                            timestamp = Convert.ToDateTime(row[5].ToString())
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


        //Regresa todo los registros que existen por el folio de la caja
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

                    SqlCommand command2 = new SqlCommand("sp_getAllRegistroDetalleOverByFolioCaja", connection);
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

        //Regresa todos los detalles de un vale por su ID
        private IList<DetalleVale> getAllDetalles(int vale_id)
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
                            producto = new Producto
                            {
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


        //Crear registro de la devolucion
        public TransactionResult createRegistroDetalleDev(RegistroDetalleDev registro)
        {

            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    SqlCommand command;
                    connection.Open();

                    if (registro.observaciones == null || registro.observaciones == string.Empty)
                    {
                        registro.observaciones = "";
                    }

                    command = new SqlCommand("sp_createRegistroDetalleDev", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("devolucion_id", registro.devolucion.id));
                    command.Parameters.Add(new SqlParameter("tipodev", registro.tipodev));
                    command.Parameters.Add(new SqlParameter("observaciones", registro.observaciones));
                    command.Parameters.Add(new SqlParameter("user_id", registro.user.id));
                    command.Parameters.Add(new SqlParameter("folio", registro.folio));
                    command.Parameters.Add(new SqlParameter("producto_id", registro.producto.id));
                    command.ExecuteNonQuery();

                    //Devolución de una caja completa
                    if (registro.tipodev == 2)
                    {
                        IList<RegistroDetalle> objects = new List<RegistroDetalle>();

                        objects = getAllRegistersByFolioCaja(registro.folio);

                        foreach (RegistroDetalle r in objects)
                        {

                            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
                            {
                                try
                                {

                                    //Función recursiva
                                    RegistroDetalleDev reg = new RegistroDetalleDev
                                    {
                                        folio = r.folio,
                                        devolucion = registro.devolucion,
                                        tipodev = 2,
                                        observaciones = "Este producto fue regresado en la devolución completa de la caja con folio: " + registro.folio,
                                        user = registro.user
                                    };

                                    createRegistroDetalleDev(reg);

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

                        return TransactionResult.CREATED;

                }


                        // Esto es para cuando es la devolución de un vale
                        if (registro.tipodev == 3)
                    {
                        IList<DetalleVale> detalles = getAllDetalles(int.Parse(registro.folio));

                        IList<RegistroDetalle> objects = new List<RegistroDetalle>();

                        //Obtiene todos los registros de los detalles del vale
                        foreach (DetalleVale d in detalles)
                        {
                            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
                            {
                                try
                                {
                                    connection.Open();
                                    SqlCommand command2 = new SqlCommand("sp_getAllRegistroDetalleByDetalleValeId", connection);
                                    command2.CommandType = CommandType.StoredProcedure;
                                    command2.Parameters.Add(new SqlParameter("detallevale_id", d.id));
                                    SqlDataAdapter data_adapter = new SqlDataAdapter(command2);
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

                                }
                                catch (SqlException ex)
                                {
                                    if (connection != null)
                                    {
                                        connection.Close();
                                    }
                                }
                            }
                        }

                        //Obtiene todos los registros Over de los detalles
                        foreach (DetalleVale d in detalles)
                        {
                            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
                            {
                                try
                                {
                                    connection.Open();
                                    SqlCommand command3 = new SqlCommand("sp_getAllRegistroDetalleOverByDetalleValeId", connection);
                                    command3.CommandType = CommandType.StoredProcedure;
                                    command3.Parameters.Add(new SqlParameter("detallevale_id", d.id));
                                    SqlDataAdapter data_adapter = new SqlDataAdapter(command3);
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

                                }
                                catch (SqlException ex)
                                {
                                    if (connection != null)
                                    {
                                        connection.Close();
                                    }
                                }
                            }
                        }

                        foreach (RegistroDetalle r in objects)
                        {

                            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
                            {
                                try
                                {

                                    //Función recursiva
                                    RegistroDetalleDev reg = new RegistroDetalleDev
                                    {
                                        folio = r.folio,
                                        devolucion = registro.devolucion,
                                        tipodev = 2,
                                        observaciones = "Este producto fue regresado en la devolución completa del vale con folio: " + registro.folio,
                                        user = registro.user
                                    };

                                    createRegistroDetalleDev(reg);

                                    /*connection.Open();
                                    SqlCommand command4 = new SqlCommand("sp_createRegistroDetalleDev", connection);
                                    command4.CommandType = CommandType.StoredProcedure;
                                    command4.Parameters.Add(new SqlParameter("devolucion_id", registro.devolucion.id));
                                    command4.Parameters.Add(new SqlParameter("tipodev", 2));
                                    command4.Parameters.Add(new SqlParameter("observaciones", "Este producto fue regresado en la devolución completa del vale con folio: " + registro.folio));
                                    command4.Parameters.Add(new SqlParameter("user_id", registro.user.id));
                                    command4.Parameters.Add(new SqlParameter("folio", r.folio));
                                    command4.ExecuteNonQuery();*/

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

                        return TransactionResult.CREATED;
                    }

                    //Termina la devolución completa del vale



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

    }
}