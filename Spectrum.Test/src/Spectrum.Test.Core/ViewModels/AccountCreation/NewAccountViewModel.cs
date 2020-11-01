using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Spectrum.Test.Core.ViewModels.AccountCreation
{
    public class NewAccountViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private MvxAsyncCommand _createAccountCommand;

        public IMvxAsyncCommand CreateAccountCommand => _createAccountCommand ??= new MvxAsyncCommand(OnCreateAccountCommand);

        public NewAccountViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private Task OnCreateAccountCommand()
        {
            return _navigationService.Navigate<AccountCreationSuccessViewModel>();
        }

        public override void ViewDisappearing()
        {
            base.ViewDisappearing();
            //_navigationService.ChangePresentation()
        }
    }
}
