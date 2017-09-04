using CarsSale.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsSale.DataAccess.DTO
{
    public class Advertisement: BaseDto<ADVERTISEMENT>
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public User User { get; set; }

        public Vehicl Vehicl { get; set; }

        public Advertisement(ADVERTISEMENT entity)
            : base(entity)
        {
            Id = entity.ID;
            IsActive = entity.IS_ACTIVE;
            ExpirationDate = entity.EXPIRATION_DATE;
            CreatedDate = entity.CREATED_DATE;
            User = new User(entity.USER);
            Vehicl = new Vehicl(entity.VEHICL);
        }
    }
}
