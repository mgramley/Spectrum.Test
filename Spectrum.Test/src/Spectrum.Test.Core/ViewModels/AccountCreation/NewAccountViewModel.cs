using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Spectrum.Test.Core.Models;
using Spectrum.Test.Core.Models.Entities;
using Spectrum.Test.Core.Services;
using Spectrum.Test.Core.ViewModels.PresentationHints;

namespace Spectrum.Test.Core.ViewModels.AccountCreation
{
    public class NewAccountViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAccountService _accountService;
        private MvxAsyncCommand _createAccountCommand;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private DateTime _serviceStartDate = DateTime.Now.Date;
        private string _serviceStartDateString = DateTime.Now.Date.ToShortDateString();
        private string _password;

        private string _firstNameErrorMessage;
        private string _lastNameErrorMessage;
        private string _emailErrorMessage;
        private string _phoneNumberErrorMessage;
        private string _serviceDateErrorMessage;
        private string _passwordErrorMessage;

        public IMvxAsyncCommand CreateAccountCommand => _createAccountCommand ??= new MvxAsyncCommand(OnCreateAccountCommandAsync, CanCreateAccount);

        /// <summary>
        /// Indicates that errors are being corrected. Set after an attempt to create an account is made. This way, we don't populate blank text fields with errors
        /// </summary>
        public bool IsCorrectingMistakes { get; protected set; }

        public NewAccountViewModel(IMvxNavigationService navigationService, IAccountService accountService)
        {
            _navigationService = navigationService;
            _accountService = accountService;
        }

        #region User Properties

        public string FirstName
        {
            get => _firstName;
            set
            {
                SetProperty(ref _firstName, value);
                CreateAccountCommand.RaiseCanExecuteChanged();
                if (IsCorrectingMistakes)
                {
                    IsFirstNameValid();
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                SetProperty(ref _lastName, value);
                CreateAccountCommand.RaiseCanExecuteChanged();
                if (IsCorrectingMistakes)
                {
                    IsLastNameValid();
                }
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value);
                CreateAccountCommand.RaiseCanExecuteChanged();
                if (IsCorrectingMistakes)
                {
                    IsEmailValid();
                }
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                SetProperty(ref _phoneNumber, value);
                CreateAccountCommand.RaiseCanExecuteChanged();
                if (IsCorrectingMistakes)
                {

                }
            }
        }

        public string ServiceStartDateString
        {
            get => _serviceStartDateString;
            set
            {
                SetProperty(ref _serviceStartDateString, value);
                _serviceStartDate = Convert.ToDateTime(_serviceStartDateString);
                CreateAccountCommand.RaiseCanExecuteChanged();
                if (IsCorrectingMistakes)
                {
                    IsServiceStartDateValid();
                    //RaisePropertyChanged(nameof(ServiceDateErrorMessage));
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                CreateAccountCommand.RaiseCanExecuteChanged();
                if (IsCorrectingMistakes)
                {
                    IsPasswordValid();
                    //RaisePropertyChanged(nameof(PasswordErrorMessage));
                }
            }
        }

        #endregion

        #region ValidationMessageProperties

        public string FirstNameErrorMessage
        {
            get => _firstNameErrorMessage;
            set => SetProperty(ref _firstNameErrorMessage, value);
        }

        public string LastNameErrorMessage
        {
            get => _lastNameErrorMessage;
            set => SetProperty(ref _lastNameErrorMessage, value);
        }

        public string EmailErrorMessage
        {
            get => _emailErrorMessage;
            set => SetProperty(ref _emailErrorMessage, value);
        }

        public string PhoneNumberErrorMessage
        {
            get => _phoneNumberErrorMessage;
            set => SetProperty(ref _phoneNumberErrorMessage, value);
        }

        public string ServiceDateErrorMessage
        {
            get => _serviceDateErrorMessage;
            set => SetProperty(ref _serviceDateErrorMessage, value);
        }

        public string PasswordErrorMessage
        {
            get => _passwordErrorMessage;
            set => SetProperty(ref _passwordErrorMessage, value);
        }

        #endregion

        private async Task OnCreateAccountCommandAsync()
        {
            var areAccountDetailsValid = await IsValidAccountDetails();
            if (!areAccountDetailsValid)
            {
                IsCorrectingMistakes = true;
                return;
            }

            IsCorrectingMistakes = false;
            var result = await _accountService.CreateAccountAsync(FirstName, LastName, Email, Password, PhoneNumber,
                _serviceStartDate);
            if(result.Success == false) return;
           await _navigationService.Navigate<AccountCreationSuccessViewModel, IUser>(result.Result);
        }

        #region AccountValidation

        private bool CanCreateAccount()
        {
            return !string.IsNullOrWhiteSpace(FirstName)
                   && FirstName.Length > 1
                   && !string.IsNullOrWhiteSpace(LastName)
                   && LastName.Length > 1
                   && !string.IsNullOrWhiteSpace(Email)
                   && Email.Length > 1
                   && !string.IsNullOrWhiteSpace(PhoneNumber)
                   && PhoneNumber.Length >= 10
                   && !string.IsNullOrWhiteSpace(Password)
                   && Password.Length >= 8
                   && !string.IsNullOrWhiteSpace(ServiceStartDateString);
        }

        private async Task<bool> IsValidAccountDetails()
        {
            // call each validator method separately, otherwise only the first error message will get set.
           var isEmailValid = await IsEmailValid();
           var isFirstValid= IsFirstNameValid();
           var isLastValid = IsLastNameValid();
           var isPassValid = IsPasswordValid();
           var isStartValid = IsServiceStartDateValid();

           return isEmailValid && isFirstValid && isLastValid && isPassValid && isStartValid;
        }

        private async Task<bool> IsEmailValid()
        {
            if (!AccountValidation.IsValidEmail(Email))
            {
                EmailErrorMessage = null;
                EmailErrorMessage = Messages.EmailErrorMessage;
                return false;
            }

            var isEmailRegistered = await _accountService.IsEmailRegisteredAsync(Email);
            if (isEmailRegistered)
            {
                EmailErrorMessage = null;
                EmailErrorMessage = Messages.DuplicateEmailErrorMessage;
                return false;
            }

            EmailErrorMessage = null;
            return true;
        }

        private bool IsPasswordValid()
        {
            if (!AccountValidation.IsValidPasswordForCreation(Password))
            {
                PasswordErrorMessage = null;
                PasswordErrorMessage = Messages.PasswordErrorMessage;
                return false;
            }
            else
            {
                PasswordErrorMessage = null;
                return true;
            }
        }

        private bool IsFirstNameValid()
        {
            if (!AccountValidation.IsValidName(FirstName))
            {
                FirstNameErrorMessage = null;
                FirstNameErrorMessage = Messages.NamesErrorMessage;
                return false;
            }
            else
            {
                FirstNameErrorMessage = null;
                return true;
            }
        }

        private bool IsLastNameValid()
        {
            if (!AccountValidation.IsValidName(LastName))
            {
                LastNameErrorMessage = null;
                LastNameErrorMessage = Messages.NamesErrorMessage;
                return false;
            }
            else
            {
                LastNameErrorMessage = null;
                return true;
            }
        }

        private bool IsServiceStartDateValid()
        {
            if (!AccountValidation.IsValidStartDate(_serviceStartDate))
            {
                ServiceDateErrorMessage = null;
                ServiceDateErrorMessage = Messages.ServiceDateErrorMessage;
                return false;
            }
            else
            {
                ServiceDateErrorMessage = null;
                return true;
            }
        }

        #endregion
    }
}
