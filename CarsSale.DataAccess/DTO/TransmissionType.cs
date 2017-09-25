using CarsSale.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsSale.DataAccess.DTO
{
    public class TransmissionType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TransmissionType() { }

        public TransmissionType(TRANSMISSION_TYPE entity = null)
        {
            if (entity == null) return;
            Id = entity.ID;
            Name = entity.NAME;
        }

        public TRANSMISSION_TYPE Convert() =>
            new TRANSMISSION_TYPE
            {
                ID = Id,
                NAME = Name
            };
    }
}
