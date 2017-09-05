﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsSale.DataAccess.Repositories.Interfaces
{
    public interface IBrandRepository: IRepository<BRAND, int>
    {
        BRAND CreateIfNotExists(string name);
    }
}
