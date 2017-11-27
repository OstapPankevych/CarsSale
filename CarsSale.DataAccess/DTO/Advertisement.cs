using System;
using System.Linq;
using System.Security.Claims;
using CarsSale.DataAccess.Identity.Entities;

namespace CarsSale.DataAccess.DTO
{
    public class Advertisement
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public CarsSaleUser User { get; set; }

        public Vehicl Vehicl { get; set; }

        public Region Region { get; set; }

        public string ImagePath { get; set; }

        public Currency Currency { get; set; }

        public int Price { get; set; }

        public Advertisement(ADVERTISEMENT entity = null)
        {
            if (entity == null) return;
            Id = entity.ID;
            IsActive = entity.IS_ACTIVE;
            ExpirationDate = entity.EXPIRATION_DATE;
            CreatedDate = entity.CREATED_DATE;
            Vehicl = new Vehicl(entity.VEHICL);
            Region = new Region(entity.REGION);
            ImagePath = entity.IMAGE_PATH;
            Currency = new Currency(entity.CURRENCY);
            Price = entity.PRICE;
            User = new CarsSaleUser
            {
                Email = entity.User?.Email,
                UserName = entity.User?.UserName,
                PhoneNumber = entity.User?.PhoneNumber,
                FullName = entity.User?.FullName
            };
        }

        public ADVERTISEMENT Convert()
        {
            return new ADVERTISEMENT
            {
                IS_ACTIVE = IsActive,
                EXPIRATION_DATE = ExpirationDate,
                CREATED_DATE = CreatedDate,
                VEHICL_ID = Vehicl.Id,
                REGION_ID = Region.Id,
                USER_ID = User.Id,
                IMAGE_PATH = ImagePath,
                PRICE = Price,
                CURRENCY_ID = Currency.Id
            };
        }
    }
}
