using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;

namespace CarsSale.WebUi.Support
{
    public static class ConnectionStringBuilder
    {
        public static string ConnectionString { get; private set; }

        public static string IdentityConnectionString { get; private set; }

        static ConnectionStringBuilder()
        {
            ConnectionString = System.Configuration.ConfigurationManager.
                ConnectionStrings["CarsSaleEntities"].ConnectionString;

            var entityConnectionStringBuilder =
                new EntityConnectionStringBuilder(ConnectionString);

            var sqlConnection = new SqlConnection(entityConnectionStringBuilder.ProviderConnectionString);

            IdentityConnectionString = sqlConnection.ConnectionString;
        }
    }
}