namespace CarsSale.DataAccess.Repositories.Interfaces
{
    public interface IVehiclTypeRepository: IRepository<VEHICL_TYPE, int>
    {
        VEHICL_TYPE CreateIfNotExists(string name);
    }
}
