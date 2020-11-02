using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Spectrum.Test.Core.Providers;
using Spectrum.Test.Core.Services;
using Spectrum.Test.Core.ViewModels;
using Spectrum.Test.Core.ViewModels.Main;

namespace Spectrum.Test.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.IoCProvider.RegisterType<ISystemIO, SystemIOWrapper>();
            Mvx.IoCProvider.RegisterType<IAccountService, JsonAccountService>();


            RegisterAppStart<LoginViewModel>();
        }
    }
}
