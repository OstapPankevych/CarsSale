using CarsSale.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsSale.DataAccess.DTO
{
    public class VehiclType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public VehiclType() { }

        public VehiclType(VEHICL_TYPE entity)
        {
            Id = entity.ID;
            Name = entity.NAME;
        }

        public VEHICL_TYPE Convert() =>
            new VEHICL_TYPE
            {
                ID = Id,
                NAME = Name
            };
    }
}
