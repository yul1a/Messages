using System.Web.Http;
using System.Web.OData;
using Messages.Server;

namespace Messages.server
{
    public interface IMessagesController
    {
        IHttpActionResult Get();
        IHttpActionResult Body(int id);
        IHttpActionResult AddMessage([FromBody] Message message);
        IHttpActionResult Delete(int id);
        IHttpActionResult Patch(int id, Delta<Message> message);
    }
}