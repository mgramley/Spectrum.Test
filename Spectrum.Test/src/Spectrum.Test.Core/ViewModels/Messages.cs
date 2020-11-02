using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrum.Test.Core.ViewModels
{
    public static class Messages
    {
        public static string NoAccountErrorMessage = "This account doesn't exist, please try again.";
        public static string BadPasswordErrorMessage = "This password doesn't match the given user, please try again.";

        public static string EmailErrorMessage = "Email address must be in the form of user@domain.com";
        public static string DuplicateEmailErrorMessage = "This email address has already been registered";
        public static string PasswordErrorMessage = "Password must be 8-15 characters, must contain 1 uppercase and 1 lowercase letter, and may not contain any repeating sequences such as AbcAbc or 12341234";
        public static string NamesErrorMessage = "Names may not any of these special characters: !@#$%^&";
        public static string ServiceDateErrorMessage = "The service start date may not be a past date, nor any date after 30 days from the present date";
        public static string PhoneNumberErrorMessage = "The phone number must be in the form (XXX)-XXX-XXXX";
    }
}
