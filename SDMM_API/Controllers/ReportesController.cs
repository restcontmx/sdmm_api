﻿using Business.Interface;
using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SDMM_API.Controllers
{
    public class ReportesController : BasicApiController
    {
        private IReportesService reportes_service;

        /// <summary>
        /// Vale controller constructor
        /// </summary>
        /// <param name="vale_service">Injects vale service</param>
        public ReportesController(IReportesService reportes_service)
        {
            this.reportes_service = reportes_service;
        }

        [Route("api/reporte/sedena")]
        [HttpPost]
        public HttpResponseMessage listSedena([FromBody] ReportesVo list)
        {
            try
            {
                IDictionary<string, IList<RegistroDetalle>> data = new Dictionary<string, IList<RegistroDetalle>>();
                data.Add("data", reportes_service.getListaSedena(list));
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format("There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        [Route("api/reporte/vale")]
        [HttpGet]
        public HttpResponseMessage listVale([FromBody] ReportesVo list)
        {
            try
            {
                IDictionary<string, IList<Vale>> data = new Dictionary<string, IList<Vale>>();
                data.Add("data", reportes_service.getListaVale(list));
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