using System;
using Newtonsoft.Json;

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

        public User(string firstName, string lastName, string email, string phoneNumber, DateTime serviceStartDate, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            ServiceStartDate = serviceStartDate;
            Password = password;
        }

        public User()
        {

        }

        [JsonProperty]
        public string FirstName { get; set; }

        [JsonProperty]
        public string LastName { get; set; }

        [JsonProperty]
        public string Email { get; set; }

        [JsonProperty]
        public string PhoneNumber { get; set; }

        [JsonProperty]
        public DateTime ServiceStartDate { get; set; }

        [JsonProperty]
        public string Password { get; set; }
    }
}
