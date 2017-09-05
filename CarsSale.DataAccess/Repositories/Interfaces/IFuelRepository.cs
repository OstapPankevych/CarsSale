namespace CarsSale.DataAccess.Repositories.Interfaces
{
    public interface IFuelRepository: IRepository<FUEL, int>
    {
        FUEL CreateIfNotExists(string name);
    }
}
