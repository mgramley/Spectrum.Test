using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;

namespace Spectrum.Test.Core.ViewModels.User
{
    public class UsersDirectoryViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public UsersDirectoryViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
