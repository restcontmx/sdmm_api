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

        public IList<Vale> getListaVale(ReportesVo reportes_vo)
        {
            return reportes_repository.getListaVale(reportes_vo);
        }
    }
}