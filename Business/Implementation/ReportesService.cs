using Business.Interface;
using Data.Interface;
using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Implementation
{
    public class ReportesService : IReportesService
    {
        private IReportesRepository reportes_repository;

        public ReportesService(IReportesRepository reportes_repository)
        {
            this.reportes_repository = reportes_repository;
        }

        public IList<RegistroDetalle> getListaSedena(ReportesVo reportes_vo)
        {
            return reportes_repository.getListaSedena(reportes_vo);
        }

        public IList<ReporteAccPac> getListVale(ReportesVo reportes_vo)
        {
            //return reportes_repository.getListVale(reportes_vo);
            return reportes_repository.getListValeFeb2018(reportes_vo);
        }

        public IList<ReporteDetalleSalidaC> getlistSalidaCombustibleReporte(SalidaCombustibleReporteVo salidaComReporteVo)
        {
            //return reportes_repository.getListVale(reportes_vo);
            return reportes_repository.getlistSalidaCombustibleReporte(salidaComReporteVo);
        }

        public IList<ReporteDetalleSalidaC> getlistSalidaCombustibleReportePDF(SalidaCombustibleReportePDFVo reportesalidaPDFVo)
        {
            //return reportes_repository.getListVale(reportes_vo);
            return reportes_repository.getlistSalidaCombustibleReportePDF(reportesalidaPDFVo);
        }
    }
}