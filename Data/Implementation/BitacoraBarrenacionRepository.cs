using Data.Interface;
using System;
using System.Collections.Generic;
using Models.Catalogs;
using Warrior.Handlers.Enums;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Models.Auth;
using Warrior.Data;

namespace Data.Implementation
{
    public class BitacoraBarrenacionRepository : IBitacoraBarrenacionRepository
    {
        public int create(BitacoraBarrenacion bitacora)
        {
            bitacora.status_edicion = 1;
            bitacora.dias_apertura_calendario = 0;

            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createBitacoraBarrenacion", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("maquinaria_id", bitacora.maquinaria.id));
                    command.Parameters.Add(new SqlParameter("operador_id", bitacora.operador.id));
                    command.Parameters.Add(new SqlParameter("ayudante_id", bitacora.ayudante.id));
                    command.Parameters.Add(new SqlParameter("turno", bitacora.turno));
                    command.Parameters.Add(new SqlParameter("fecha_bitacora", bitacora.fecha_bitacora));
                    command.Parameters.Add(new SqlParameter("mesa", bitacora.mesa));
                    command.Parameters.Add(new SqlParameter("beta", bitacora.beta));
                    command.Parameters.Add(new SqlParameter("comentarios", Validations.defaultString(bitacora.comentarios)));
                    command.Parameters.Add(new SqlParameter("metros_finales", bitacora.metros_finales));
                    command.Parameters.Add(new SqlParameter("vale_acero", Validations.defaultString(bitacora.vale_acero)));
                    command.Parameters.Add(new SqlParameter("hora_primer_barreno", Validations.defaultString(bitacora.hora_primer_barreno.ToShortTimeString())));
                    command.Parameters.Add(new SqlParameter("hora_ultimo_barreno", Validations.defaultString(bitacora.hora_ultimo_barreno.ToShortTimeString())));
                    command.Parameters.Add(new SqlParameter("status_edicion", bitacora.status_edicion));
                    command.Parameters.Add(new SqlParameter("dias_apertura_calendario", bitacora.dias_apertura_calendario));
                    command.Parameters.Add(new SqlParameter("user_id", bitacora.user.id));
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return 0;
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
                    SqlCommand command = new SqlCommand("sp_deleteBitacoraBarrenacion", connection);
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

        public BitacoraBarrenacion detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_bitacoraBarrenacionDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new BitacoraBarrenacion
                    {
                        id = int.Parse(row[0].ToString()),
                        maquinaria = new Maquinaria
                        {
                            id = int.Parse(row[1].ToString()),
                            nombre = row[2].ToString(),
                        },
                        operador = new Operador
                        {
                            id = int.Parse(row[3].ToString()),
                            nombre = row[4].ToString(),
                            ap_paterno = row[5].ToString(),
                            ap_materno = row[6].ToString()
                        },
                        turno = int.Parse(row[7].ToString()),
                        fecha_bitacora = Convert.ToDateTime(row[8].ToString()),
                        mesa = row[9].ToString(),
                        beta = row[10].ToString(),
                        comentarios = row[11].ToString(),
                        metros_finales = double.Parse(row[12].ToString()),
                        vale_acero = row[13].ToString(),
                        hora_primer_barreno = Convert.ToDateTime(row[14].ToString()),
                        hora_ultimo_barreno = Convert.ToDateTime(row[15].ToString()),
                        status_edicion = int.Parse(row[16].ToString()),
                        dias_apertura_calendario = int.Parse(row[17].ToString()),
                        user = new User
                        {
                            id = int.Parse(row[18].ToString()),
                            first_name = row[19].ToString(),
                            second_name = row[20].ToString()
                        },
                        timestamp = Convert.ToDateTime(row[21].ToString()),
                        updated = Convert.ToDateTime(row[22].ToString()),
                        ayudante = new Operador
                        {
                            id = int.Parse(row[24].ToString()),
                            nombre = row[25].ToString(),
                            ap_paterno = row[26].ToString(),
                            ap_materno = row[27].ToString()
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

        public IList<BitacoraBarrenacion> getAll()
        {
            SqlConnection connection = null;
            IList<BitacoraBarrenacion> objects = new List<BitacoraBarrenacion>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllBitacoraBarrenacion", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new BitacoraBarrenacion
                        {
                            id = int.Parse(row[0].ToString()),
                            maquinaria = new Maquinaria
                            {
                                id = int.Parse(row[1].ToString()),
                                nombre = row[2].ToString(),
                            },
                            operador = new Operador
                            {
                                id = int.Parse(row[3].ToString()),
                                nombre = row[4].ToString(),
                                ap_paterno = row[5].ToString(),
                                ap_materno = row[6].ToString()
                            },
                            turno = int.Parse(row[7].ToString()),
                            fecha_bitacora = Convert.ToDateTime(row[8].ToString()),
                            mesa = row[9].ToString(),
                            beta = row[10].ToString(),
                            comentarios = row[11].ToString(),
                            metros_finales = double.Parse(row[12].ToString()),
                            vale_acero = row[13].ToString(),
                            hora_primer_barreno = Convert.ToDateTime(row[14].ToString()),
                            hora_ultimo_barreno = Convert.ToDateTime(row[15].ToString()),
                            status_edicion = int.Parse(row[16].ToString()),
                            dias_apertura_calendario = int.Parse(row[17].ToString()),
                            user = new User
                            {
                                id = int.Parse(row[18].ToString()),
                                first_name = row[19].ToString(),
                                second_name = row[20].ToString()
                            },
                            timestamp = Convert.ToDateTime(row[21].ToString()),
                            updated = Convert.ToDateTime(row[22].ToString()),
                            ayudante = new Operador
                            {
                                id = int.Parse(row[24].ToString()),
                                nombre = row[25].ToString(),
                                ap_paterno = row[26].ToString(),
                                ap_materno = row[27].ToString()
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

        public IList<BitacoraBarrenacion> getAllBitacoraByIdSupervisor(int user_id)
        {
            SqlConnection connection = null;
            IList<BitacoraBarrenacion> objects = new List<BitacoraBarrenacion>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllBitacoraBarrenacionByUserId", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("user_id", user_id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new BitacoraBarrenacion
                        {
                            id = int.Parse(row[0].ToString()),
                            maquinaria = new Maquinaria
                            {
                                id = int.Parse(row[1].ToString()),
                                nombre = row[2].ToString(),
                            },
                            operador = new Operador
                            {
                                id = int.Parse(row[3].ToString()),
                                nombre = row[4].ToString(),
                                ap_paterno = row[5].ToString(),
                                ap_materno = row[6].ToString()
                            },
                            turno = int.Parse(row[7].ToString()),
                            fecha_bitacora = Convert.ToDateTime(row[8].ToString()),
                            mesa = row[9].ToString(),
                            beta = row[10].ToString(),
                            comentarios = row[11].ToString(),
                            metros_finales = double.Parse(row[12].ToString()),
                            vale_acero = row[13].ToString(),
                            hora_primer_barreno = Convert.ToDateTime(row[14].ToString()),
                            hora_ultimo_barreno = Convert.ToDateTime(row[15].ToString()),                            
                            status_edicion = int.Parse(row[16].ToString()),
                            dias_apertura_calendario = int.Parse(row[17].ToString()),
                            user = new User
                            {
                                id = int.Parse(row[18].ToString()),
                                first_name = row[19].ToString(),
                                second_name = row[20].ToString()
                            },
                            timestamp = Convert.ToDateTime(row[21].ToString()),
                            updated = Convert.ToDateTime(row[22].ToString()),
                            ayudante = new Operador
                            {
                                id = int.Parse(row[24].ToString()),
                                nombre = row[25].ToString(),
                                ap_paterno = row[26].ToString(),
                                ap_materno = row[27].ToString()
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


        public TransactionResult update(BitacoraBarrenacion bitacora)
        {
            bitacora.status_edicion = 0;
            bitacora.dias_apertura_calendario = 0;

            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateBitacoraBarrenacion", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("maquinaria_id", bitacora.maquinaria.id));
                    command.Parameters.Add(new SqlParameter("operador_id", bitacora.operador.id));
                    command.Parameters.Add(new SqlParameter("ayudante_id", bitacora.ayudante.id));
                    command.Parameters.Add(new SqlParameter("turno", bitacora.turno));
                    command.Parameters.Add(new SqlParameter("fecha_bitacora", bitacora.fecha_bitacora));
                    command.Parameters.Add(new SqlParameter("mesa", bitacora.mesa));
                    command.Parameters.Add(new SqlParameter("beta", bitacora.beta));
                    command.Parameters.Add(new SqlParameter("comentarios", Validations.defaultString(bitacora.comentarios)));
                    command.Parameters.Add(new SqlParameter("metros_finales", bitacora.metros_finales));
                    command.Parameters.Add(new SqlParameter("vale_acero", Validations.defaultString(bitacora.vale_acero)));
                    command.Parameters.Add(new SqlParameter("hora_primer_barreno", Validations.defaultString(bitacora.hora_primer_barreno.ToShortTimeString())));
                    command.Parameters.Add(new SqlParameter("hora_ultimo_barreno", Validations.defaultString(bitacora.hora_ultimo_barreno.ToShortTimeString())));
                    command.Parameters.Add(new SqlParameter("status_edicion", bitacora.status_edicion));
                    command.Parameters.Add(new SqlParameter("dias_apertura_calendario", bitacora.dias_apertura_calendario));
                    command.Parameters.Add(new SqlParameter("user_id", bitacora.user.id));
                    command.Parameters.Add(new SqlParameter("id", bitacora.id));
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return TransactionResult.ERROR;
                }
            }
        }


        /*************** SECCIÓN DE DEMORAS **********************/
        public TransactionResult createDetalleDemoraBitacora(DetalleDemoraBitacora demora)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createDetalleDemoraBitacora", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("bitacora_id", demora.bitacora_barrenacion.id));
                    command.Parameters.Add(new SqlParameter("tipo_bitacora", demora.tipo_bitacora));
                    command.Parameters.Add(new SqlParameter("demora_id", demora.demora.id));
                    command.Parameters.Add(new SqlParameter("comentarios", Validations.defaultString(demora.comentarios)));
                    command.Parameters.Add(new SqlParameter("horas_perdidas", demora.horas_perdidas.ToString()));
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

        public IList<DetalleDemoraBitacora> getDemorasByIdBitacora(int bitacora_id)
        {
            SqlConnection connection = null;
            IList<DetalleDemoraBitacora> objects = new List<DetalleDemoraBitacora>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllDetalleDemoraBitacora", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("bitacora_id", bitacora_id));
                    command.Parameters.Add(new SqlParameter("tipo_bitacora", 2));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new DetalleDemoraBitacora
                        {
                            id = int.Parse(row[0].ToString()),
                            bitacora_barrenacion = new BitacoraBarrenacion
                            {
                                id = int.Parse(row[1].ToString())
                            },
                            tipo_bitacora = int.Parse(row[2].ToString()),
                            demora = new Demora
                            {
                                id = int.Parse(row[3].ToString()),
                                nombre = row[4].ToString()
                            },
                            comentarios = row[5].ToString(),
                            horas_perdidas = Convert.ToDateTime(row[6].ToString()),
                            timestamp = Convert.ToDateTime(row[7].ToString()),
                            updated = Convert.ToDateTime(row[8].ToString())
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

        public TransactionResult deleteDetalleDemoraBitacora(int bitacora_id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_deleteDetalleDemoraBitacoraByIdBitacora", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("bitacora_id", bitacora_id));
                    command.Parameters.Add(new SqlParameter("tipo_bitacora", 2));
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

        // ------------------- AUTORIZACIONES -------------------
        public TransactionResult autorizarEdicion(BitacoraBarrenacion bitacora)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_autorizarEdicionBitacoraBarrenacion", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", bitacora.id));
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return TransactionResult.ERROR;
                }
            }
        }

        public TransactionResult autorizarRango(BitacoraBarrenacion bitacora)
        {

            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_autorizarRangoBitacoraBarrenacion", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("dias_apertura_calendario", bitacora.dias_apertura_calendario));
                    command.Parameters.Add(new SqlParameter("id", bitacora.id));
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    return TransactionResult.ERROR;
                }
            }
        }

        /*************** SECCIÓN DE LINEAS **********************/
        public int createLineaBitacora(Linea linea)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createLinea", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("numero", linea.numero));
                    command.Parameters.Add(new SqlParameter("vale_id", linea.vale.id));
                    command.Parameters.Add(new SqlParameter("subnivel_id", linea.subnivel.id));
                    command.Parameters.Add(new SqlParameter("tipo", linea.tipo));
                    command.Parameters.Add(new SqlParameter("bitacora_id", linea.bitacora.id));
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

        public IList<Linea> getLineasByIdBitacora(int bitacora_id)
        {
            SqlConnection connection = null;
            IList<Linea> objects = new List<Linea>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllLineaByIdBitacora", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("bitacora_id", bitacora_id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Linea
                        {
                            id = int.Parse(row[0].ToString()),
                            numero = row[1].ToString(),
                            vale = new Vale { id = int.Parse(row[2].ToString()) },
                            tipo = int.Parse(row[3].ToString()),
                            bitacora = new BitacoraBarrenacion { id = int.Parse(row[4].ToString()) },
                            subnivel = new SubNivel
                            {
                                id = int.Parse(row[5].ToString()),
                                nombre = row[6].ToString(),
                                nivel = new Nivel
                                {
                                    nombre = row[7].ToString(),
                                    codigo = row[8].ToString()
                                },
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

        public TransactionResult deleteLineaByIdBitacora(int bitacora_id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_deleteLineasByIdBitacora", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("bitacora_id", bitacora_id));
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

        /*************** SECCIÓN DE BARRENOS **********************/
        public TransactionResult createBarrenoLineaBitacora(Barreno barreno)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createBarreno", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("cantidad", barreno.cantidad));
                    command.Parameters.Add(new SqlParameter("longitud", barreno.longitud));
                    command.Parameters.Add(new SqlParameter("metros", barreno.metros));
                    command.Parameters.Add(new SqlParameter("linea_id", barreno.linea.id));
                    command.Parameters.Add(new SqlParameter("bitacora_id", barreno.bitacora.id));
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

        public IList<Barreno> getBarrenosByIdLinea(int linea_id)
        {
            SqlConnection connection = null;
            IList<Barreno> objects = new List<Barreno>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllBarrenoByIdLinea", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("linea_id", linea_id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new Barreno
                        {
                            id = int.Parse(row[0].ToString()),
                            cantidad = double.Parse(row[1].ToString()),
                            longitud = double.Parse(row[2].ToString()),
                            metros = double.Parse(row[3].ToString()),
                            linea = new Linea { id = int.Parse(row[4].ToString()) },
                            bitacora = new BitacoraBarrenacion { id = int.Parse(row[5].ToString()) }
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

        public TransactionResult deleteBarrenosByIdBitacora(int bitacora_id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_deleteBarrenosByIdBitacora", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("bitacora_id", bitacora_id));
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