using Business.Interface;
using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Warrior.Handlers.Enums;
using Models.Auth;

namespace SDMM_API.Controllers
{
    /// <summary>
    /// Vale controller implementation
    /// </summary>
    public class ValeController : BasicApiController
    {
        private IValeService vale_service;

        /// <summary>
        /// Vale controller constructor
        /// </summary>
        /// <param name="vale_service">Injects vale service</param>
        public ValeController(IValeService vale_service)
        {
            this.vale_service = vale_service;
        }

        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/vale/")]
        [HttpGet]
        public HttpResponseMessage list()
        {
            try
            {
                IDictionary<string, IList<Vale>> data = new Dictionary<string, IList<Vale>>();
                data.Add("data", vale_service.getAll());
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format("There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }


        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/vale/activos/")]
        [HttpGet]
        public HttpResponseMessage listActivos()
        {
            try
            {

                IList<Vale> valesTodos = vale_service.getAll();

                IList<Vale> valesActivos = new List<Vale>();

                foreach (Vale v in valesTodos)
                {
                    if (v.active != 0 && v.userAutorizo.id != 0)
                    {
                        valesActivos.Add(v);
                    }
                }

                IDictionary<string, IList<Vale>> data = new Dictionary<string, IList<Vale>>();
                data.Add("data", valesActivos);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format("There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        /// <summary>
        /// Create object pettition
        /// </summary>
        /// <param name="empleado_vo"></param>
        /// <returns></returns>
        [Route("api/vale/")]
        [HttpPost]
        public HttpResponseMessage create([FromBody] ValeVo vale_vo)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();

            if (vale_vo == null)
            {
                data.Add("message", "El objeto recibido es nulo.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }

            if (vale_vo.folio_fisico == null || vale_vo.folio_fisico == string.Empty)
            {
                vale_vo.folio_fisico = "0";
            }

            string s = "";

            if (vale_vo.detalles == null || vale_vo.detalles.Count == 0)
            {
                data.Add("message", "La lista de detalles es nula.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
            else
            {
                foreach (DetalleValeVo d in vale_vo.detalles)
                {
                    s = s + " | " + "cantidad: " + d.cantidad.ToString() + ", producto_id: " + d.producto_id.ToString();

                    if (d.cantidad == 0 || d.producto_id == 0)
                    {
                        data.Add("message", s);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, data);
                    }
                }
            }

            IList<int> idsProductos = new List<int>();
            IList<DetalleValeVo> detallesAux = new List<DetalleValeVo>();

            foreach (DetalleValeVo d in vale_vo.detalles)
            {
                if (!idsProductos.Contains(d.producto_id))
                {
                    detallesAux.Add(d);
                    idsProductos.Add(d.producto_id);
                }
            }


            vale_vo.detalles = detallesAux;

            TransactionResult tr;

            if (vale_vo.user_id != 0)
            {
                tr = vale_service.create(vale_vo, new Models.Auth.User { id = vale_vo.user_id });
            }
            else
            {
                tr = vale_service.create(vale_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
            }

            if (tr == TransactionResult.CREATED)
            {
                data.Add("message", "Object created.");
                return Request.CreateResponse(HttpStatusCode.Created, data);
            }
            else if (tr == TransactionResult.EXISTS)
            {
                data.Add("message", "Object already existed.");
                return Request.CreateResponse(HttpStatusCode.Conflict, data);
            }
            else
            {
                data.Add("message", "There was an error attending your request.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        // <summary>
        /// Get vale detalle by vale id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/vale/{id}")]
        [HttpGet]
        public HttpResponseMessage detail(int id)
        {
            Vale vale = new Vale();
            try
            {
                vale = vale_service.detail(id);
                vale.detalles = vale_service.getDetailsByValeId(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }

            if (vale != null)
            {
                IDictionary<string, Vale> data = new Dictionary<string, Vale>();
                data.Add("data", vale);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            else
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", "Object not found.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        /// <summary>
        /// Get vale detalle by vale id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/vale/detalle/{id}")]
        [HttpGet]
        public HttpResponseMessage listDetails(int id) {
            try
            {
                IDictionary<string, IList<DetalleVale>> data = new Dictionary<string, IList<DetalleVale>>();
                data.Add("data", vale_service.getDetailsByValeId(id));
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format("There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        /// <summary>
        /// List registers by detalle id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/vale/detalle/registro/{id}")]
        [HttpGet]
        public HttpResponseMessage listRegisters(int id) {
            try
            {
                IDictionary<string, IList<RegistroDetalle>> data = new Dictionary<string, IList<RegistroDetalle>>();
                data.Add("data", vale_service.getAllRegistersByDetalle(id));
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format("There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }


        /// <summary>
        /// List registers by detalle id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/vale/historicoNum/")]
        [HttpPost]
        public HttpResponseMessage iteracionesHistorico([FromBody] RegistrosCompararVo listComparar)
        {
            try
            {
                IList<int> list = new List<int>();
                /*IList<RegistroDetalle> listH = vale_service.getAllRegistersHistorico();

                foreach(RegistroDetalle r in listH)
                {
                    list.Add(r.id);
                }*/
                if (listComparar != null && listComparar.registros_vo.Count != 0)
                {
                    IList<RegistroDetalle> registrosResponse = new List<RegistroDetalle>();
                    IList<RegistroDetalle> registrosH = new List<RegistroDetalle>();
                    registrosH = vale_service.getAllRegistersHistorico();

                    IList<int> idsExistentes = new List<int>();
                    foreach (RegistroCompararVo reg in listComparar.registros_vo)
                    {
                        idsExistentes.Add(reg.id);
                    }


                    foreach (RegistroDetalle r in registrosH)
                    {
                        if (idsExistentes.Contains(r.id))
                        {
                            foreach (RegistroCompararVo reg in listComparar.registros_vo)
                            {
                                if (r.id == reg.id)
                                {
                                    if (r.status != reg.estatus)
                                    {
                                        registrosResponse.Add(r);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            registrosResponse.Add(r);
                        }
                    }

                    list.Add(registrosResponse.Count);
                }
                else
                {
                    list.Add(vale_service.getAllRegistersHistorico().Count);
                }
                IDictionary<string, IList<int>> data = new Dictionary<string, IList<int>>();
                data.Add("data", list);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format("There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        /// <summary>
        /// List registers by detalle id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/vale/detalle/registro/historico/")]
        [HttpGet]
        public HttpResponseMessage listRegistersHist([FromBody] IList<RegistroCompararVo> listComparar)
        {
            try
            {
                IDictionary<string, IList<RegistroDetalle>> data = new Dictionary<string, IList<RegistroDetalle>>();
                data.Add("data", vale_service.getAllRegistersHistorico());
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format("There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }


        /// <summary>
        /// List registers by detalle id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/vale/detalle/registro/historico/")]
        [HttpPost]
        public HttpResponseMessage listRegistersHist2([FromBody] RegistrosCompararVo listComparar)
        {
            try
            {
                int iteracion = listComparar.registros_vo[listComparar.registros_vo.Count - 1].id;
                listComparar.registros_vo.RemoveAt(listComparar.registros_vo.Count - 1);
                IList<RegistroDetalle> registrosResponse = new List<RegistroDetalle>();
                IList<RegistroDetalle> registrosH = new List<RegistroDetalle>();
                registrosH = vale_service.getAllRegistersHistorico();

                IList<int> idsExistentes = new List<int>();

                if (listComparar.registros_vo.Count > 1 && listComparar.registros_vo != null)
                {

                    IList<RegistroDetalle> registrosResponseAux = new List<RegistroDetalle>();

                    idsExistentes = new List<int>();
                    foreach (RegistroCompararVo reg in listComparar.registros_vo)
                    {
                        idsExistentes.Add(reg.id);
                    }


                    foreach (RegistroDetalle r in registrosH)
                    {
                        if (idsExistentes.Contains(r.id))
                        {
                            foreach (RegistroCompararVo reg in listComparar.registros_vo)
                            {
                                if (r.id == reg.id)
                                {
                                    if (r.status != reg.estatus)
                                    {
                                        registrosResponseAux.Add(r);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            registrosResponseAux.Add(r);
                        }
                    }

                    iteracion = iteracion * 800;
                    int cont = 0;
                    foreach (RegistroDetalle reg in registrosResponseAux)
                    {
                        if ((iteracion + cont) != registrosResponseAux.Count)
                        {
                            registrosResponse.Add(registrosResponseAux[iteracion + cont]);
                            cont = cont + 1;
                            if (cont == 800)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                }
                else
                {
                    iteracion = iteracion * 800;
                    int cont = 0;
                    foreach(RegistroDetalle reg in registrosH)
                    {
                        if ((iteracion + cont) != registrosH.Count)
                        {
                            registrosResponse.Add(registrosH[iteracion + cont]);
                            cont = cont + 1;
                            if (cont == 800)
                            {
                                break;
                            }
                        }else
                        {
                            break;
                        }
                    }
                    
                }

                IDictionary<string, IList<RegistroDetalle>> data = new Dictionary<string, IList<RegistroDetalle>>();
                data.Add("data", registrosResponse);
                idsExistentes.Clear();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format("There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        /// <summary>
        /// List registers by detalle id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/vale/detalle/registro/over/historico/")]
        [HttpGet]
        public HttpResponseMessage listRegistersHistOver()
        {
            try
            {
                IDictionary<string, IList<RegistroDetalle>> data = new Dictionary<string, IList<RegistroDetalle>>();
                data.Add("data", vale_service.getAllRegistersHistoricoOver());
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format("There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        /// <summary>
        /// List registers by detalle id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/vale/detalle/registro/over/{id}")]
        [HttpGet]
        public HttpResponseMessage listRegistersOver(int id)
        {
            try
            {
                IDictionary<string, IList<RegistroDetalle>> data = new Dictionary<string, IList<RegistroDetalle>>();
                data.Add("data", vale_service.getAllRegistersOverByDetalle(id));
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format("There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        /// <summary>
        /// Create register
        /// </summary>
        /// <param name="registrodetalle_vo"></param>
        /// <returns></returns>
        [Route("api/vale/detalle/registro/")]
        [HttpPost]
        public HttpResponseMessage createRegistro([FromBody] RegistroDetalleVo registrodetalle_vo)
        {
            TransactionResult tr = vale_service.createRegistroDetalle(registrodetalle_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
            IDictionary<string, string> data = new Dictionary<string, string>();
            if (tr == TransactionResult.CREATED)
            {
                data.Add("message", "Object created.");
                return Request.CreateResponse(HttpStatusCode.Created, data);
            }
            else if (tr == TransactionResult.EXISTS)
            {
                data.Add("message", "Object already existed.");
                return Request.CreateResponse(HttpStatusCode.Conflict, data);
            }
            else
            {
                data.Add("message", "There was an error attending your request.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        /// <summary>
        /// Create register
        /// </summary>
        /// <param name="registrodetalle_vo"></param>
        /// <returns></returns>
        [Route("api/vale/detalle/registro/especial/")]
        [HttpPost]
        public HttpResponseMessage createRegistroEspecial([FromBody] RegistroDetalleVo registrodetalle_vo)
        {
            TransactionResult tr = vale_service.createRegistroDetalleOver(registrodetalle_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
            IDictionary<string, string> data = new Dictionary<string, string>();
            if (tr == TransactionResult.CREATED)
            {
                data.Add("message", "Object created.");
                return Request.CreateResponse(HttpStatusCode.Created, data);
            }
            else if (tr == TransactionResult.EXISTS)
            {
                data.Add("message", "Object already existed.");
                return Request.CreateResponse(HttpStatusCode.Conflict, data);
            }
            else
            {
                data.Add("message", "There was an error attending your request.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        [Route("api/vale/detalle/registro/bunch/")]
        [HttpPost]
        public HttpResponseMessage createRegistroBunch([FromBody]  RegistersVo registros)
        {
            Console.WriteLine(Request.Content);
            TransactionResult tr = vale_service.createRegistroDetalleByList(registros.registrodetalles_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
            IDictionary<string, string> data = new Dictionary<string, string>();
            if (tr == TransactionResult.CREATED)
            {
                data.Add("message", "Object created.");
                return Request.CreateResponse(HttpStatusCode.Created, data);
            }
            else if (tr == TransactionResult.EXISTS)
            {
                data.Add("message", "Object already existed.");
                return Request.CreateResponse(HttpStatusCode.Conflict, data);
            }
            else
            {
                data.Add("message", "There was an error attending your request.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }


        /// <summary>
        /// Create object pettition
        /// </summary>
        /// <param name="empleado_vo"></param>
        /// <returns></returns>
        [Route("api/vale/updateactive/")]
        [HttpPut]
        public HttpResponseMessage updateActive([FromBody] ValeVo vale_vo)
        {
            vale_vo.active = 0;
            TransactionResult tr = vale_service.updateStatus(vale_vo);
            IDictionary<string, string> data = new Dictionary<string, string>();
            if (tr == TransactionResult.OK)
            {
                data.Add("message", "Object updated.");
                data.Add("status", "1");
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            else
            {
                data.Add("message", "There was an error attending your request.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }


        /// <summary>
        /// List registers by detalle id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/vale/detalle/registro/caja/{id}")]
        [HttpGet]
        public HttpResponseMessage listRegistersByCaja(string id)
        {
            try
            {
                IDictionary<string, IList<RegistroDetalle>> data = new Dictionary<string, IList<RegistroDetalle>>();
                data.Add("data", vale_service.getAllRegistersByFolioCaja(id));
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format("There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }


        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/vale/detalle/registro/sacos/")]
        [HttpGet]
        public HttpResponseMessage listRegistersSacos()
        {
            try
            {
                IDictionary<string, IList<RegistroDetalle>> data = new Dictionary<string, IList<RegistroDetalle>>();
                data.Add("data", vale_service.getAllRegistersSacos());
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format("There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }


        ////////////// update vale controller 
        /// <summary>
        /// Create object pettition
        /// </summary>
        /// <param name="empleado_vo"></param>
        /// <returns></returns>
        [Route("api/vale/")]
        [HttpPut]
        public HttpResponseMessage update([FromBody] ValeVo vale)
        {
            TransactionResult tr;

            if (vale.autorizo == 1)
            {
                tr = vale_service.updateAutorizacion(vale, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
            }
            else if (vale.autorizo == 2)
            {
                tr = vale_service.updateAutorizacion(vale, new Models.Auth.User { id = vale.user_id_autorizo });
            }
            else
            {
                tr = vale_service.update(vale);
            }
            //TransactionResult tr = vale_service.update(vale);
            IDictionary<string, string> data = new Dictionary<string, string>();
            if (tr == TransactionResult.OK)
            {
                data.Add("message", "Object updated.");
                data.Add("status", "1");
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            else
            {
                data.Add("message", "There was an error attending your request.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }

        }

        /// <summary>
        /// Delete object request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/vale/{id}")]
        [HttpDelete]
        public HttpResponseMessage delete(int id)
        {
            TransactionResult tr = vale_service.delete(id);
            IDictionary<string, string> data = new Dictionary<string, string>();
            if (tr == TransactionResult.DELETED)
            {
                data.Add("message", "Object deleted.");
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            else
            {
                data.Add("message", "There was an error attending your request.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        /// <summary>
        /// Create register
        /// </summary>
        /// <param name="registrodetalle_vo"></param>
        /// <returns></returns>
        [Route("api/vale/validar")]
        [HttpPost]
        public HttpResponseMessage validarLoginTablet([FromBody] UserVo user_vo)
        {
            try
            {
                IDictionary<string, User> data = new Dictionary<string, User>();
                data.Add("data", vale_service.validarLoginTablet(user_vo));
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format("There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }
    }
}
