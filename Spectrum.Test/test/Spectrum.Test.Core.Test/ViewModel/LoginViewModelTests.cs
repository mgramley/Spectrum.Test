using System;
using MvvmCross.Navigation;
using NSubstitute;
using NUnit.Framework;
using Spectrum.Test.Core.Models.Entities;
using Spectrum.Test.Core.Services;
using Spectrum.Test.Core.ViewModels;
using Spectrum.Test.Core.ViewModels.AccountCreation;
using Spectrum.Test.Core.ViewModels.PresentationHints;
using Spectrum.Test.Core.ViewModels.User;

namespace Spectrum.Test.Core.Test.ViewModel
{
    [TestFixture]
    public class LoginViewModelTests
    {
        private LoginViewModel _sutLoginViewModel;
        private IMvxNavigationService _navigationService;
        private IAccountService _accountService;
        private string _validEmail = "user@domain.com";
        private string _validPassword = "ValidPassword";

        [SetUp]
        public void Setup()
        {
            _navigationService = Substitute.For<IMvxNavigationService>();
            _accountService = Substitute.For<IAccountService>();

            _sutLoginViewModel = new LoginViewModel(_navigationService, _accountService);
        }

        [Test]
        public void PrepareTest()
        {
            var sut = new LoginViewModel(_navigationService, _accountService);
            sut.Prepare();
            _navigationService.Received(1).ChangePresentation(Arg.Any<PopBackStackHint>());
        }

        [Test]
        public void CanLoginValidCredTest()
        {
            Assert.IsFalse(_sutLoginViewModel.LoginCommand.CanExecute());

            _sutLoginViewModel.Email = _validEmail;

            Assert.IsFalse(_sutLoginViewModel.LoginCommand.CanExecute());

            _sutLoginViewModel.Password = _validPassword;

            Assert.IsTrue(_sutLoginViewModel.LoginCommand.CanExecute());
        }

        [Test]
        public void CanLoginInValidCredTest()
        {
            Assert.IsFalse(_sutLoginViewModel.LoginCommand.CanExecute());
            _sutLoginViewModel.Password = _validPassword;
            _sutLoginViewModel.Email = "badEmail";

            Assert.IsFalse(_sutLoginViewModel.LoginCommand.CanExecute());

            _sutLoginViewModel.Password = "123";
            _sutLoginViewModel.Email = _validEmail;

            Assert.IsFalse(_sutLoginViewModel.LoginCommand.CanExecute());
        }

        [Test]
        public void NewUserCommandTest()
        {
            _sutLoginViewModel.NewUserCommand.Execute();

            _navigationService.Received(1).Navigate<NewAccountViewModel>();
        }

        [Test]
        public void LoginSuccessCommandTest()
        {
            var user = Substitute.For<IUser>();
            _accountService.LoginAsync(_validEmail, _validPassword)
                .Returns(new Tuple<LoginResult, IUser>(LoginResult.Success, user));

            _sutLoginViewModel.Email = _validEmail;
            _sutLoginViewModel.Password = _validPassword;
            _sutLoginViewModel.LoginCommand.Execute();

            _navigationService.Received(1).Navigate<UserViewModel, IUser>(user);
        }

        [Test]
        public void LoginSuccessBadUserCommandTest()
        {
            var user = Substitute.For<IUser>();
            _accountService.LoginAsync(_validEmail, _validPassword)
                .Returns(new Tuple<LoginResult, IUser>(LoginResult.Success, null));

            _sutLoginViewModel.Email = _validEmail;
            _sutLoginViewModel.Password = _validPassword;
            _sutLoginViewModel.LoginCommand.Execute();

            _navigationService.DidNotReceive().Navigate<UserViewModel, IUser>(Arg.Any<IUser>());
        }

        [Test]
        public void LoginNoAccountCommandTest()
        {
            _accountService.LoginAsync(_validEmail, _validPassword)
                .Returns(new Tuple<LoginResult, IUser>(LoginResult.NoAccount, null));

            _sutLoginViewModel.Email = _validEmail;
            _sutLoginViewModel.Password = _validPassword;
            _sutLoginViewModel.LoginCommand.Execute();

            _navigationService.DidNotReceive().Navigate<UserViewModel, IUser>(Arg.Any<IUser>());
            Assert.AreEqual(Messages.NoAccountErrorMessage, _sutLoginViewModel.ErrorMessage);
        }

        [Test]
        public void LoginBadPasswordCommandTest()
        {
            _accountService.LoginAsync(_validEmail, _validPassword)
                .Returns(new Tuple<LoginResult, IUser>(LoginResult.BadPassword, null));

            _sutLoginViewModel.Email = _validEmail;
            _sutLoginViewModel.Password = _validPassword;
            _sutLoginViewModel.LoginCommand.Execute();

            _navigationService.DidNotReceive().Navigate<UserViewModel, IUser>(Arg.Any<IUser>());
            Assert.AreEqual(Messages.BadPasswordErrorMessage, _sutLoginViewModel.ErrorMessage);
        }
    }
}
