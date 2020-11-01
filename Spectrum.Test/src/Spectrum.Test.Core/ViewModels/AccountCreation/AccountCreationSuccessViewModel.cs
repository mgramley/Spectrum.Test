using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Spectrum.Test.Core.ViewModels.AccountCreation
{
    public class AccountCreationSuccessViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        private MvxAsyncCommand _loginRetrunCommand;

        public IMvxAsyncCommand LoginReturnCommand => _loginRetrunCommand ??= new MvxAsyncCommand(OnLoginReturnCommand);

        public AccountCreationSuccessViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Prepare()
        {
             base.Prepare();
             //_navigationService.Close()
        }

        private Task OnLoginReturnCommand()
        {
            return _navigationService.Navigate<LoginViewModel>();
        }


        
    }
}
