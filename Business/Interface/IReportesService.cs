using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IReportesService
    {
        IList<RegistroDetalle> getListaSedena(ReportesVo reportesVo);
        IList<ReporteAccPac> getListVale(ReportesVo reportesVo);

        IList<ReporteDetalleSalidaC> getlistSalidaCombustibleReporte(SalidaCombustibleReporteVo salidaComReporteVo);
        IList<ReporteDetalleSalidaC> getlistSalidaCombustibleReportePDF(SalidaCombustibleReportePDFVo reportesalidaPDFVo);

        //*** REPORTES BOTACORAS **///
        IList<ReporteJumbo> getListaReporteJumbo(ReportesVo reportesVo);
        IList<ReporteJumbo> getListaReporteAnclador(ReportesVo reportesVo);
        IList<ReporteJumboSolo> getListaReporteJumboSolos(ReportesVo reportesVo);
    }
}
