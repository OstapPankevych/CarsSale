using CarsSale.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsSale.DataAccess.DTO
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public DateTime Birthday { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public User() { }

        public User(USER entity)
        {
            Id = entity.ID;
            Login = entity.LOGIN;
            Name = entity.NAME;
            Email = entity.EMAIL;
            Birthday = entity.BIRTHDAY;
            Phone = entity.PHONE;
            Password = entity.PASSWORD;
            Role = new Role(entity.ROLE);
        }

        public USER Convert() =>
            new USER
            {
                ID = Id,
                LOGIN = Login,
                NAME = Name,
                EMAIL = Email,
                BIRTHDAY = Birthday,
                PASSWORD = Password,
                PHONE = Phone,
                ROLE_ID = Role.Id
            };
    }
}
