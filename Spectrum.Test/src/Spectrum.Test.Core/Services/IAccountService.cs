using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Spectrum.Test.Core.Models.Entities;

namespace Spectrum.Test.Core.Services
{
    public interface IAccountService 
    {
        Task<AccountCreationResult> CreateAccount(string firstName, string lastName, string email, string password,
            string phoneNumber, DateTime serviceStartDate);

        Task<Tuple<LoginResult, IUser>> Login(string email, string password);

        Task<IList<IUser>> GetAllUsers();
    }

    public class AccountCreationResult
    {
        public bool Success { get; }
        public IUser Result { get; }

        public AccountCreationResult(bool success, IUser result)
        {
            Success = success;
            Result = result;
        }
    }

    public enum LoginResult
    {
        Success,
        NoAccount,
        BadPassword
    }
}
