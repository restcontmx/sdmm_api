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
                        vale = new Vale { id = int.Parse(row[2].ToString()) }

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
                                id = int.Parse(row[10].ToString()),
                                nombre = row[6].ToString(),
                                segmento = new SegmentoProducto { name = row[9].ToString() },
                                tipo_producto = new TipoProducto { value = int.Parse(row[11].ToString()) }
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


        public Devolucion detailComprobante(int id)
        {
            SqlConnection connection = null;
            IList<RegistroDetalleDev> objects = new List<RegistroDetalleDev>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_devolucionDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];

                    Devolucion dev = new Devolucion
                    {
                        id = int.Parse(row[0].ToString()),
                        motivo = row[1].ToString(),
                        turno = int.Parse(row[2].ToString()),
                        user = new User { id = int.Parse(row[3].ToString()) },
                        vale = new Vale { id = int.Parse(row[4].ToString()) },
                        timestamp = Convert.ToDateTime(row[5].ToString())
                    };

                    dev.vale = detailVale(dev.vale.id);
                    dev.registros = detail(dev.id);
                    dev.detalles = new List<DetalleVale>();

                    IList<int> listProductos = new List<int>();
                    int contP = 0;

                    foreach (RegistroDetalleDev reg in dev.registros)
                    {
                        if (reg.tipodev == 1)
                        {
                            if (!listProductos.Contains(reg.producto.id))
                            {
                                foreach (RegistroDetalleDev r in dev.registros)
                                {
                                    if (r.producto.id == reg.producto.id)
                                    {
                                        if (r.tipodev == 2)
                                        {
                                            Caja cajaAux = detailCaja(r.folio);

                                            contP = contP + ((int.Parse(cajaAux.folio_fin) - int.Parse(cajaAux.folio_ini)) + 1);
                                        }
                                        else
                                        {
                                            if (r.producto.tipo_producto.value != 1 && r.producto.tipo_producto.value != 2)
                                            {
                                                contP = contP + int.Parse(r.folio);
                                            }
                                            else
                                            {
                                                contP = contP + 1;
                                            }
                                        }
                                    }
                                }

                                dev.detalles.Add(new DetalleVale { producto = reg.producto, cantidad = contP });
                                listProductos.Add(reg.producto.id);
                                contP = 0;
                            }
                        }
                        else if (reg.tipodev == 2)
                        {
                            if (!listProductos.Contains(reg.producto.id))
                            {
                                foreach (RegistroDetalleDev r in dev.registros)
                                {
                                    if (r.producto.id == reg.producto.id)
                                    {
                                        if (r.tipodev == 2)
                                        {
                                            Caja cajaAux = detailCaja(r.folio);

                                            contP = contP + ((int.Parse(cajaAux.folio_fin) - int.Parse(cajaAux.folio_ini)) + 1);
                                        }
                                        else
                                        {
                                            if (r.producto.tipo_producto.value != 1 && r.producto.tipo_producto.value != 2)
                                            {
                                                contP = contP + int.Parse(r.folio);
                                            }
                                            else
                                            {
                                                contP = contP + 1;
                                            }
                                        }
                                    }
                                }

                                dev.detalles.Add(new DetalleVale { producto = reg.producto, cantidad = contP });
                                listProductos.Add(reg.producto.id);
                                contP = 0;
                            }

                            dev.vale = detailVale(dev.vale.id);
                        }
                        else if (reg.tipodev == 3)
                        {
                            dev.detalles = getAllDetalles(dev.vale.id);
                        }
                    }

                    return dev;

                }
                catch (SqlException ex)
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return null;
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
        public IList<RegistroDetalle> getAllRegistersByValeId(string folioCaja)
        {
            SqlConnection connection = null;
            IList<RegistroDetalle> objects = new List<RegistroDetalle>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllRegistroDetalleByValeId", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("valeid", int.Parse(folioCaja)));
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
                            detallevale = new DetalleVale { id = int.Parse(row[4].ToString()) },
                            user = new User { id = int.Parse(row[5].ToString()) },
                            timestamp = Convert.ToDateTime(row[9].ToString()),
                            updated = Convert.ToDateTime(row[10].ToString())
                        };

                        objects.Add(rgd);

                    }

                    return objects;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
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
            
            bool isVale = false;
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    connection.Open();

                    if (registro.observaciones == null || registro.observaciones == string.Empty)
                    {
                        registro.observaciones = "";
                    }

                    //command = new SqlCommand("sp_createRegistroDetalleDev", connection);
                    //command = new SqlCommand("sp_createRegistroDetalleDev", connection);

                    if (registro.tipodev == 1)
                    {
                        command = new SqlCommand("sp_createRegistroDetalleDevProducto", connection);
                    }
                    else if (registro.tipodev == 2)
                    {
                        command = new SqlCommand("sp_createRegistroDetalleDevCaja", connection);
                    }
                    else if (registro.tipodev == 3)
                    {
                        command = new SqlCommand("sp_createRegistroDetalleDevVale", connection);
                        isVale = true;
                    }

                    if (!isVale)
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("devolucion_id", registro.devolucion.id));
                        command.Parameters.Add(new SqlParameter("tipodev", registro.tipodev));
                        command.Parameters.Add(new SqlParameter("observaciones", registro.observaciones));
                        command.Parameters.Add(new SqlParameter("user_id", registro.user.id));
                        command.Parameters.Add(new SqlParameter("folio", registro.folio));
                        command.Parameters.Add(new SqlParameter("producto_id", registro.producto.id));
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("devolucion_id", registro.devolucion.id));
                        command.Parameters.Add(new SqlParameter("observaciones", registro.observaciones));
                        command.Parameters.Add(new SqlParameter("user_id", registro.user.id));
                        command.Parameters.Add(new SqlParameter("valeid", registro.folio));
                        command.ExecuteNonQuery();
                        connection.Close();

                        IList<RegistroDetalle> objects = new List<RegistroDetalle>();

                        objects = getAllRegistersByValeId(registro.folio);

                        IList<string> codigosCajas = new List<string>();

                        SqlCommand command2 = new SqlCommand();

                        foreach (RegistroDetalle r in objects)
                        {
                            if (!codigosCajas.Contains(r.folioCaja))
                            {
                                if (r.folioCaja == "N/A")
                                {
                                    connection.Open();
                                    command2 = new SqlCommand("sp_activarBulto", connection);
                                    command2.CommandType = CommandType.StoredProcedure;
                                    command2.Parameters.Add(new SqlParameter("folioBulto", r.folio));
                                    command2.ExecuteNonQuery();
                                    connection.Close();
                                }
                                else
                                {
                                    connection.Open();
                                    command2 = new SqlCommand("sp_devCantidadCaja", connection);
                                    command2.CommandType = CommandType.StoredProcedure;

                                    IList<RegistroDetalle> registersLocal = new List<RegistroDetalle>();

                                    foreach (RegistroDetalle reg in objects)
                                    {
                                        if (reg.folioCaja == r.folioCaja)
                                        {
                                            registersLocal.Add(reg);
                                        }
                                    }

                                    command2.Parameters.Add(new SqlParameter("folioCaja", r.folioCaja));
                                    command2.Parameters.Add(new SqlParameter("cantidad", registersLocal.Count));
                                    command2.ExecuteNonQuery();
                                    connection.Close();

                                    codigosCajas.Add(r.folioCaja);

                                }
                            }
                        }

                        connection.Open();
                        command2 = new SqlCommand("sp_devCantidadProductoVale", connection);
                        command2.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.Add(new SqlParameter("valeid", registro.folio));
                        command2.ExecuteNonQuery();
                        connection.Close();
                    }
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
            }
        }

        /// <summary>
        /// Retrieve object from the db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <summary>
        /// Retrieve object from the db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Caja detailCaja(string codigo)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_cajaDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("codigo", codigo));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new Caja
                    {
                        id = int.Parse(row[0].ToString()),
                        codigo = row[1].ToString(),
                        folio_ini = row[2].ToString(),
                        folio_fin = row[3].ToString(),
                        cantidad = int.Parse(row[4].ToString()),
                        producto = new Producto { id = int.Parse(row[5].ToString()) }
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

        public Vale detailVale(int id)
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

                    if (aux2 == String.Empty || aux2 == null || int.Parse(row[27].ToString()) == 0)
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
                            producto = new Producto
                            {
                                id = int.Parse(row[1].ToString()),
                                nombre = row[2].ToString(),
                                segmento = new SegmentoProducto
                                {
                                    name = row[5].ToString()
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

    }
}