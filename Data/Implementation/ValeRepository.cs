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
                    SqlCommand command = new SqlCommand();

                    if (vale.userAutorizo.id != 0)
                    {
                        command = new SqlCommand("sp_createValeFromHH", connection);
                    }
                    else
                    {
                        command = new SqlCommand("sp_createVale", connection);
                    }
                        
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("turno", vale.turno));
                    command.Parameters.Add(new SqlParameter("compania_id", vale.compania.id));
                    command.Parameters.Add(new SqlParameter("polvorero_id", vale.polvorero.id));
                    command.Parameters.Add(new SqlParameter("cargador1_id", vale.cargador1.id));
                    command.Parameters.Add(new SqlParameter("cargador2_id", vale.cargador2.id));
                    command.Parameters.Add(new SqlParameter("user_id", vale.user.id));
                    command.Parameters.Add(new SqlParameter("subnivel_id", vale.subnivel.id));
                    command.Parameters.Add(new SqlParameter("fuente", vale.fuente));
                    command.Parameters.Add(new SqlParameter("folio_fisico", vale.folio_fisico));
                    command.Parameters.Add(new SqlParameter("observaciones", vale.observaciones));

                    if (vale.userAutorizo.id != 0)
                    {
                        command.Parameters.Add(new SqlParameter("userAutorizo", vale.userAutorizo.id));
                        command.Parameters.Add(new SqlParameter("active", vale.active));
                    }

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

        public int createDetalle(DetalleVale detalle)
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

                    if (!changePro)
                    {
                        command.Parameters.Add(new SqlParameter("folioCaja", registro.folioCaja));
                    }
                    
                    command.Parameters.Add(new SqlParameter("user_id", registro.user.id));
                    command.Parameters.Add(new SqlParameter("producto_id", registro.producto.id));
                    command.Parameters.Add(new SqlParameter("vale_id", registro.vale.id));
                    command.Parameters.Add(new SqlParameter("turno", registro.turno));
                    command.Parameters.Add(new SqlParameter("status", 1));
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
                    command.Parameters.Add(new SqlParameter("codigoCaja", registro.folioCaja));
                    command.Parameters.Add(new SqlParameter("user_id", registro.user.id));
                    command.Parameters.Add(new SqlParameter("producto_id", registro.producto.id));
                    command.Parameters.Add(new SqlParameter("vale_id", registro.vale.id));
                    command.Parameters.Add(new SqlParameter("turno", registro.turno));
                    command.Parameters.Add(new SqlParameter("status", 1));
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

                    bool authEmpty = true;

                    //Revisa si la autorización está vacía
                    string aux2 = row[27].ToString();

                    if(aux2 == String.Empty || aux2 == null || int.Parse(row[27].ToString()) == 0)
                    {
                        authEmpty = false;
                    }

                    if (authEmpty)
                    {
                        return new Vale
                        {
                            id = int.Parse(row[0].ToString()),
                            turno = int.Parse(row[1].ToString()),
                            user = new User
                            {
                                id = int.Parse(row[2].ToString()),
                                first_name = row[14].ToString(),
                                second_name = row[15].ToString()
                            },
                            timestamp = Convert.ToDateTime(row[3].ToString()),
                            updated = Convert.ToDateTime(row[4].ToString()),
                            compania = new Compania
                            {
                                id = int.Parse(row[6].ToString()),
                                nombre_sistema = row[7].ToString()
                            },
                            polvorero = new Empleado
                            {
                                id = int.Parse(row[8].ToString()),
                                nombre = row[9].ToString(),
                                ap_paterno = row[10].ToString(),
                                ap_materno = row[11].ToString()
                            },
                            cargador1 = new Empleado
                            {
                                id = int.Parse(row[12].ToString()),
                                nombre = row[32].ToString(),
                                ap_paterno = row[33].ToString()
                            },
                            cargador2 = new Empleado
                            {
                                id = int.Parse(row[13].ToString()),
                                nombre = row[36].ToString(),
                                ap_paterno = row[37].ToString()
                            },
                            subnivel = new SubNivel
                            {
                                id = int.Parse(row[5].ToString()),
                                nombre = row[17].ToString(),
                                nivel = new Nivel
                                {
                                    id = int.Parse(row[18].ToString()),
                                    nombre = row[19].ToString(),
                                    codigo = row[20].ToString()
                                },
                                cuenta = new Cuenta
                                {
                                    id = int.Parse(row[23].ToString()),
                                    numero = row[24].ToString()
                                }
                            },
                            observaciones = row[25].ToString(),
                            active = int.Parse(row[16].ToString()),
                            fuente = int.Parse(row[21].ToString()),
                            folio_fisico = row[22].ToString(),
                            userAutorizo = new User
                            {
                                id = int.Parse(row[27].ToString()),
                                first_name = row[28].ToString(),
                                second_name = row[29].ToString()
                            }
                        };
                    }
                    else
                    {

                        return new Vale
                        {
                            id = int.Parse(row[0].ToString()),
                            turno = int.Parse(row[1].ToString()),
                            user = new User
                            {
                                id = int.Parse(row[2].ToString()),
                                first_name = row[14].ToString(),
                                second_name = row[15].ToString()
                            },
                            timestamp = Convert.ToDateTime(row[3].ToString()),
                            updated = Convert.ToDateTime(row[4].ToString()),
                            compania = new Compania
                            {
                                id = int.Parse(row[6].ToString()),
                                nombre_sistema = row[7].ToString()
                            },
                            polvorero = new Empleado
                            {
                                id = int.Parse(row[8].ToString()),
                                nombre = row[9].ToString(),
                                ap_paterno = row[10].ToString(),
                                ap_materno = row[11].ToString()
                            },
                            cargador1 = new Empleado
                            {
                                id = int.Parse(row[12].ToString()),
                                nombre = row[32].ToString(),
                                ap_paterno = row[33].ToString()
                            },
                            cargador2 = new Empleado
                            {
                                id = int.Parse(row[13].ToString()),
                                nombre = row[36].ToString(),
                                ap_paterno = row[37].ToString()
                            },
                            subnivel = new SubNivel
                            {
                                id = int.Parse(row[5].ToString()),
                                nombre = row[17].ToString(),
                                nivel = new Nivel
                                {
                                    id = int.Parse(row[18].ToString()),
                                    nombre = row[19].ToString(),
                                    codigo = row[20].ToString()
                                },
                                cuenta = new Cuenta
                                {
                                    id = int.Parse(row[23].ToString()),
                                    numero = row[24].ToString()
                                }
                            },
                            observaciones = row[25].ToString(),
                            active = int.Parse(row[16].ToString()),
                            fuente = int.Parse(row[21].ToString()),
                            folio_fisico = row[22].ToString(),
                            userAutorizo = new User()
                            
                        };

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
                        bool authEmpty = true;

                        //Revisa si la autorización está vacía
                        string aux2 = row[22].ToString();

                        if (aux2 == String.Empty || aux2 == null)
                        {
                            authEmpty = false;
                        }

                        if (authEmpty)
                        {
                            objects.Add(new Vale
                            {
                                id = int.Parse(row[0].ToString()),
                                turno = int.Parse(row[1].ToString()),
                                user = new User { id = int.Parse(row[2].ToString()) },
                                timestamp = Convert.ToDateTime(row[3].ToString()),
                                updated = Convert.ToDateTime(row[4].ToString()),
                                subnivel = new SubNivel
                                {
                                    id = int.Parse(row[5].ToString()),
                                    nombre = row[15].ToString(),
                                    nivel = new Nivel
                                    {
                                        id = int.Parse(row[16].ToString()),
                                        nombre = row[17].ToString(),
                                        codigo = row[18].ToString()
                                    },
                                },
                                compania = new Compania
                                {
                                    id = int.Parse(row[6].ToString()),
                                    nombre_sistema = row[7].ToString()
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
                                active = int.Parse(row[14].ToString()),
                                fuente = int.Parse(row[19].ToString()),
                                folio_fisico = row[20].ToString(),
                                userAutorizo = new User
                                {
                                    id = int.Parse(row[22].ToString()),
                                    first_name = row[23].ToString(),
                                    second_name = row[24].ToString()
                                }
                            });
                        }
                        else
                        {
                            objects.Add(new Vale
                            {
                                id = int.Parse(row[0].ToString()),
                                turno = int.Parse(row[1].ToString()),
                                user = new User { id = int.Parse(row[2].ToString()) },
                                timestamp = Convert.ToDateTime(row[3].ToString()),
                                updated = Convert.ToDateTime(row[4].ToString()),
                                subnivel = new SubNivel
                                {
                                    id = int.Parse(row[5].ToString()),
                                    nombre = row[15].ToString(),
                                    nivel = new Nivel
                                    {
                                        id = int.Parse(row[16].ToString()),
                                        nombre = row[17].ToString(),
                                        codigo = row[18].ToString()
                                    },
                                },
                                compania = new Compania
                                {
                                    id = int.Parse(row[6].ToString()),
                                    nombre_sistema = row[7].ToString()
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
                                active = int.Parse(row[14].ToString()),
                                fuente = int.Parse(row[19].ToString()),
                                folio_fisico = row[20].ToString(),
                                userAutorizo = new User()
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
                                nombre = row[2].ToString(),
                                codigo = row[7].ToString(),
                                segmento = new SegmentoProducto
                                {
                                    name = row[5].ToString()
                                },
                                tipo_producto = new TipoProducto
                                {
                                    value = int.Parse(row[6].ToString())
                                }
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
                            folioCaja = row[2].ToString(),
                            turno = int.Parse(row[3].ToString()),
                            detallevale = new DetalleVale { id = int.Parse(row[4].ToString()) },
                            user = new User { id = int.Parse(row[5].ToString()) },
                            vale = new Vale { id = int.Parse(row[8].ToString()) },
                            producto = new Producto { id = int.Parse(row[7].ToString()) },
                            timestamp = Convert.ToDateTime(row[9].ToString()),
                            updated = Convert.ToDateTime(row[10].ToString()),

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
                            folioCaja = row[2].ToString(),
                            turno = int.Parse(row[3].ToString()),
                            detallevale = new DetalleVale { id = int.Parse(row[4].ToString()) },
                            user = new User { id = int.Parse(row[5].ToString()) },
                            vale = new Vale { id = int.Parse(row[8].ToString()) },
                            producto = new Producto { id = int.Parse(row[7].ToString()) },
                            timestamp = Convert.ToDateTime(row[9].ToString()),
                            updated = Convert.ToDateTime(row[10].ToString())
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
                            folioCaja = row[2].ToString(),
                            turno = int.Parse(row[3].ToString()),
                            detallevale = new DetalleVale { id = int.Parse(row[4].ToString()) },
                            user = new User { id = int.Parse(row[5].ToString()) },
                            vale = new Vale { id = int.Parse(row[8].ToString()) },
                            producto = new Producto { id = int.Parse(row[7].ToString()) },
                            timestamp = Convert.ToDateTime(row[9].ToString()),
                            updated = Convert.ToDateTime(row[10].ToString())
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
                            folioCaja = row[2].ToString(),
                            turno = int.Parse(row[3].ToString()),
                            detallevale = new DetalleVale { id = int.Parse(row[4].ToString()) },
                            user = new User { id = int.Parse(row[5].ToString()) },
                            vale = new Vale { id = int.Parse(row[8].ToString()) },
                            producto = new Producto { id = int.Parse(row[7].ToString()) },
                            timestamp = Convert.ToDateTime(row[9].ToString()),
                            updated = Convert.ToDateTime(row[10].ToString())
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
                            turno = int.Parse(row[3].ToString()),
                            detallevale = new DetalleVale { id = int.Parse(row[4].ToString()) },
                            user = new User { id = int.Parse(row[5].ToString()) },
                            vale = new Vale { id = int.Parse(row[8].ToString()) },
                            producto = new Producto { id = int.Parse(row[7].ToString()) },
                            timestamp = Convert.ToDateTime(row[9].ToString()),
                            updated = Convert.ToDateTime(row[10].ToString())
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
        public IList<RegistroDetalle> getAllRegistersHistorico()
        {
            SqlConnection connection = null;
            IList<RegistroDetalle> objects = new List<RegistroDetalle>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllRegistroHistorico", connection);
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
                            folioCaja = row[2].ToString(),
                            turno = int.Parse(row[3].ToString()),
                            detallevale = new DetalleVale { id = int.Parse(row[4].ToString()) },
                            user = new User { id = int.Parse(row[5].ToString()) },
                            status = int.Parse(row[6].ToString()),
                            vale =  new Vale { id = int.Parse(row[8].ToString()) },
                            producto = new Producto { id = int.Parse(row[7].ToString()) },
                            timestamp = Convert.ToDateTime(row[9].ToString()),
                            updated = Convert.ToDateTime(row[10].ToString())
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
        public IList<RegistroDetalle> getAllRegistersHistoricoOver()
        {
            SqlConnection connection = null;
            IList<RegistroDetalle> objects = new List<RegistroDetalle>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllRegistroOverHistorico", connection);
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
                            turno = int.Parse(row[3].ToString()),
                            detallevale = new DetalleVale { id = int.Parse(row[4].ToString()) },
                            user = new User { id = int.Parse(row[5].ToString()) },
                            vale = new Vale { id = int.Parse(row[8].ToString()) },
                            producto = new Producto { id = int.Parse(row[7].ToString()) },
                            timestamp = Convert.ToDateTime(row[9].ToString()),
                            updated = Convert.ToDateTime(row[10].ToString())
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
                    command.Parameters.Add(new SqlParameter("turno",vale.turno));
                    command.Parameters.Add(new SqlParameter("polvorero_id", vale.polvorero.id));
                    command.Parameters.Add(new SqlParameter("cargador1_id", vale.cargador1.id));
                    command.Parameters.Add(new SqlParameter("cargador2_id", vale.cargador2.id));
                    command.Parameters.Add(new SqlParameter("subnivel_id", vale.subnivel.id));
                    command.Parameters.Add(new SqlParameter("active", vale.active));
                    command.Parameters.Add(new SqlParameter("folio_fisico", vale.folio_fisico));
                    command.Parameters.Add(new SqlParameter("observaciones", vale.observaciones));
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

        public TransactionResult updateAutorizacion(Vale vale)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateValeAutorización", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id ", vale.id));
                    command.Parameters.Add(new SqlParameter("user_id", vale.userAutorizo.id));
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

        public User validarLoginTablet(User user)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getUserTablet", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("username ", user.username));
                    command.Parameters.Add(new SqlParameter("pass ", user.password));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];

                    bool userEmpty = true;

                    //Revisa si la autorización está vacía
                    string aux2 = row[0].ToString();

                    if (aux2 == String.Empty || aux2 == null)
                    {
                        userEmpty = false;
                        return null;
                    }

                    if (userEmpty)
                    {
                        if (int.Parse(row[5].ToString()) == 3)
                        {
                            return new User
                            {
                                id = int.Parse(row[0].ToString()),
                                first_name = row[3].ToString(),
                                second_name = row[4].ToString()
                            };
                        }else
                        {
                            return null;
                        }
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


        public TransactionResult cerrarVale(Vale vale)
        {
            try
            {
                vale.detalles = getAllDetalles(vale.id);

                foreach (DetalleVale dv in vale.detalles)
                {
                    dv.registros = new List<RegistroDetalle>();
                    if (dv.producto.tipo_producto.value > 2)
                    {
                        dv.registros.Add(new RegistroDetalle
                        {
                            folio = dv.cantidad.ToString(),
                            detallevale = new DetalleVale { id = dv.id },
                            producto = new Producto { id = dv.producto.id },
                            vale = new Vale { id = vale.id },
                            turno = vale.turno,
                            user = new User { id = vale.user.id },
                            folioCaja = "N/C"
                        });
                    }
                    else if (dv.producto.tipo_producto.value == 1)
                    {
                        for (int i = 0; i < dv.cantidad; i++)
                        {
                            dv.registros.Add(new RegistroDetalle
                            {
                                folio = "S/F",
                                detallevale = new DetalleVale { id = dv.id },
                                producto = new Producto { id = dv.producto.id },
                                vale = new Vale { id = vale.id },
                                turno = vale.turno,
                                user = new User { id = vale.user.id },
                                folioCaja = "S/C",

                            });
                        }
                    }
                    else if (dv.producto.tipo_producto.value == 2)
                    {
                        for (int i = 0; i < dv.cantidad; i++)
                        {
                            dv.registros.Add(new RegistroDetalle
                            {
                                folio = "S/F",
                                detallevale = new DetalleVale { id = dv.id },
                                producto = new Producto { id = dv.producto.id },
                                vale = new Vale { id = vale.id },
                                turno = vale.turno,
                                user = new User { id = vale.user.id },
                                folioCaja = "N/A"
                            });
                        }
                    }

                    foreach (RegistroDetalle r in dv.registros)
                    {
                        createRegistroDetalle(r);
                    }
                }

                vale.active = 0;
                return cerrarValeStatus(vale);
            }
            catch(Exception ex)
            {
                return TransactionResult.ERROR;
            }
        }

        public TransactionResult cerrarValeStatus(Vale vale)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateCerrarValeStatus", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id ", vale.id));
                    command.Parameters.Add(new SqlParameter("active", vale.active));
                    command.Parameters.Add(new SqlParameter("usuario_id", vale.user.id));
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

        //Obtener todos los registros de salidas de un vale
        public IList<RegistroDetalle> getAllRegistersNoEscaneableByVale(int vale_id)
        {
            SqlConnection connection = null;
            IList<RegistroDetalle> objects = new List<RegistroDetalle>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllRegistroDetalleNoEscaneableByValeId", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("valeid", vale_id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new RegistroDetalle
                        {
                            id = int.Parse(row[0].ToString()),
                            folio = row[1].ToString(),
                            folioCaja = row[2].ToString(),
                            turno = int.Parse(row[3].ToString()),
                            detallevale = new DetalleVale { id = int.Parse(row[4].ToString()) },
                            user = new User { id = int.Parse(row[5].ToString()) },
                            producto = new Producto {
                                id = int.Parse(row[6].ToString()),
                                nombre = row[7].ToString(),
                                codigo = row[8].ToString(),
                                tipo_producto = new TipoProducto { value = int.Parse(row[9].ToString()) }
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
    }
}