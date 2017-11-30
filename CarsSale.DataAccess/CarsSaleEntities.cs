using System.Data.Entity;

namespace CarsSale.DataAccess
{
    public partial class CarsSaleEntities : DbContext
    {
        public CarsSaleEntities(string connectionString)
            : base(connectionString)
        { }
    }
}