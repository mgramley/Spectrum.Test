using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;

namespace Spectrum.Test.Core.ViewModels.User
{
    public class UsersListViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public UsersListViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
