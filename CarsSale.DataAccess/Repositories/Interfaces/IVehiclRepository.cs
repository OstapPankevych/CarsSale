namespace CarsSale.DataAccess.Repositories.Interfaces
{
    public interface IVehiclRepository: IRepository<VEHICL, int>
    {
        VEHICL CreateIfNotExists(int brandId,
            int vehiclTypeId,
            int completeSetId);
    }
}
