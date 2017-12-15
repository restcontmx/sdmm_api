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

namespace Data.Implementation
{
    public class ReportesRepository : IReportesRepository
    {
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
            DateTime rangeEnd = DateTime.Parse(reportes_vo.rangeEnd);

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
                        Vale valeAux = new Vale
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

    }
}