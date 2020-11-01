using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Spectrum.Test.Core.ViewModels.Main
{
    public class MainContainerViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private IMvxAsyncCommand _showLoginCommand;

        public IMvxAsyncCommand ShowLoginCommand => _showLoginCommand ??= new MvxAsyncCommand(NavigateToLogin);

        public MainContainerViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private Task NavigateToLogin()
        {
            return _navigationService.Navigate<LoginViewModel>();
        }
    }
}
