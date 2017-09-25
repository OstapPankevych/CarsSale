using System;
using System.Collections.Generic;
using CarsSale.DataAccess.DTO;

namespace CarsSale.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User Add(User user);
        IEnumerable<User> GetAll(Func<USER, bool> predicate);
        User Get(Func<USER, bool> predicate);
    }
}
