namespace CarsSale.DataAccess.Repositories.Interfaces
{
    public interface ICompleteSetRepository: IRepository<COMPLETESET, int>
    {
        COMPLETESET CreateIfNotExists(int engineId, int transmissionId);
    }
}
