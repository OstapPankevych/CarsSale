using CarsSale.DataAccess.Services.Interfaces;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User CreateUser(User user)
        {
            return _userRepository.Add(user);
        }

        public User Get(string userName)
        {
            return _userRepository.Get(x => x.LOGIN == userName);
        }
    }
}
