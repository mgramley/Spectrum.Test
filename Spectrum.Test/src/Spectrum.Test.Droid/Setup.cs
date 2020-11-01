using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Presenters;
using Spectrum.Test.Core;

namespace Spectrum.Test.Droid
{
    public class Setup : MvxAndroidSetup<App>
    {
        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            //var presenter = base.CreateViewPresenter();
            //return presenter;

            return new CustomPresenter(AndroidViewAssemblies);
        }
    }
}
