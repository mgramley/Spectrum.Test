using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
