using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using Newtonsoft.Json;

namespace Messages.Server
{
    public class Linq2Db : ILinq2Db
    {
        private readonly string ConnectionString = System.Configuration
            .ConfigurationManager.ConnectionStrings["DbMessages"].ConnectionString;

        private readonly DataConnection db;

        public Linq2Db()
        {
            db = SqlServerTools.CreateDataConnection(ConnectionString);
        }

        public DataConnection GetConnection()
        {
            return db;
        }
    }
}