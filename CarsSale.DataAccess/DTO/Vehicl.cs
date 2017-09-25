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

        public VehiclType VehiclType { get; set; }

        public Engine Engine { get; set; }

        public TransmissionType TransmissionType { get; set; }

        public Vehicl() { }

        public Vehicl(VEHICL entity = null)
        {
            if (entity == null) return;
            Id = entity.ID;
            Brand = new Brand(entity.BRAND);
            VehiclType = new VehiclType(entity.VEHICL_TYPE);
            Engine = new Engine(entity.ENGINE);
            TransmissionType = new TransmissionType(entity.TRANSMISSION_TYPE);
        }

        public VEHICL Convert() =>
            new VEHICL
            {
                ID = Id,
                BRAND_ID = Brand.Id,
                VEHICL_TYPE_ID = VehiclType.Id,
                ENGINE_ID = Engine.Id,
                TRANSMISSION_TYPE_ID = TransmissionType.Id
            };
    }
}
