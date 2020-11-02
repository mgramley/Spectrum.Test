using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Spectrum.Test.Core.Models.Entities;
using Spectrum.Test.Core.ViewModels.PresentationHints;

namespace Spectrum.Test.Core.ViewModels.AccountCreation
{
    public class AccountCreationSuccessViewModel : BaseViewModel<IUser>
    {
        private readonly IMvxNavigationService _navigationService;
        private IUser _user;

        private MvxAsyncCommand _loginRetrunCommand;
        private string _firstName;

        public IMvxAsyncCommand LoginReturnCommand => _loginRetrunCommand ??= new MvxAsyncCommand(OnLoginReturnCommand);

        public AccountCreationSuccessViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public override void Prepare(IUser user)
        {
            _user = user;
            FirstName = _user.FirstName;
        }

        public override void Prepare()
        {
             base.Prepare();
            _navigationService.ChangePresentation(new PopBackStackHint());

        }

        private Task OnLoginReturnCommand()
        {
            return _navigationService.Navigate<LoginViewModel>();
        }


        
    }
}
