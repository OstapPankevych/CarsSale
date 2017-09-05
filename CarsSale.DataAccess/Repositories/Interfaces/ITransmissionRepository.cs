namespace CarsSale.DataAccess.Repositories.Interfaces
{
    public interface ITransmissionRepository: IRepository<TRANSMISSION, int>
    {
        TRANSMISSION CreateIfNotExists(string name);
    }
}
