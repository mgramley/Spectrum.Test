using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Spectrum.Test.Core.Models.Entities;
using Spectrum.Test.Core.Services;
using Spectrum.Test.Core.ViewModels.AccountCreation;
using Spectrum.Test.Core.ViewModels.PresentationHints;
using Spectrum.Test.Core.ViewModels.User;

namespace Spectrum.Test.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAccountService _accountService;
        private MvxAsyncCommand _newUserCommand;
        private MvxAsyncCommand _loginCommand;
        private string _email;
        private string _password;
        private string _errorMessage;

        public IMvxAsyncCommand NewUserCommand => _newUserCommand ??= new MvxAsyncCommand(OnNewUserCommand);
        public IMvxAsyncCommand LoginCommand => _loginCommand ??= new MvxAsyncCommand(OnLoginCommandAsync, CanLogin);


        public LoginViewModel(IMvxNavigationService navigationService, IAccountService accountService)
        {
            _navigationService = navigationService;
            _accountService = accountService;
        }

        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public override void Prepare()
        {
            base.Prepare();
            _navigationService.ChangePresentation(new PopBackStackHint());

        }

        private Task OnNewUserCommand()
        {
            return _navigationService.Navigate<NewAccountViewModel>();
        }

        private bool CanLogin()
        {
            return Email != null && Email.Contains("@") && Password != null && Password.Length >= 8;
        }

        private async Task OnLoginCommandAsync()
        {
            Tuple<LoginResult, IUser> result = await _accountService.Login(Email, Password);

            switch (result.Item1)
            {
                case LoginResult.Success:
                    if (result.Item2 != null)
                    {
                        await _navigationService.Navigate<UserViewModel, IUser>(result.Item2);
                    }
                    break;
                case LoginResult.NoAccount:
                    ErrorMessage = Messages.NoAccountErrorMessage;
                    break;
                case LoginResult.BadPassword:
                    ErrorMessage = Messages.BadPasswordErrorMessage;
                    break;
            }
        }

    }
}
