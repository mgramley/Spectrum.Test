using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Spectrum.Test.Core.ViewModels.AccountCreation;

namespace Spectrum.Test.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private MvxAsyncCommand _newUserCommand;

        public IMvxAsyncCommand NewUserCommand => _newUserCommand ??= new MvxAsyncCommand(OnNewUserCommand);

        public LoginViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private Task OnNewUserCommand()
        {
            return _navigationService.Navigate<NewAccountViewModel>();
        }
    }
}
