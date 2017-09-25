using System;

namespace CarsSale.DataAccess.DTO
{
    public class Advertisement
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public User User { get; set; }

        public Vehicl Vehicl { get; set; }

        public Region Region { get; set; }

        public Advertisement(ADVERTISEMENT entity = null)
        {
            if (entity == null) return;
            Id = entity.ID;
            IsActive = entity.IS_ACTIVE;
            ExpirationDate = entity.EXPIRATION_DATE;
            CreatedDate = entity.CREATED_DATE;
            User = new User(entity.USER);
            Vehicl = new Vehicl(entity.VEHICL);
            Region = new Region(entity.REGION);
        }

        public ADVERTISEMENT Convert()
        {
            return new ADVERTISEMENT
            {
                IS_ACTIVE = IsActive,
                EXPIRATION_DATE = ExpirationDate,
                CREATED_DATE = CreatedDate,
                VEHICL_ID = Vehicl.Id,
                USER_ID = User.Id,
                REGION_ID = Region.Id
            };
        }
    }
}
