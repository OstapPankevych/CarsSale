﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsSale.DataAccess.DTO
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Role() { }

        public Role(ROLE entity = null)
        {
            if (entity == null) return;
            Id = entity.ID;
            Name = entity.NAME;
        }

        public ROLE Convert() =>
            new ROLE
            {
                ID = Id,
                NAME = Name
            };
    }
}