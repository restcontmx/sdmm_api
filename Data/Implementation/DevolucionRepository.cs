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

        public TransactionResult createP(Devolucion devolucion)
        {

            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    connection.Open();
                    if (devolucion.tipo == 1)
                    {
                        command = new SqlCommand("sp_createDevolucion", connection);
                    }
                    else if (devolucion.tipo == 2)
                    {
                        command = new SqlCommand("sp_createDevolucionCaja", connection);
                    }
                    else if (devolucion.tipo == 3)
                    {
                        // si entra aquí se necesita terminar aquí para comprobar que fue realizada correctamente la consulta
                        command = new SqlCommand("sp_createDevolucionVale", connection);

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("folio", devolucion.folio));
                        command.Parameters.Add(new SqlParameter("user_id", devolucion.user.id));
                        command.ExecuteNonQuery();


                        IList<DetalleVale> detalles = getAllDetalles(int.Parse(devolucion.folio));

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
                                    connection.Open();
                                    SqlCommand command4 = new SqlCommand("sp_createDevolucion", connection);
                                    command4.CommandType = CommandType.StoredProcedure;
                                    command4.Parameters.Add(new SqlParameter("proveedor_id", devolucion.proveedor.id));
                                    command4.Parameters.Add(new SqlParameter("folio", r.folio));
                                    command4.Parameters.Add(new SqlParameter("user_id", devolucion.user.id));
                                    command4.ExecuteNonQuery();

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

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("proveedor_id", devolucion.proveedor.id));
                    command.Parameters.Add(new SqlParameter("folio", devolucion.folio));
                    command.Parameters.Add(new SqlParameter("user_id", devolucion.user.id));
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
    }
}