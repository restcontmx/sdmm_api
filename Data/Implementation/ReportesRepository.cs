using Data.Interface;
using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using Models.Auth;

namespace Data.Implementation
{
    public class ReportesRepository : IReportesRepository
    {
        public DevolucionRepository devRepo = new DevolucionRepository();
        public ValeRepository valeRepo = new ValeRepository();

        public IList<RegistroDetalle> getListaSedena(ReportesVo reportes_vo) {

            DateTime rangeStart = DateTime.Parse(reportes_vo.rangeStart);
            DateTime rangeEnd = DateTime.Parse(reportes_vo.rangeEnd);

            SqlConnection connection = null;
            IList<RegistroDetalle> objects = new List<RegistroDetalle>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_reporteSedenaDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("rangeStart", rangeStart));
                    command.Parameters.Add(new SqlParameter("rangeEnd", rangeEnd));
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
                            detallevale = new DetalleVale
                            {
                                id = int.Parse(row[4].ToString()),
                            },
                                producto = new Producto
                            {
                                id = int.Parse(row[6].ToString()),
                                nombre = row[9].ToString(),
                                segmento = new SegmentoProducto
                                {
                                    id = int.Parse(row[10].ToString()),
                                },
                                    proveedor = new Proveedor
                                {
                                    razon_social = row[11].ToString(),
                                    permiso_sedena = row[12].ToString()
                                    
                                },
                            },
                            vale = new Vale
                            {
                                id = int.Parse(row[7].ToString()),
                            },
                            timestamp = Convert.ToDateTime(row[8].ToString())

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

        public IList<ReporteAccPac> getListVale(ReportesVo reportes_vo)
        {

            DateTime rangeStart = DateTime.Parse(reportes_vo.rangeStart).AddHours(6.25);
            DateTime rangeEnd = DateTime.Parse(reportes_vo.rangeEnd).AddHours(23.99);


            SqlConnection connection = null;
            IList<ReporteAccPac> objects = new List<ReporteAccPac>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_reporteAccPacDetailvale", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("rangeStart", rangeStart));
                    command.Parameters.Add(new SqlParameter("rangeEnd", rangeEnd));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        DateTime rangeEndAux = rangeEnd;

                        //Verificar si el vale llevará la cuenta de la compañía
                        Cuenta cuentaAux = new Cuenta();

                        int cuentaPropia = int.Parse(row[8].ToString());

                        if (cuentaPropia != 0)
                        {
                            cuentaAux.numero = row[12].ToString();
                            cuentaAux.num_categoria = row[13].ToString();
                        }
                        else
                        {
                            cuentaAux.numero = row[4].ToString();
                            cuentaAux.num_categoria = row[5].ToString();
                        }
                        //---------------------------------------------------

                        Vale valeAux = new Vale
                        {
                            id = int.Parse(row[0].ToString()),
                            turno = int.Parse(row[1].ToString()),
                            updated = Convert.ToDateTime(row[2].ToString()),
                            subnivel = new SubNivel

                            {
                                nombre = row[3].ToString(),
                                proceso = new ProcesoMinero
                                {
                                    nombre = row[6].ToString()
                                },
                                /*cuenta = new Cuenta
                                {
                                    numero = row[4].ToString(),
                                    num_categoria = row[5].ToString()
                                }*/
                                cuenta = cuentaAux
                            },
                            compania = new Compania
                            {

                                razon_social = row[7].ToString()

                            }
                        };


                        if (valeAux.updated > rangeEndAux.AddDays(1.0))
                        {
                            rangeEndAux = rangeEnd.AddDays(1.0).AddHours(6.25);
                            if (valeAux.updated < rangeEndAux)
                            {
                                objects.Add(new ReporteAccPac { vale = valeAux });
                            }
                        }
                        else
                        {
                            objects.Add(new ReporteAccPac { vale = valeAux });
                        }

                        /*
                        objects.Add(new ReporteAccPac
                        {
                            vale = new Vale
                            {
                                id = int.Parse(row[0].ToString()),
                                turno = int.Parse(row[1].ToString()),
                                updated = Convert.ToDateTime(row[2].ToString()),
                                subnivel = new SubNivel

                                {
                                    nombre = row[3].ToString(),
                                    categoria = new Categoria
                                    {
                                        numero = row[4].ToString(),
                                        procesominero = new ProcesoMinero
                                        {
                                            nombre = row[5].ToString()
                                        }
                                    },
                                    cuenta = new Cuenta
                                    {
                                        numero = row[6].ToString()
                                    }
                                },
                                compania = new Compania
                                {

                                    razon_social = row[7].ToString()

                                }
                            }
                        });*/
                    }

