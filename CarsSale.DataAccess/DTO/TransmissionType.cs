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

        public TransmissionType(TRANSMISSION_TYPE entity)
        {
            Id = entity.ID;
            Name = entity.NAME;
        }
    }
}
