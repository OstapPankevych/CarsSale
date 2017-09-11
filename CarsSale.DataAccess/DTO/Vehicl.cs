using CarsSale.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsSale.DataAccess.DTO
{
    public class Vehicl
    {
        public int Id { get; set; }

        public Brand Brand { get; set; }

        public CompleteSet CompleteSet { get; set; }

        public VehiclType VehiclType { get; set; }

        public Vehicl(VEHICL entity)
        {
            Brand = new Brand(entity.BRAND);
            CompleteSet = new CompleteSet(entity.COMPLETESET);
            VehiclType = new VehiclType(entity.VEHICL_TYPE);
        }
    }
}
