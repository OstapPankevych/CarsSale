using System.Security.Cryptography;
using System.Text;
using CarsSale.DataAccess.Services.Interfaces;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Services
{
    public class UserService : IUserService
    {
        private const string UserRole = "user";

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public User CreateUser(User user)
        {
            user.Password = GetCrypto(user.Password);
            user.Role = _roleRepository.GetRoleByName(UserRole.ToUpper());
            return _userRepository.Add(user);
        }

        public User Get(string login)
        {
            return _userRepository.Get(x => x.LOGIN == login);
        }

        public bool IsUserValid(string login, string password)
        {
            var dbUser = _userRepository.Get(x => x.LOGIN == login);
            return dbUser?.Password.Equals(GetCrypto(password)) ?? false;
        }

        #region Helpers

        private string GetCrypto(string password)
        {
            using (var crypto = MD5.Create())
            {
                var bytes = crypto.ComputeHash(Encoding.Unicode.GetBytes(password));
                var cryptoPass = new StringBuilder();
                foreach (var symbol in bytes)
                {
                    cryptoPass.Append(symbol.ToString("x2"));
                }
                return cryptoPass.ToString();
            }
        }

        #endregion
    }
}
