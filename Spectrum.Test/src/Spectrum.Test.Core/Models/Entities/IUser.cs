using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrum.Test.Core.Models.Entities
{
    public interface IUser
    {
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        string PhoneNumber { get; }
        DateTime ServiceStartDate { get; }
    }
}
