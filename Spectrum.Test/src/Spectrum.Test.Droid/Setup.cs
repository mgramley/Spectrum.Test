using MvvmCross;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Presenters;
using Spectrum.Test.Core;
using Spectrum.Test.Core.Services;
using Spectrum.Test.Droid.Services;

namespace Spectrum.Test.Droid
{
    public class Setup : MvxAndroidSetup<App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            Mvx.IoCProvider.RegisterSingleton<IAccountService>( new AndroidAccountService());
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            //var presenter = base.CreateViewPresenter();
            //return presenter;

            return new CustomPresenter(AndroidViewAssemblies);
        }
    }
}
