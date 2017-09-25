using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class UserRepository: Repository, IUserRepository
    {
        public User Add(User user)
        {
            using (var context = new CarsSaleEntities())
            {
                var entity = context.USERs.Add(user.Convert());
                context.SaveChanges();
                return new User(entity);
            }
        }

        public IEnumerable<User> GetAll(Func<USER, bool> predicate)
        {
            using (var context = CreateContext())
            {
                return context.USERs
                    .AsEnumerable()
                    .Where(predicate)
                    .Select(x => new User(x))
                    .ToList();
            }
        }

        public User Get(Func<USER, bool> predicate)
        {
            using (var context = CreateContext())
            {
                var user = context.USERs.FirstOrDefault(predicate);
                return new User(user);
            }
        }
    }
}
