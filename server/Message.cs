using LinqToDB.Mapping;

namespace Messages.Server
{
    public class Message
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        public string Header { get; set; }

        public string Body { get; set; }
    }
}