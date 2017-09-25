using CarsSale.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsSale.DataAccess.DTO
{
    public class Region
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Region() { }

        public Region(REGION entity = null)
        {
            if (entity == null) return;
            Id = entity.ID;
            Name = entity.NAME;
        }

        public REGION Convert() =>
            new REGION
            {
                ID = Id,
                NAME = Name
            };
}
}
