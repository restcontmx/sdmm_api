using Business.Interface;
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

        [Route("api/reporte/accpac")]
        [HttpPost]
        public HttpResponseMessage listVale([FromBody] ReportesVo list)
        {
            try
            {
                IDictionary<string, IList<ReporteAccPac>> data = new Dictionary<string, IList<ReporteAccPac>>();
                data.Add("data", reportes_service.getListVale(list));
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format("There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        [Route("api/reporte/salidaCombustibleAccpac")]
        [HttpPost]
        public HttpResponseMessage listSalidaCombustibleReporte([FromBody] SalidaCombustibleReporteVo list)
        {
            try
            {
                IDictionary<string, IList<ReporteDetalleSalidaC>> data = new Dictionary<string, IList<ReporteDetalleSalidaC>>();
                data.Add("data", reportes_service.getlistSalidaCombustibleReporte(list));
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format("There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        [Route("api/reporte/salidaCombustibleAccpacPDF")]
        [HttpPost]
        public HttpResponseMessage listSalidaCombustibleReportePDF([FromBody] SalidaCombustibleReportePDFVo list)
        {
            try
            {
                IDictionary<string, IList<ReporteDetalleSalidaC>> data = new Dictionary<string, IList<ReporteDetalleSalidaC>>();
                data.Add("data", reportes_service.getlistSalidaCombustibleReportePDF(list));
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