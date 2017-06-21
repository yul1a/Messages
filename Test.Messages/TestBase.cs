using Messages.Server;
using NUnit.Framework;

namespace Test.Messages
{
    public abstract class TestBase
    {
        protected readonly MessagesController messagesController;
        protected readonly Storage storage;
        protected readonly Linq2Db host;

        protected TestBase()

        {
            host = new Linq2Db();
            storage = new Storage(host);
            messagesController = new MessagesController(storage);
        }

        [SetUp]
        public void ActualSetUp()
        {
            SetUp();
        }

        public virtual void SetUp()
        {
            Assert.That(host.GetConnection().ConnectionString, Does.Contain("Database=testmessages"));
            storage.Remove<Message>(x => true);
        }
    }
}