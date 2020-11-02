using System;
using NUnit.Framework;
using Spectrum.Test.Core.Models;

namespace Spectrum.Test.Core.Test
{
    [TestFixture]
    public class AccountValidationTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void IsValidEmailTest()
        {
            var email1Success = "JohnDoe@gmail.com";
            var email2Fail = "JohnDoe@gmail";
            var email3Fail = "JohnDoe.com";
            var email4Fail = "john";

            Assert.IsTrue(AccountValidation.IsValidEmail(email1Success));
            Assert.IsFalse(AccountValidation.IsValidEmail(email2Fail));
            Assert.IsFalse(AccountValidation.IsValidEmail(email3Fail));
            Assert.IsFalse(AccountValidation.IsValidEmail(email4Fail));
        }

        [Test]
        public void IsValidPasswordForCreationTest()
        {
            // 8 characters, min 1 upper and 1 lower. Should pass
            var pass8charsCorrectForm = "acdefgHi";
            Assert.IsTrue(AccountValidation.IsValidPasswordForCreation(pass8charsCorrectForm));

            // 8 characters, min 1 upper and 1 lower. Should pass
            var secondPass = "ThisIsATest";
            Assert.IsTrue(AccountValidation.IsValidPasswordForCreation(secondPass));

            //Pass meets character requirements but is too short (7chars), should fail
            var passToShort = "Abcdfte";
            Assert.IsFalse(AccountValidation.IsValidPasswordForCreation(passToShort));

            //Pass meets character requirements but is too long (16 chars), should fail
            var passToLong = "Abcdtfghkdlsherh";
            Assert.IsFalse(AccountValidation.IsValidPasswordForCreation(passToLong));

            //Pass fails character requirements (no uppers), should fail
            var passFailChars = "abcdefghi";
            Assert.IsFalse(AccountValidation.IsValidPasswordForCreation(passFailChars));

            //Pass fails character requirements (no lower), should fail
            var passFailCharsLow = "ABCDEFGHI";
            Assert.IsFalse(AccountValidation.IsValidPasswordForCreation(passFailCharsLow));

            //pass fails because of 2 repetitive chars
            var passRepeat2 = "ababTEST";
            Assert.IsFalse(AccountValidation.IsValidPasswordForCreation(passRepeat2));

            //pass fails because of 3 repetitive chars
            var passRepeat3 = "abcabcTEST";
            Assert.IsFalse(AccountValidation.IsValidPasswordForCreation(passRepeat3));

            //pass fails because of 4 repetitive chars
            var passRepeat4 = "12341234Test";
            Assert.IsFalse(AccountValidation.IsValidPasswordForCreation(passRepeat4));

            //pass fails because of 4 repetitive chars
            var scatteredPatternFail = "1234Test1234";
            Assert.IsFalse(AccountValidation.IsValidPasswordForCreation(scatteredPatternFail));

            string nullPass = null;
            Assert.IsFalse(AccountValidation.IsValidPasswordForCreation(nullPass));

            string emptyPass = string.Empty;
            Assert.IsFalse(AccountValidation.IsValidPasswordForCreation(emptyPass));
        }

        [Test]
        public void IsValidPasswordForLoginTest()
        {
            string nullPass = null;
            Assert.IsFalse(AccountValidation.IsValidPasswordForLogin(nullPass));

            string emptyPass = string.Empty;
            Assert.IsFalse(AccountValidation.IsValidPasswordForLogin(emptyPass));

            //Pass is too short (7chars), should fail
            var passToShort = "abcdfte";
            Assert.IsFalse(AccountValidation.IsValidPasswordForLogin(passToShort));

            //Pass  is too long (16 chars), should fail
            var passToLong = "abcdtfghkdlsherh";
            Assert.IsFalse(AccountValidation.IsValidPasswordForLogin(passToLong));

            // 8 character, Should pass
            var pass8charsCorrectForm = "acdefghi";

            Assert.IsTrue(AccountValidation.IsValidPasswordForLogin(pass8charsCorrectForm));
        }

        [Test]
        public void ValidNamesTest()
        {
            string name1 = "John";
            Assert.IsTrue(AccountValidation.IsValidName(name1));

            string name2 = "John!";
            Assert.IsFalse(AccountValidation.IsValidName(name2));

            string name3 = "John@";
            Assert.IsFalse(AccountValidation.IsValidName(name3));

            string name4 = "John#";
            Assert.IsFalse(AccountValidation.IsValidName(name4));

            string name5 = "John$";
            Assert.IsFalse(AccountValidation.IsValidName(name5));

            string name6 = "John%";
            Assert.IsFalse(AccountValidation.IsValidName(name6));

            string name7 = "John^";
            Assert.IsFalse(AccountValidation.IsValidName(name7));

            string name8 = "John&";
            Assert.IsFalse(AccountValidation.IsValidName(name8));
        }

        [Test]
        public void ValidStartDateTest()
        {
            var tommorrow = DateTime.Now.Date.AddDays(1);
            Assert.IsTrue(AccountValidation.IsValidStartDate(tommorrow));

            var yesterday = DateTime.Now.Date.AddDays(-1);
            Assert.IsFalse(AccountValidation.IsValidStartDate(yesterday));

            var month = DateTime.Now.Date.AddDays(30);
            Assert.IsTrue(AccountValidation.IsValidStartDate(month));

            var monthPlus = DateTime.Now.Date.AddDays(31);
            Assert.IsFalse(AccountValidation.IsValidStartDate(monthPlus));
        }

        [Test]
        public void ValidPhoneNumberTest()
        {
            var phone1 = "650-555-2345";
            var phone2 = "4155551234";
            var phone3 = "(415) 555-1234";
            var phone4 = "(415)-555-1234";
            var phone5 = "415 555 1234";

            Assert.IsTrue(AccountValidation.IsValidPhoneNumber(phone1));
            Assert.IsTrue(AccountValidation.IsValidPhoneNumber(phone2));
            Assert.IsTrue(AccountValidation.IsValidPhoneNumber(phone3));
            Assert.IsTrue(AccountValidation.IsValidPhoneNumber(phone4));
            Assert.IsTrue(AccountValidation.IsValidPhoneNumber(phone5));

            var phone6 = "1112223333435252562";
            var phone7 = "4155551234aasgag";
            var phone8 = "abc";
            var phone9 = "1112223333abc";
            var phone10 = "10";
            var phone11 = "10   111  1";
            var phone12 = "111222333444";

            Assert.IsFalse(AccountValidation.IsValidPhoneNumber(phone6));
            Assert.IsFalse(AccountValidation.IsValidPhoneNumber(phone7));
            Assert.IsFalse(AccountValidation.IsValidPhoneNumber(phone8));
            Assert.IsFalse(AccountValidation.IsValidPhoneNumber(phone9));
            Assert.IsFalse(AccountValidation.IsValidPhoneNumber(phone10));
            Assert.IsFalse(AccountValidation.IsValidPhoneNumber(phone11));
            Assert.IsFalse(AccountValidation.IsValidPhoneNumber(phone12));
        }
    }
}
