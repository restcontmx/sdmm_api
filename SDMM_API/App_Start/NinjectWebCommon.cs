﻿[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SDMM_API.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SDMM_API.App_Start.NinjectWebCommon), "Stop")]

namespace SDMM_API.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Business.Helpers;
    using Business.Implementation;
    using Business.Interface;
    using Modules;
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                kernel.Bind<IHttpModule>().To<AuthenticationModule>();
                // Web Api
                System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);

                // MVC 
                System.Web.Mvc.DependencyResolver.SetResolver(new Ninject.Web.Mvc.NinjectDependencyResolver(kernel));
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            NInjectHelper.SetupKernel(kernel);
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IAuthenticationService>().To<UserService>();
            kernel.Bind<IDummyService>().To<DummyService>();
            kernel.Bind<IProveedorService>().To<ProveedorService>();
            kernel.Bind<IEmpleadoService>().To<EmpleadoService>();
            kernel.Bind<IProductoService>().To<ProductoService>();
            kernel.Bind<INivelService>().To<NivelService>();
            kernel.Bind<ISubNivelService>().To<SubNivelService>();
            kernel.Bind<IProcesoMineroService>().To<ProcesoMineroService>();
            kernel.Bind<IPresupuestoService>().To<PresupuestoService>();
            kernel.Bind<ICategoriaService>().To<CategoriaService>();
            kernel.Bind<ITipoEmpleadoService>().To<TipoEmpleadoService>();
            kernel.Bind<ITipoProductoService>().To<TipoProductoService>();
            kernel.Bind<IValeService>().To<ValeService>();
            kernel.Bind<IDevolucionService>().To<DevolucionServide>();
            kernel.Bind<ICompaniaService>().To<CompaniaService>();
            kernel.Bind<ICuentaService>().To<CuentaService>();
            kernel.Bind<ICajaService>().To<CajaService>();
            kernel.Bind<ISegmentoProductoService>().To<SegmentoProductoService>();
            kernel.Bind<IInventarioService>().To<InventarioService>();
            kernel.Bind<IBultoService>().To<BultoService>();
            kernel.Bind<IReportesService>().To<ReportesService>();
            kernel.Bind<IOperadorService>().To<OperadorService>();
            kernel.Bind<IPipaService>().To<PipaService>();
            kernel.Bind<IMaquinariaService>().To<MaquinariaService>();
            kernel.Bind<ICombustibleService>().To<CombustibleService>();
            kernel.Bind<ITipoMaquinariaService>().To<TipoMaquinariaService>();
            kernel.Bind<ISalidaCombustibleService>().To<SalidaCombustibleService>();
            kernel.Bind<IAbastecimientoService>().To<AbastecimientoService>();
            kernel.Bind<IFichaEntregaService>().To<FichaEntregaService>();
            kernel.Bind<IBitacoraDesarrolloService>().To<BitacoraDesarrolloService>();
            kernel.Bind<IDemoraService>().To<DemoraService>();
            kernel.Bind<ITipoDesarrolloService>().To<TipoDesarrolloService>();
            kernel.Bind<IBitacoraBarrenacionService>().To<BitacoraBarrenacionService>();
        }
    }
}