                    connection.Close();


                    foreach (ReporteAccPac reporte in objects)
                    {

                        IList<int> idsp = new List<int>();
                        IList<string> codigos = new List<string>();
                        int cantidad = 0;

                        SqlCommand command2 = new SqlCommand("sp_reporteAccPacDetail", connection);
                        command2.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.Add(new SqlParameter("id", reporte.vale.id));
                        SqlDataAdapter data_adapter2 = new SqlDataAdapter(command2);
                        DataSet data_set2 = new DataSet();
                        data_adapter2.Fill(data_set2);

                        bool productoActivo = false;
                        int cont = 0;
                        string codigoaux = "";

                        reporte.registros = new List<DetalleVale>();

                        foreach (DataRow row in data_set2.Tables[0].Rows)
                        {
                            cont = cont + 1;
                            int auxId = int.Parse(row[2].ToString());

                            if (idsp.Contains(int.Parse(row[2].ToString())))
                            {
                                if (cont == data_set2.Tables[0].Rows.Count)
                                {
                                    cantidad = cantidad + 1;

                                    reporte.registros.Add(new DetalleVale
                                    {
                                        producto = new Producto { id = idsp[idsp.Count - 1], codigo = row[4].ToString() },
                                        cantidad = cantidad
                                    });
                                }
                                else
                                {
                                    cantidad = cantidad + 1;
                                }
                            }
                            else
                            {
                                if (productoActivo)
                                {
                                    reporte.registros.Add(new DetalleVale
                                    {
                                        producto = new Producto { id = idsp[idsp.Count - 1], codigo = codigoaux },
                                        cantidad = cantidad
                                    });

                                    if (int.Parse(row[5].ToString()) <= 2)
                                    {
                                        idsp.Add(int.Parse(row[2].ToString()));
                                        cantidad = 0;
                                        cantidad = cantidad + 1;
                                        productoActivo = true;
                                    }
                                    else
                                    {
                                        if (codigos.Contains(row[4].ToString()))
                                        {
                                            cantidad = cantidad + int.Parse(row[1].ToString());
                                        }
                                        else
                                        {
                                            idsp.Add(int.Parse(row[2].ToString()));
                                            codigos.Add(row[4].ToString());
                                            cantidad = 0;
                                            productoActivo = false;

                                            cantidad = cantidad + int.Parse(row[1].ToString());

                                            /*reporte.registros.Add(new DetalleVale
                                            {
                                                producto = new Producto { id = idsp[idsp.Count - 1], codigo = row[4].ToString() },
                                                cantidad = cantidad
                                            });*/
                                        }
                                    }

                                }
                                else
                                {
                                    if (int.Parse(row[5].ToString()) <= 2)
                                    {
                                        idsp.Add(int.Parse(row[2].ToString()));
                                        cantidad = 0;
                                        cantidad = cantidad + 1;
                                        productoActivo = true;
                                    }
                                    else
                                    {
                                        if (codigos.Contains(row[4].ToString()))
                                        {
                                            if (cont == data_set2.Tables[0].Rows.Count)
                                            {
                                                cantidad = cantidad + int.Parse(row[1].ToString());

                                                reporte.registros.Add(new DetalleVale
                                                {
                                                    producto = new Producto { id = idsp[idsp.Count - 1], codigo = row[4].ToString() },
                                                    cantidad = cantidad
                                                });
                                            }
                                            else
                                            {
                                                cantidad = cantidad + int.Parse(row[1].ToString());
                                            }
                                        }
                                        else
                                        {
                                            if (cont == data_set2.Tables[0].Rows.Count)
                                            {

                                                reporte.registros.Add(new DetalleVale
                                                {
                                                    producto = new Producto { id = idsp[idsp.Count - 1], codigo = codigoaux },
                                                    cantidad = cantidad
                                                });

                                                idsp.Add(int.Parse(row[2].ToString()));
                                                cantidad = 0;
                                                cantidad = cantidad + int.Parse(row[1].ToString());

                                                reporte.registros.Add(new DetalleVale
                                                {
                                                    producto = new Producto { id = idsp[idsp.Count - 1], codigo = row[4].ToString() },
                                                    cantidad = cantidad
                                                });
                                            }
                                            else
                                            {
                                                idsp.Add(int.Parse(row[2].ToString()));
                                                codigos.Add(row[4].ToString());

                                                productoActivo = false;

                                                reporte.registros.Add(new DetalleVale
                                                {
                                                    producto = new Producto { id = idsp[idsp.Count - 1], codigo = codigoaux },
                                                    cantidad = cantidad
                                                });

                                                cantidad = 0;
                                                cantidad = cantidad + int.Parse(row[1].ToString());
                                            }


                                        }
                                    }
                                }

                            }

                            codigoaux = row[4].ToString();
                            if (cont == data_set2.Tables[0].Rows.Count)
                            {
                                reporte.registros.Add(new DetalleVale
                                {
                                    producto = new Producto { id = idsp[idsp.Count - 1], codigo = row[4].ToString() },
                                    cantidad = cantidad
                                });
                            }
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

        public IList<ReporteAccPac> getListValeFeb2018(ReportesVo reportes_vo)
        {

            DateTime rangeStart = DateTime.Parse(reportes_vo.rangeStart).AddHours(6.25);
            DateTime rangeEnd = DateTime.Parse(reportes_vo.rangeEnd).AddHours(23.99);

            int auxIdR = 0;
            SqlConnection connection = null;
            IList<ReporteAccPac> objects = new List<ReporteAccPac>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    //--------- Datos Generales del Vale ----------
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_reporteAccPacDetailvale", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("rangeStart", rangeStart));
                    command.Parameters.Add(new SqlParameter("rangeEnd", rangeEnd));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        DateTime rangeEndAux = rangeEnd;

                        //Verificar si el vale llevará la cuenta de la compañía
                        Cuenta cuentaAux = new Cuenta();

                        int cuentaPropia = int.Parse(row[8].ToString());

                        if (cuentaPropia != 0)
                        {
                            cuentaAux.numero = row[12].ToString();
                            cuentaAux.num_categoria = row[13].ToString();
                        }
                        else
                        {
                            cuentaAux.numero = row[4].ToString();
                            cuentaAux.num_categoria = row[5].ToString();
                        }
                        //---------------------------------------------------

                        Vale valeAux = new Vale
                        {
                            id = int.Parse(row[0].ToString()),
                            turno = int.Parse(row[1].ToString()),
                            updated = Convert.ToDateTime(row[2].ToString()),
                            subnivel = new SubNivel

                            {
                                nombre = row[3].ToString(),
                                proceso = new ProcesoMinero
                                {
                                    nombre = row[6].ToString()
                                },
                                cuenta = cuentaAux
                            },
                            compania = new Compania
                            {

                                razon_social = row[7].ToString()

                            }
                        };

                        valeAux.detalles = new List<DetalleVale>();
                        valeAux.detalles = valeRepo.getAllDetalles(valeAux.id);


                        if (valeAux.updated > rangeEndAux.AddDays(1.0))
                        {
                            rangeEndAux = rangeEnd.AddDays(1.0).AddHours(6.25);
                            if (valeAux.updated < rangeEndAux)
                            {
                                objects.Add(new ReporteAccPac { vale = valeAux });
                            }
                        }
                        else
                        {
                            objects.Add(new ReporteAccPac { vale = valeAux });
                        }

                    }

                    connection.Close();

                    //--------- FIN de Datos Generales del Vale ----------

                    //--------- Agregar los registros para reporte AccPac ----------

                    foreach (ReporteAccPac reporte in objects)
                    {

                        IList<int> idsp = new List<int>();
                        IList<string> codigos = new List<string>();
                        int cantidad = 0;
                        bool devCompleta = false;

                        auxIdR = reporte.vale.id;

                        reporte.registros = new List<DetalleVale>();

                        if (reporte.vale.id == 2901)
                        {
                            Console.WriteLine("holi");
                        }

                        IList<Devolucion> devolucionesVale = devolucionesDeVale(reporte.vale.id);

                        foreach (Devolucion d in devolucionesVale)
                        {
                            if (d.registros.Count == 1)
                            {
                                if (d.registros[0].tipodev == 3)
                                {
                                    devCompleta = true;
                                    break;
                                }
                            }
                            foreach (DetalleVale det in d.detalles)
                            {
                                if (det.producto.tipo_producto.value < 3)
                                {
                                    foreach (DetalleVale detV in reporte.vale.detalles)
                                    {
                                        if (det.producto.id == detV.producto.id)
                                        {
                                            detV.cantidad = detV.cantidad - det.cantidad;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        if (!devCompleta)
                        {
                            foreach (DetalleVale detV in reporte.vale.detalles)
                            {
                                if (detV.producto.tipo_producto.value < 3)
                                {
                                    if (detV.cantidad > 0)
                                    {
                                        reporte.registros.Add(new DetalleVale
                                        {
                                            producto = new Producto { id = detV.producto.id, codigo = detV.producto.codigo },
                                            cantidad = detV.cantidad
                                        });
                                    }
                                }
                            }

                            //Recorrido para agrupar productos no escaneables
                            SqlCommand command2 = new SqlCommand("sp_reporteAccPacDetailAux", connection);
                            command2.CommandType = CommandType.StoredProcedure;
                            command2.Parameters.Add(new SqlParameter("id", reporte.vale.id));
                            SqlDataAdapter data_adapter2 = new SqlDataAdapter(command2);
                            DataSet data_set2 = new DataSet();
                            data_adapter2.Fill(data_set2);

                            bool productoActivo = false;
                            int cont = 0;
                            string codigoaux = "";

                            foreach (DataRow row in data_set2.Tables[0].Rows)
                            {
                                cont = cont + 1;
                                int auxId = int.Parse(row[2].ToString());

                                if (idsp.Contains(int.Parse(row[2].ToString())))
                                {
                                    if (cont == data_set2.Tables[0].Rows.Count)
                                    {
                                        cantidad = cantidad + 1;

                                        reporte.registros.Add(new DetalleVale
                                        {
                                            producto = new Producto { id = idsp[idsp.Count - 1], codigo = row[4].ToString() },
                                            cantidad = cantidad
                                        });
                                    }
                                    else
                                    {
                                        cantidad = cantidad + 1;
                                    }
                                }
                                else
                                {
                                    if (productoActivo)
                                    {
                                        if (cantidad > 0)
                                        {
                                            reporte.registros.Add(new DetalleVale
                                            {
                                                producto = new Producto { id = idsp[idsp.Count - 1], codigo = codigoaux },
                                                cantidad = cantidad
                                            });
                                        }

                                        if (int.Parse(row[5].ToString()) <= 2)
                                        {
                                            idsp.Add(int.Parse(row[2].ToString()));
                                            cantidad = 0;
                                            cantidad = cantidad + 1;
                                            productoActivo = true;
                                        }
                                        else
                                        {
                                            if (codigos.Contains(row[4].ToString()))
                                            {
                                                cantidad = cantidad + int.Parse(row[1].ToString());
                                            }
                                            else
                                            {
                                                idsp.Add(int.Parse(row[2].ToString()));
                                                codigos.Add(row[4].ToString());
                                                cantidad = 0;
                                                productoActivo = false;

                                                cantidad = cantidad + int.Parse(row[1].ToString());

                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (int.Parse(row[5].ToString()) <= 2)
                                        {
                                            idsp.Add(int.Parse(row[2].ToString()));
                                            cantidad = 0;
                                            cantidad = cantidad + 1;
                                            productoActivo = true;
                                        }
                                        else
                                        {
                                            if (codigos.Contains(row[4].ToString()))
                                            {
                                                if (cont == data_set2.Tables[0].Rows.Count)
                                                {
                                                    cantidad = cantidad + int.Parse(row[1].ToString());

                                                    if (cantidad > 0)
                                                    {
                                                        reporte.registros.Add(new DetalleVale
                                                        {
                                                            producto = new Producto { id = idsp[idsp.Count - 1], codigo = row[4].ToString() },
                                                            cantidad = cantidad
                                                        });
                                                    }
                                                }
                                                else
                                                {
                                                    cantidad = cantidad + int.Parse(row[1].ToString());
                                                }
                                            }
                                            else
                                            {
                                                if (cont == data_set2.Tables[0].Rows.Count)
                                                {
                                                    if (cantidad > 0)
                                                    {
                                                        reporte.registros.Add(new DetalleVale
                                                        {
                                                            producto = new Producto { id = idsp[idsp.Count - 1], codigo = codigoaux },
                                                            cantidad = cantidad
                                                        });
                                                    }

                                                    idsp.Add(int.Parse(row[2].ToString()));
                                                    cantidad = 0;
                                                    cantidad = cantidad + int.Parse(row[1].ToString());

                                                    if (cantidad > 0)
                                                    {
                                                        reporte.registros.Add(new DetalleVale
                                                        {
                                                            producto = new Producto { id = idsp[idsp.Count - 1], codigo = row[4].ToString() },
                                                            cantidad = cantidad
                                                        });
                                                    }
                                                }
                                                else
                                                {
                                                    idsp.Add(int.Parse(row[2].ToString()));
                                                    codigos.Add(row[4].ToString());

                                                    productoActivo = false;

                                                    reporte.registros.Add(new DetalleVale
                                                    {
                                                        producto = new Producto { id = idsp[idsp.Count - 1], codigo = codigoaux },
                                                        cantidad = cantidad
                                                    });

                                                    cantidad = 0;
                                                    cantidad = cantidad + int.Parse(row[1].ToString());
                                                }


                                            }
                                        }
                                    }

                                }

                                codigoaux = row[4].ToString();
                                if (cont == data_set2.Tables[0].Rows.Count)
                                {
                                    if (cantidad > 0)
                                    {
                                        reporte.registros.Add(new DetalleVale
                                        {
                                            producto = new Producto { id = idsp[idsp.Count - 1], codigo = row[4].ToString() },
                                            cantidad = cantidad
                                        });
                                    }
                                }
                            }
                            //Fin de recorrido de productos no escaneables
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
                catch (Exception ex)
                {
                    Console.WriteLine(auxIdR);
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return objects;
                }
            }
        }

        public IList<ReporteDetalleSalidaC> getlistSalidaCombustibleReporte(SalidaCombustibleReporteVo reportesalidavo)
        {

            DateTime rangeStart = DateTime.Parse(reportesalidavo.rangeStart);
            DateTime rangeEnd = DateTime.Parse(reportesalidavo.rangeEnd);

            SqlConnection connection = null;
            IList<ReporteDetalleSalidaC> objects = new List<ReporteDetalleSalidaC>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_reporteAccPacCombustible", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("rangeStart", rangeStart));
                    command.Parameters.Add(new SqlParameter("rangeEnd", rangeEnd));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new ReporteDetalleSalidaC
                        {
                            salidacombustible = new SalidaCombustible
                            {
                                id = int.Parse(row[0].ToString()),
                                timestamp = Convert.ToDateTime(row[1].ToString()),
                                maquinaria = new Maquinaria
                                {
                                    nombre = row[2].ToString()
                                }
                            },
                            cuenta = new Cuenta
                            {
                                num_categoria = row[3].ToString(),
                                numero = row[4].ToString(),
                                tipo_producto = new TipoProducto
                                {
                                    id = int.Parse(row[5].ToString())
                                }
                            },
                            detallesalida = new DetalleSalidaCombustible
                            {
                                litros_surtidos = float.Parse(row[6].ToString()),
                                combustible = new Combustible
                                {
                                    codigo = row[7].ToString()
                                }
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
                catch (Exception ex)
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return objects;
                }
            }


        }

        public IList<ReporteDetalleSalidaC> getlistSalidaCombustibleReportePDF(SalidaCombustibleReportePDFVo reportesalidaPDFvo)
        {

            DateTime rangeStart = DateTime.Parse(reportesalidaPDFvo.rangeStart);
            //DateTime rangeEnd = DateTime.Parse(reportesalidavo.rangeEnd);

            SqlConnection connection = null;
            IList<ReporteDetalleSalidaC> objects = new List<ReporteDetalleSalidaC>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Combustibles_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_reporteAccPacCombustiblePDF", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("rangeStart", rangeStart));
                    command.Parameters.Add(new SqlParameter("turno", reportesalidaPDFvo.turno));
                    command.Parameters.Add(new SqlParameter("pipa", reportesalidaPDFvo.pipa_id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new ReporteDetalleSalidaC
                        {
                            salidacombustible = new SalidaCombustible
                            {
                                id = int.Parse(row[0].ToString()),
                                odometro = int.Parse(row[1].ToString()),
                                turno = int.Parse(row[2].ToString()),
                                timestamp = Convert.ToDateTime(row[3].ToString()),
                                maquinaria = new Maquinaria
                                {
                                    nombre = row[4].ToString()
                                },
                                operador = new Operador
                                {
                                    nombre = row[5].ToString(),
                                    ap_paterno = row[6].ToString(),
                                    ap_materno = row[7].ToString()
                                },
                                despachador = new User
                                {
                                    first_name = row[8].ToString(),
                                    second_name = row[9].ToString()
                                }
                            },
                            pipa = new Pipa
                            {
                                nombre = row[10].ToString(),
                                no_economico = row[11].ToString(),
                                placas = row[12].ToString()
                            },
                            cuenta = new Cuenta
                            {
                                num_categoria = row[13].ToString(),
                                numero = row[14].ToString(),
                                tipo_producto = new TipoProducto
                                {
                                    id = int.Parse(row[15].ToString())
                                }
                            },
                            detallesalida = new DetalleSalidaCombustible
                            {
                                litros_surtidos = float.Parse(row[16].ToString()),
                                combustible = new Combustible
                                {
                                    codigo = row[17].ToString()
                                }
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
                catch (Exception ex)
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return objects;
                }
            }


        }

        //Devuelve los detalles de una devolución por medio del vale id
        public IList<Devolucion> devolucionesDeVale(int id)
        {
            SqlConnection connection = null;
            IList<RegistroDetalleDev> objects = new List<RegistroDetalleDev>();
            IList<Devolucion> devs = new List<Devolucion>();
            int devAux = 0;
            int contReg = 0;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_devolucionDetailByIdVale", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {

                        Devolucion dev = new Devolucion
                        {
                            id = int.Parse(row[0].ToString()),
                            motivo = row[1].ToString(),
                            turno = int.Parse(row[2].ToString()),
                            user = new User { id = int.Parse(row[3].ToString()) },
                            vale = new Vale { id = int.Parse(row[4].ToString()) },
                            timestamp = Convert.ToDateTime(row[5].ToString())
                        };

                        devAux = dev.id;
                        dev.vale = devRepo.detailVale(dev.vale.id);
                        dev.registros = devRepo.detail(dev.id);
                        dev.detalles = new List<DetalleVale>();

                        IList<int> listProductos = new List<int>();
                        int contP = 0;

                        foreach (RegistroDetalleDev reg in dev.registros)
                        {
                            contReg = contReg + 1;
                            if(contReg == 19)
                            {
                                Console.WriteLine("holi2");
                            }
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
                                                Caja cajaAux = devRepo.detailCaja(r.folio);

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
                                                Caja cajaAux = devRepo.detailCaja(r.folio);

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

                                dev.vale = devRepo.detailVale(dev.vale.id);
                            }
                            else if (reg.tipodev == 3)
                            {
                                dev.detalles = devRepo.getAllDetalles(dev.vale.id);
                            }
                        }

                        devs.Add(dev);
                        objects.Clear();
                    }

                    contReg = 0;
                    return devs;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(devAux);
                    Console.WriteLine(contReg);
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    contReg = 0;
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(devAux);
                    Console.WriteLine(contReg);
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    contReg = 0;
                    return null;
                }
            }
        }

        public DateTime fechaAdministrativa(string fecha)
        {
            string[] sqlDateArr1 = fecha.Split('/');

            var sDay = sqlDateArr1[0];
            var sMonth = (int.Parse(sqlDateArr1[1])).ToString();
            string[] sqlDateArr2 = sqlDateArr1[2].Split(' ');

            var sYear = sqlDateArr2[0];
            string[] sqlDateArr3 = sqlDateArr2[1].Split(':');
            var sHour = sqlDateArr3[0];
            var sMinute = sqlDateArr3[1];
            var sSecond = sqlDateArr3[2];

            var dateV = new DateTime(int.Parse(sYear), 
                                     int.Parse(sMonth), 
                                     int.Parse(sDay), 
                                     int.Parse(sHour), 
                                     int.Parse(sMinute), 
                                     int.Parse(sSecond));



            var dateIni = new DateTime(int.Parse(sYear),
                                        int.Parse(sMonth),
                                        int.Parse(sDay), 
                                        0, 
                                        0, 
                                        0);

            var dateFin = new DateTime(int.Parse(sYear),
                                        int.Parse(sMonth),
                                        int.Parse(sDay),
                                        6,
                                        15,
                                        0);

            if (dateV > dateIni && dateV < dateFin)
            {
                var minusOneDay = dateV.AddDays(-1.0);

                //alert("menos un día: " + minusOneDay2);
                return minusOneDay;
            }
            else
            {
                var convertTime = new DateTime(int.Parse(sYear),
                                                int.Parse(sMonth),
                                                int.Parse(sDay),
                                                int.Parse(sHour),
                                                int.Parse(sMinute),
                                                int.Parse(sSecond));

                //alert("fecha normal: " +convertTime);
                return convertTime;
            }
        }
    }
}