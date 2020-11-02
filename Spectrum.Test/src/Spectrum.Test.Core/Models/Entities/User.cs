using System;

namespace Spectrum.Test.Core.Models.Entities
{
    public class User : IUser
    {
        public User(string firstName, string lastName, string email, string phoneNumber, DateTime serviceStartDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            ServiceStartDate = serviceStartDate;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public DateTime ServiceStartDate { get; }
    }
}
