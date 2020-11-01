using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.ViewModels;
using Spectrum.Test.Core.ViewModels;
using Spectrum.Test.Core.ViewModels.AccountCreation;
using Spectrum.Test.Core.ViewModels.Main;

namespace Spectrum.Test.Droid.Views.NewAccount
{
    [MvxFragmentPresentation(typeof(MainContainerViewModel), Resource.Id.content_frame, true)]
    public class NewAccountFragment : BaseFragment<NewAccountViewModel>
    {
        protected override int FragmentLayoutId => Resource.Layout.fragment_new_account;
        
    }
}
