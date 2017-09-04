using CarsSale.DataAccess.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Services
{
    public class UserService : Service, IUserService
    {
        private const string UserRole = "user";

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(CarsSaleEntities dbContext,
            IUserRepository userRepository,
            IRoleRepository roleRepository)
            : base(dbContext)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        
        public IEnumerable<Role> GetRoles()
        {
            return _roleRepository.GetAll()
                .AsEnumerable()
                .Select(x => new Role(x));
        }

        public User GetUser(int userId)
        {
            var user = _userRepository.Get(x => x.ID == userId);
            return user != null ? new User(user) : null;
        }

        public User GetUserWithRole(int userId)
        {
            var user = _userRepository
                .GetAll()
                .Include(x => x.ROLE)
                .FirstOrDefault(x => x.ID == userId);
            return user != null ? new User(user) : null;
        }

        public void CreateUser(User user)
        {
            user.Role = GetRoles()
                .FirstOrDefault(x => x.Name == UserRole.ToUpper());
            _userRepository.Create(user.CreateUser());
            SaveChanges();
        }
    }
}
