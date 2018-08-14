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
    public class BitacoraDesarrolloRepository : IBitacoraDesarrolloRepository
    {
        public int create(BitacoraDesarrollo bitacora)
        {
            bitacora.status_edicion = 1;
            bitacora.dias_apertura_calendario = 0;

            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                { 
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_createBitacoraDesarrollo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("maquinaria_id", bitacora.maquinaria.id));
                    command.Parameters.Add(new SqlParameter("fecha_bitacora", Validations.defaultString(bitacora.fecha_bitacora.ToString())));
                    command.Parameters.Add(new SqlParameter("grupo", Validations.defaultString(bitacora.grupo)));
                    command.Parameters.Add(new SqlParameter("turno", bitacora.turno));
                    command.Parameters.Add(new SqlParameter("compania_id", bitacora.compania.id));
                    command.Parameters.Add(new SqlParameter("vale_acero", Validations.defaultString(bitacora.vale_acero)));
                    command.Parameters.Add(new SqlParameter("vale_explosivos", Validations.defaultString(bitacora.vale_explosivos)));
                    command.Parameters.Add(new SqlParameter("subnivel_id", bitacora.subnivel.id));
                    command.Parameters.Add(new SqlParameter("zona", Validations.defaultString(bitacora.zona)));
                    command.Parameters.Add(new SqlParameter("tipo_desarrollo_id", bitacora.tipo_desarrollo.id));
                    command.Parameters.Add(new SqlParameter("hora_primer_barreno", Validations.defaultString(bitacora.hora_primer_barreno.ToShortTimeString())));
                    command.Parameters.Add(new SqlParameter("hora_ultimo_barreno", Validations.defaultString(bitacora.hora_ultimo_barreno.ToShortTimeString())));
                    command.Parameters.Add(new SqlParameter("numero_barrenos", bitacora.numero_barrenos));
                    command.Parameters.Add(new SqlParameter("anclas", bitacora.anclas));
                    command.Parameters.Add(new SqlParameter("mallas", bitacora.mallas));
                    command.Parameters.Add(new SqlParameter("operador_id", bitacora.operador.id));
                    command.Parameters.Add(new SqlParameter("ayudante_id", bitacora.ayudante.id));
                    command.Parameters.Add(new SqlParameter("comentarios", Validations.defaultString(bitacora.comentarios)));
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

        public TransactionResult delete(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_deleteBitacoraDesarrollo", connection);
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

        public BitacoraDesarrollo detail(int id)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_bitacoraDesarrolloDetail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("id", id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    DataRow row = data_set.Tables[0].Rows[0];
                    return new BitacoraDesarrollo
                    {
                        id = int.Parse(row[0].ToString()),
                        maquinaria = new Maquinaria
                            {
                                id = int.Parse(row[1].ToString()),
                                nombre = row[2].ToString(),
                            },
                        fecha_bitacora = Convert.ToDateTime(row[3].ToString()),
                        grupo = row[4].ToString(),
                        compania = new Compania
                            {
                                id = int.Parse(row[5].ToString()),
                                nombre_sistema = row[6].ToString()
                            },
                        vale_acero = row[7].ToString(),
                        vale_explosivos = row[8].ToString(),
                        turno = int.Parse(row[9].ToString()),
                        subnivel = new SubNivel
                            {
                                id = int.Parse(row[10].ToString()),
                                nombre = row[11].ToString(),
                                nivel = new Nivel
                                {
                                    nombre = row[12].ToString(),
                                    codigo = row[13].ToString()
                                },
                            },
                        zona = row[14].ToString(),
                        tipo_desarrollo = new TipoDesarrollo
                            {
                                id = int.Parse(row[15].ToString()),
                                nombre = row[16].ToString()
                        },
                        hora_primer_barreno = Convert.ToDateTime(row[17].ToString()),
                        hora_ultimo_barreno = Convert.ToDateTime(row[18].ToString()),
                        numero_barrenos = int.Parse(row[19].ToString()),
                        anclas = int.Parse(row[20].ToString()),
                        mallas = int.Parse(row[21].ToString()),
                        operador = new Operador
                            {
                                id = int.Parse(row[22].ToString()),
                                nombre = row[23].ToString(),
                                ap_paterno = row[24].ToString(),
                                ap_materno = row[25].ToString()
                            },
                        comentarios = row[26].ToString(),
                        status_edicion = int.Parse(row[27].ToString()),
                        dias_apertura_calendario = int.Parse(row[28].ToString()),
                        user = new User
                            {
                                id = int.Parse(row[29].ToString()),
                                first_name = row[30].ToString(),
                                second_name = row[31].ToString()
                            },
                        timestamp = Convert.ToDateTime(row[32].ToString()),
                        updated = Convert.ToDateTime(row[33].ToString()),
                        ayudante = new Operador
                        {
                            id = int.Parse(row[35].ToString()),
                            nombre = row[36].ToString(),
                            ap_paterno = row[37].ToString(),
                            ap_materno = row[38].ToString()
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

        public IList<BitacoraDesarrollo> getAll()
        {
            SqlConnection connection = null;
            IList<BitacoraDesarrollo> objects = new List<BitacoraDesarrollo>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllBitacoraDesarrollo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new BitacoraDesarrollo
                        {
                            id = int.Parse(row[0].ToString()),
                            maquinaria = new Maquinaria
                            {
                                id = int.Parse(row[1].ToString()),
                                nombre = row[2].ToString(),
                            },
                            fecha_bitacora = Convert.ToDateTime(row[3].ToString()),
                            grupo = row[4].ToString(),
                            compania = new Compania
                            {
                                id = int.Parse(row[5].ToString()),
                                nombre_sistema = row[6].ToString()
                            },
                            vale_acero = row[7].ToString(),
                            vale_explosivos = row[8].ToString(),
                            turno = int.Parse(row[9].ToString()),
                            subnivel = new SubNivel
                            {
                                id = int.Parse(row[10].ToString()),
                                nombre = row[11].ToString(),
                                nivel = new Nivel
                                {
                                    nombre = row[12].ToString(),
                                    codigo = row[13].ToString()
                                },
                            },
                            zona = row[14].ToString(),
                            tipo_desarrollo = new TipoDesarrollo
                            {
                                id = int.Parse(row[15].ToString()),
                                nombre = row[16].ToString()
                            },
                            hora_primer_barreno = Convert.ToDateTime(row[17].ToString()),
                            hora_ultimo_barreno = Convert.ToDateTime(row[18].ToString()),
                            numero_barrenos = int.Parse(row[19].ToString()),
                            anclas = int.Parse(row[20].ToString()),
                            mallas = int.Parse(row[21].ToString()),
                            operador = new Operador
                            {
                                id = int.Parse(row[22].ToString()),
                                nombre = row[23].ToString(),
                                ap_paterno = row[24].ToString(),
                                ap_materno = row[25].ToString()
                            },
                            comentarios = row[26].ToString(),
                            status_edicion = int.Parse(row[27].ToString()),
                            dias_apertura_calendario = int.Parse(row[28].ToString()),
                            user = new User
                            {
                                id = int.Parse(row[29].ToString()),
                                first_name = row[30].ToString(),
                                second_name = row[31].ToString()
                            },
                            timestamp = Convert.ToDateTime(row[32].ToString()),
                            updated = Convert.ToDateTime(row[33].ToString()),
                            ayudante = new Operador
                            {
                                id = int.Parse(row[35].ToString()),
                                nombre = row[36].ToString(),
                                ap_paterno = row[37].ToString(),
                                ap_materno = row[38].ToString()
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

        public IList<BitacoraDesarrollo> getAllBitacoraByIdSupervisor(int user_id)
        {
            SqlConnection connection = null;
            IList<BitacoraDesarrollo> objects = new List<BitacoraDesarrollo>();
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_getAllBitacoraDesarrolloByUserId", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("user_id", user_id));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new BitacoraDesarrollo
                        {
                            id = int.Parse(row[0].ToString()),
                            maquinaria = new Maquinaria
                            {
                                id = int.Parse(row[1].ToString()),
                                nombre = row[2].ToString(),
                            },
                            fecha_bitacora = Convert.ToDateTime(row[3].ToString()),
                            grupo = row[4].ToString(),
                            compania = new Compania
                            {
                                id = int.Parse(row[5].ToString()),
                                nombre_sistema = row[6].ToString()
                            },
                            vale_acero = row[7].ToString(),
                            vale_explosivos = row[8].ToString(),
                            turno = int.Parse(row[9].ToString()),
                            subnivel = new SubNivel
                            {
                                id = int.Parse(row[10].ToString()),
                                nombre = row[11].ToString(),
                                nivel = new Nivel
                                {
                                    nombre = row[12].ToString(),
                                    codigo = row[13].ToString()
                                },
                            },
                            zona = row[14].ToString(),
                            tipo_desarrollo = new TipoDesarrollo
                            {
                                id = int.Parse(row[15].ToString()),
                                nombre = row[16].ToString()
                            },
                            hora_primer_barreno = Convert.ToDateTime(row[17].ToString()),
                            hora_ultimo_barreno = Convert.ToDateTime(row[18].ToString()),
                            numero_barrenos = int.Parse(row[19].ToString()),
                            anclas = int.Parse(row[20].ToString()),
                            mallas = int.Parse(row[21].ToString()),
                            operador = new Operador
                            {
                                id = int.Parse(row[22].ToString()),
                                nombre = row[23].ToString(),
                                ap_paterno = row[24].ToString(),
                                ap_materno = row[25].ToString()
                            },
                            comentarios = row[26].ToString(),
                            status_edicion = int.Parse(row[27].ToString()),
                            dias_apertura_calendario = int.Parse(row[28].ToString()),
                            user = new User
                            {
                                id = int.Parse(row[29].ToString()),
                                first_name = row[30].ToString(),
                                second_name = row[31].ToString()
                            },
                            timestamp = Convert.ToDateTime(row[32].ToString()),
                            updated = Convert.ToDateTime(row[33].ToString()),
                            ayudante = new Operador
                            {
                                id = int.Parse(row[35].ToString()),
                                nombre = row[36].ToString(),
                                ap_paterno = row[37].ToString(),
                                ap_materno = row[38].ToString()
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


        public TransactionResult update(BitacoraDesarrollo bitacora)
        {
            bitacora.status_edicion = 0;
            bitacora.dias_apertura_calendario = 0;

            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateBitacoraDesarrollo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("maquinaria_id", bitacora.maquinaria.id));
                    command.Parameters.Add(new SqlParameter("fecha_bitacora", Validations.defaultString(bitacora.fecha_bitacora.ToString())));
                    command.Parameters.Add(new SqlParameter("grupo", Validations.defaultString(bitacora.grupo)));
                    command.Parameters.Add(new SqlParameter("turno", bitacora.turno));
                    command.Parameters.Add(new SqlParameter("compania_id", bitacora.compania.id));
                    command.Parameters.Add(new SqlParameter("vale_acero", Validations.defaultString(bitacora.vale_acero)));
                    command.Parameters.Add(new SqlParameter("vale_explosivos", Validations.defaultString(bitacora.vale_explosivos)));
                    command.Parameters.Add(new SqlParameter("subnivel_id", bitacora.subnivel.id));
                    command.Parameters.Add(new SqlParameter("zona", Validations.defaultString(bitacora.zona)));
                    command.Parameters.Add(new SqlParameter("tipo_desarrollo_id", bitacora.tipo_desarrollo.id));
                    command.Parameters.Add(new SqlParameter("hora_primer_barreno", Validations.defaultString(bitacora.hora_primer_barreno.ToShortTimeString())));
                    command.Parameters.Add(new SqlParameter("hora_ultimo_barreno", Validations.defaultString(bitacora.hora_ultimo_barreno.ToShortTimeString())));
                    command.Parameters.Add(new SqlParameter("numero_barrenos", bitacora.numero_barrenos));
                    command.Parameters.Add(new SqlParameter("anclas", bitacora.anclas));
                    command.Parameters.Add(new SqlParameter("mallas", bitacora.mallas));
                    command.Parameters.Add(new SqlParameter("operador_id", bitacora.operador.id));
                    command.Parameters.Add(new SqlParameter("ayudante_id", bitacora.ayudante.id));
                    command.Parameters.Add(new SqlParameter("comentarios", Validations.defaultString(bitacora.comentarios)));
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
                    command.Parameters.Add(new SqlParameter("bitacora_id", demora.bitacora_desarrollo.id));
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
                    command.Parameters.Add(new SqlParameter("tipo_bitacora", 1));
                    SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                    DataSet data_set = new DataSet();
                    data_adapter.Fill(data_set);
                    foreach (DataRow row in data_set.Tables[0].Rows)
                    {
                        objects.Add(new DetalleDemoraBitacora
                        {
                            id = int.Parse(row[0].ToString()),
                            bitacora_desarrollo = new BitacoraDesarrollo
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
                    command.Parameters.Add(new SqlParameter("tipo_bitacora", 1));
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
        public TransactionResult autorizarEdicion(BitacoraDesarrollo bitacora)
        {
            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_autorizarEdicionBitacoraDesarrollo", connection);
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

        public TransactionResult autorizarRango(BitacoraDesarrollo bitacora)
        {

            SqlConnection connection = null;
            using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Coz_Operaciones_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_autorizarRangoBitacoraDesarrollo", connection);
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
    }
}