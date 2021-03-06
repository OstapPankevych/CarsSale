﻿namespace CarsSale.DataAccess.DTO
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public Brand() { }

        public Brand(BRAND entity = null)
        {
            if (entity == null) return;
            Id = entity.ID;
            Name = entity.NAME;
        }

        public BRAND Convert() =>
            new BRAND
            {
                ID = Id,
                NAME = Name
            };
    }
}
