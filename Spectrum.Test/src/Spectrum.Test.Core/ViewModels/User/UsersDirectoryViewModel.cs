using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MvvmCross.Navigation;
using Spectrum.Test.Core.Services;

namespace Spectrum.Test.Core.ViewModels.User
{
    public class UsersDirectoryViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAccountService _accountService;
        private ObservableCollection<UserViewModel> _userDirectory = new ObservableCollection<UserViewModel>();

        public UsersDirectoryViewModel(IMvxNavigationService navigationService, IAccountService accountService)
        {
            _navigationService = navigationService;
            _accountService = accountService;
        }

        public override async void Prepare()
        {
            base.Prepare();

            var users = await _accountService.GetAllUsersAsync();

            users?.ForEach(u =>
            {
                var vm = new UserViewModel(_navigationService);
                vm.Prepare(u);
                UserDirectory.Add(vm);
            });
        }

        public ObservableCollection<UserViewModel> UserDirectory
        {
            get => _userDirectory;
            set => SetProperty(ref _userDirectory, value);
        }
    }
}
