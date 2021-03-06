﻿using Data.Implementation;
using Data.Interface;
using Ninject;

namespace Business.Helpers
{
    public class NInjectHelper
    {
        public static void SetupKernel(IKernel kernel)
        {
            kernel.Bind<IDummyRepository>().To<DummyRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IAuthenticationRepository>().To<UserRepository>();
            kernel.Bind<IProveedorRepository>().To<ProveedorRepository>();
            kernel.Bind<IEmpleadoRepository>().To<EmpleadoRepository>();
            kernel.Bind<IProductoRepository>().To<ProductoRepository>();
            kernel.Bind<INivelRepository>().To<NivelRepository>();
            kernel.Bind<ISubNivelRepository>().To<SubNivelRepository>();
            kernel.Bind<IProcesoMineroRepository>().To<ProcesoMineroRepository>();
            kernel.Bind<IPresupuestoRepository>().To<PresupuestoRepository>();
            kernel.Bind<ICategoriaRepository>().To<CategoriaRepository>();
            kernel.Bind<ITipoEmpleadoRepository>().To<TipoEmpleadoRepository>();
            kernel.Bind<ITipoProductoRepository>().To<TipoProductoRepository>();
            kernel.Bind<IValeRepository>().To<ValeRepository>();
            kernel.Bind<IDevolucionRepository>().To<DevolucionRepository>();
            kernel.Bind<ICompaniaRepository>().To<CompaniaRepository>();
            kernel.Bind<ICuentaRepository>().To<CuentaRepository>();
            kernel.Bind<ICajaRepository>().To<CajaRepository>();
            kernel.Bind<ISegmentoProductoRepository>().To<SegmentoProductoRepository>();
            kernel.Bind<IInventarioRepository>().To<InventarioRepository>();
            kernel.Bind<IBultoRepository>().To<BultoRepository>();
            kernel.Bind<IReportesRepository>().To<ReportesRepository>();
            kernel.Bind<IOperadorRepository>().To<OperadorRepository>();
            kernel.Bind<IPipaRepository>().To<PipaRepository>();
            kernel.Bind<IMaquinaRepository>().To<MaquinariaRepository>();
            kernel.Bind<ICombustibleRepository>().To<CombustibleRepository>();
            kernel.Bind<ITipoMaquinariaRepository>().To<TipoMaquinariaRepository>();
            kernel.Bind<ISalidaCombustibleRepository>().To<SalidaCombustibleRepository>();
            kernel.Bind<IAbastecimientoRepository>().To<AbastecimientoRepository>();
            kernel.Bind<IFichaEntregaRepository>().To<FichaEntregaRepository>();
            kernel.Bind<IBitacoraDesarrolloRepository>().To<BitacoraDesarrolloRepository>();
            kernel.Bind<IDemoraRepository>().To<DemoraRepository>();
            kernel.Bind<ITipoDesarrolloRepository>().To<TipoDesarrolloRepository>();
            kernel.Bind<IBitacoraBarrenacionRepository>().To<BitacoraBarrenacionRepository>();
        }
    }
}