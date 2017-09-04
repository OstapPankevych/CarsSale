using CarsSale.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsSale.DataAccess.DTO
{
    public class CompleteSet
    {
        public int Id { get; set; }

        public Engine Engine { get; set; }

        public Transmission Transmission { get; set; }

        public CompleteSet(COMPLETESET entity)
        {
            Id = entity.ID;
            Engine = new Engine(entity.ENGINE);
            Transmission = new Transmission(entity.TRANSMISSION);
        }
    }
}
