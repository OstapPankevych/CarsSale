using CarsSale.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsSale.DataAccess.DTO
{
    public class Engine
    {
        public int Id { get; set; }

        public int Volume { get; set; }

        public IEnumerable<Fuel> Fuels { get; set; }

        public Engine() { }

        public Engine(ENGINE entity = null)
        {
            if (entity == null) return;
            Id = entity.ID;
            Volume = entity.VOLUME;
            Fuels = entity.ENGINE_FUEL
                .Where(x => Id == x.ENGINE_ID)
                .Select(x => new Fuel(x.FUEL))
                .ToList();
        }

        public ENGINE Convert() =>
            new ENGINE
            {
                ID = Id,
                VOLUME = Volume
            };
    }
}
