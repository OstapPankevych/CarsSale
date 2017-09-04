using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsSale.DataAccess.Services.Interfaces
{
    public interface IService: IDisposable
    {
        int SaveChanges();
    }
}
