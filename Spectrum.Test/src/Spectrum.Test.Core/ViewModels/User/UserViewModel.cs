using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Spectrum.Test.Core.Models.Entities;

namespace Spectrum.Test.Core.ViewModels.User
{
    public class UserViewModel : BaseViewModel<IUser>
    {
        private readonly IMvxNavigationService _navigationService;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private string _serviceStartDate;
        private MvxAsyncCommand _viewAllUsersCommand;

        public UserViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        public string ServiceStartDate
        {
            get => _serviceStartDate;
            set => SetProperty(ref _serviceStartDate, value);
        }

        public IMvxCommand ViewAllUsersCommand => _viewAllUsersCommand ??= new MvxAsyncCommand(OnViewAllUsersCommand);

        public override void Prepare(IUser user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            ServiceStartDate = user.ServiceStartDate.Date.ToShortDateString();
        }

        private Task OnViewAllUsersCommand()
        {
            return _navigationService.Navigate<UsersListViewModel>();
        }
    }
}
