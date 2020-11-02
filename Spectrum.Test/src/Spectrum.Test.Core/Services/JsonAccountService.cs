using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spectrum.Test.Core.Models.Entities;
using Spectrum.Test.Core.Providers;

namespace Spectrum.Test.Core.Services
{
    public class JsonAccountService : IAccountService
    {
        private readonly IFileProvider _fileProvider;
        protected const string Ext = ".json";
        protected const string UsersList = "UsersList";
        //protected const string CurrentUserKey = "CurrentUser";
        //protected const string UserPrefix = "User-";

        public JsonAccountService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
        public async Task<AccountCreationResult> CreateAccountAsync(string firstName, string lastName, string email, string password, string phoneNumber,
            DateTime serviceStartDate)
        {
            try
            {
                var newUser = new User(firstName, lastName, email, phoneNumber, serviceStartDate, password);
                var userList = await GetAllUsersAsync();
                userList.Add(newUser);

                var usersToUpdate = new List<User>();
                userList.ForEach(u => usersToUpdate.Add((User)u));
                await UpdateUserListAsync(usersToUpdate);

                return new AccountCreationResult(true, newUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new AccountCreationResult(false, null);
            }
            
        }

        public async Task<Tuple<LoginResult, IUser>> LoginAsync(string email, string password)
        {
            var users = await GetAllUsersAsync();

            var loginUser = (User)users.FirstOrDefault(u => u.Email == email);

            if (loginUser == null)
            {
                return new Tuple<LoginResult, IUser>(LoginResult.NoAccount, null);
            }

            if (password != loginUser.Password)
            {
                return new Tuple<LoginResult, IUser>(LoginResult.BadPassword, null);
            }

            return new Tuple<LoginResult, IUser>(LoginResult.Success, loginUser);
        }

        public async Task<List<IUser>> GetAllUsersAsync()
        {
            var fileName = $"{UsersList}{Ext}";
            var users = await _fileProvider.GetFile<List<User>>(fileName);
            var interfaceUsers = new List<IUser>();

            users?.ForEach(u => interfaceUsers.Add(u));

            return interfaceUsers;
        }

        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            var allUsers = await GetAllUsersAsync();

            var alreadyRegistered = allUsers.Any(u => u.Email == email);

            return alreadyRegistered;
        }

        private async Task UpdateUserListAsync(List<User> users)
        {
            var fileName = $"{UsersList}{Ext}";

            await _fileProvider.WriteFile(fileName, users);
        }
    }
}
