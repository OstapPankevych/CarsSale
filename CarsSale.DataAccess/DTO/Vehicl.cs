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

        public BodyType BodyType { get; set; }

        public CompleteSet CompleteSet { get; set; }

        public VehiclType VehiclType { get; set; }

        public Vehicl(VEHICL entity)
        {
            Brand = new Brand(entity.BRAND);
            BodyType = new BodyType(entity.BODY_TYPE);
            CompleteSet = new CompleteSet(entity.COMPLETESET);
            VehiclType = new VehiclType(entity.VEHICL_TYPE);
        }
    }
}
