using MvvmCross.IoC;
using MvvmCross.ViewModels;
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

            RegisterAppStart<MainViewModel>();
        }
    }
}
