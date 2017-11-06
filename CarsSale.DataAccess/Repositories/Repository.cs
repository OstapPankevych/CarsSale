namespace CarsSale.DataAccess.Repositories
{
    public abstract class Repository
    {
        private readonly string _connectionString;

        protected Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected CarsSaleEntities CreateContext() =>
            new CarsSaleEntities(_connectionString);
    }
}
