using System;
using System.Text.RegularExpressions;

namespace Spectrum.Test.Core.Models
{
    public static class AccountValidation
    {
        private const string EmailPattern = @"\b[\w\.-]+@[\w\.-]+\.\w{2,4}\b";

        // 1 Upper, 1 Lower, 8-15 chars
        private const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,15}$";

        private const string SpecialCharactersPattern = @"[`!@#$%^&]";

        private const string PhoneNumberPattern = @"^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$";
        private const string OnlyNumbers = @"^[^a-z]*[0-9]+[^a-z]*$";

        /// <summary>
        /// Tests whether an email is in the correct form
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && email.Length > 4 && Regex.IsMatch(email, EmailPattern);
        }

        /// <summary>
        /// Tests if the password meets the minimum length requirements to attempt a login
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsValidPasswordForLogin(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 8 && password.Length <= 15;
        }


        /// <summary>
        /// Tests whether a password passes all validation rules for account creation
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsValidPasswordForCreation(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && Regex.IsMatch(password, PasswordPattern) && HasNoRepeatingChars(password);
        }

        public static bool IsValidStartDate(DateTime startDate)
        {
            return startDate.Date >= DateTime.Now.Date && startDate.Date <= DateTime.Now.Date.AddDays(30);
        }

        public static bool IsValidName(string name)
        {
            return !Regex.IsMatch(name, SpecialCharactersPattern);
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length <= 14 &&
                   Regex.IsMatch(phoneNumber, PhoneNumberPattern); //&& Regex.IsMatch(phoneNumber, OnlyNumbers);
        }

        private static bool HasNoRepeatingChars(string password)
        {
            bool hasNoRepeats = true;

            for (var seqStart = 0; seqStart < password.Length; seqStart++)
            {
                for (var seqLength = 2; seqLength < password.Length / 2; seqLength++)
                {
                    if(seqStart + seqLength > password.Length) break;
                    var sequence = password.Substring(seqStart, seqLength);

                    var prefix = "{2,}.*";
                    var patternToCheck = @$"^(.*{sequence}){prefix}";
                    if (Regex.IsMatch(password, patternToCheck))
                        hasNoRepeats = false;
                }
            }

            return hasNoRepeats;
        }
    }
}
