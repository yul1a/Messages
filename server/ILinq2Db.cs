using LinqToDB.Data;

namespace Messages.Server
{
    public interface ILinq2Db
    {
        DataConnection GetConnection();
    }
}