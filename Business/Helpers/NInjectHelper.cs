using Data.Implementation;
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
        }
    }
}