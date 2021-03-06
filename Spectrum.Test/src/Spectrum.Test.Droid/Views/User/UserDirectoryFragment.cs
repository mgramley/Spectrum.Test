using MvvmCross.Platforms.Android.Presenters.Attributes;
using Spectrum.Test.Core.ViewModels.Main;
using Spectrum.Test.Core.ViewModels.User;

namespace Spectrum.Test.Droid.Views.User
{
    [MvxFragmentPresentation(typeof(MainContainerViewModel), Resource.Id.content_frame, true)]
    public class UserDirectoryFragment : BaseFragment<UsersDirectoryViewModel>
    {
        protected override int FragmentLayoutId => Resource.Layout.fragment_directory;
    }
}
