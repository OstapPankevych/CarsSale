using CarsSale.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsSale.DataAccess.DTO
{
    public class Fuel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Fuel() { }

        public Fuel(FUEL entity)
        {
            Id = entity.ID;
            Name = entity.NAME;
        }

        public FUEL Convert() =>
            new FUEL
            {
                ID = Id,
                NAME = Name
            };
    }
}
