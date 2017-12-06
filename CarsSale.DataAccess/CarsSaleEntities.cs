using System.Data.Entity;

namespace CarsSale.DataAccess
{
    public partial class CarsSaleEntities
    {
        public CarsSaleEntities(string connectionString)
            : base(connectionString)
        { }
    }
}