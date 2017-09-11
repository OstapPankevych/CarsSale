namespace CarsSale.DataAccess.Repositories.Interfaces
{
    public interface ITransmissionTypeRepository: IRepository<TRANSMISSION_TYPE, int>
    {
        TRANSMISSION_TYPE CreateIfNotExists(string name);
    }
}
