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
    }
}
