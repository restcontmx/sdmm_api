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

        public IList<Vale> getListaVale(ReportesVo reportes_vo)
        {

            SqlConnection connection = null;
            IList<Vale> objects = new List<Vale>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    /*connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllVale", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {

                    }*/
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