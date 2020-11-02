using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using NSubstitute;
using NUnit.Framework;
using Spectrum.Test.Core.Models.Entities;
using Spectrum.Test.Core.Services;
using Spectrum.Test.Core.ViewModels;
using Spectrum.Test.Core.ViewModels.AccountCreation;

namespace Spectrum.Test.Core.Test.ViewModel
{
    [TestFixture]
    public class NewAccountViewModelTests
    {
        private IMvxNavigationService _navigationService;
        private IAccountService _accountService;

        private NewAccountViewModel _sutNewAccountViewModel;

        [SetUp]
        public void Setup()
        {
            _navigationService = Substitute.For<IMvxNavigationService>();
            _accountService = Substitute.For<IAccountService>();

            _sutNewAccountViewModel = new NewAccountViewModel(_navigationService, _accountService);
        }

        [Test]
        public void CanAttemptToCreateAccountTest()
        {
            Assert.IsFalse(_sutNewAccountViewModel.CreateAccountCommand.CanExecute());
            _sutNewAccountViewModel.Email = "test@domain.com";
            _sutNewAccountViewModel.FirstName = "John";
            _sutNewAccountViewModel.LastName = "Doe";
            _sutNewAccountViewModel.Password = "12345678";
            _sutNewAccountViewModel.ServiceStartDateString = DateTime.Now.Date.ToShortDateString();
            _sutNewAccountViewModel.PhoneNumber = "1112223333";
            Assert.IsTrue(_sutNewAccountViewModel.CreateAccountCommand.CanExecute());
        }

        [Test]
        public void CreateAccountErrorsTest()
        {
            Assert.IsFalse(_sutNewAccountViewModel.CreateAccountCommand.CanExecute());
            _sutNewAccountViewModel.Email = "test@d";
            _sutNewAccountViewModel.FirstName = "John!";
            _sutNewAccountViewModel.LastName = "Doe!";
            _sutNewAccountViewModel.Password = "testtest";
            _sutNewAccountViewModel.ServiceStartDateString = DateTime.Now.Date.AddDays(32).ToShortDateString();
            _sutNewAccountViewModel.PhoneNumber = "1112223333";
            Assert.IsTrue(_sutNewAccountViewModel.CreateAccountCommand.CanExecute());

            _sutNewAccountViewModel.CreateAccountCommand.Execute();

            _accountService.DidNotReceive().CreateAccountAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<DateTime>());
            Assert.IsTrue(_sutNewAccountViewModel.IsCorrectingMistakes);

            Assert.AreEqual(Messages.EmailErrorMessage, _sutNewAccountViewModel.EmailErrorMessage);
            Assert.AreEqual(Messages.PasswordErrorMessage, _sutNewAccountViewModel.PasswordErrorMessage);
            Assert.AreEqual(Messages.ServiceDateErrorMessage, _sutNewAccountViewModel.ServiceDateErrorMessage);
            Assert.AreEqual(Messages.NamesErrorMessage, _sutNewAccountViewModel.LastNameErrorMessage);
            Assert.AreEqual(Messages.NamesErrorMessage, _sutNewAccountViewModel.FirstNameErrorMessage);

            _sutNewAccountViewModel.Email = "JohnDoe@gmail.com";
            _accountService.IsEmailRegisteredAsync("JohnDoe@gmail.com").Returns(true);
            _sutNewAccountViewModel.CreateAccountCommand.Execute();

            Assert.AreEqual(Messages.DuplicateEmailErrorMessage, _sutNewAccountViewModel.EmailErrorMessage);
        }

        [Test]
        public void CreateAccountSuccessTest()
        {
            
            Assert.IsFalse(_sutNewAccountViewModel.CreateAccountCommand.CanExecute());
            _sutNewAccountViewModel.Email = "JohnDoe@gmail.com";
            _sutNewAccountViewModel.FirstName = "John";
            _sutNewAccountViewModel.LastName = "Doe";
            _sutNewAccountViewModel.Password = "Test1234";
            _sutNewAccountViewModel.ServiceStartDateString = DateTime.Now.Date.ToShortDateString();
            _sutNewAccountViewModel.PhoneNumber = "111-222-3333";
            Assert.IsTrue(_sutNewAccountViewModel.CreateAccountCommand.CanExecute());

            _accountService.CreateAccountAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<DateTime>()).Returns(new AccountCreationResult(true, Substitute.For<IUser>()));

            _sutNewAccountViewModel.CreateAccountCommand.Execute();

            _accountService.Received(1).CreateAccountAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<DateTime>());

            _navigationService.Received(1).Navigate<AccountCreationSuccessViewModel, IUser>(Arg.Any<IUser>());

        }
    }
}
