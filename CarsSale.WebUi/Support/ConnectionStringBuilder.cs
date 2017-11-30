using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;

namespace CarsSale.WebUi.Support
{
    public static class ConnectionStringBuilder
    {
        public static string IdentityConnectionString
        {
            get
            {
                var entityConnectionStringBuilder =
                    new EntityConnectionStringBuilder(ConnectionString);
                var sqlConnection = new SqlConnection(entityConnectionStringBuilder.ProviderConnectionString);

                return sqlConnection.ConnectionString;
            }
        }

        public static string ConnectionString => 
            System.Configuration.ConfigurationManager.
                ConnectionStrings["CarsSaleEntities"].ConnectionString;
    }
}