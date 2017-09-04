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

        public Region(REGION entity)
        {
            Id = entity.ID;
            Name = entity.NAME;
        }
    }
}
