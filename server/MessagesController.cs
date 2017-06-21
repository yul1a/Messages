using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using LinqToDB;
using Messages.server;

namespace Messages.Server
{
    [RoutePrefix("api/messages")]
    public class MessagesController : ApiController, IMessagesController
    {
        private readonly Storage storage;

        public MessagesController(Storage storage)
        {
            this.storage = storage;
        }

        public IHttpActionResult Get()
        {
            try
            {
                var messages = storage.Select<Message>().Select(
                    c => new Message
                    {
                        Id = c.Id,
                        Header = c.Header
                    }).ToList();

                return Ok(messages);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("body/{id:int}")]
        public IHttpActionResult Body(int id)
        {
            try
            {
                var message = storage
                    .Select<Message>()
                    .SingleOrDefault(x => x.Id == id);
                if (message == null)
                {
                    return NotFound();
                }
                return Ok(message.Body);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        public IHttpActionResult AddMessage([FromBody] Message message)
        {
            if (message == null)
            {
                return BadRequest();
            }

            var messageToAdd = new Message
            {
                Header = message.Header,
                Body = message.Body
            };

            try
            {
                storage.Create(messageToAdd);
                return Ok(messageToAdd);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var messages = storage.Select<Message>();
            var message = messages.SingleOrDefault(x => x.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            messages.Delete(x => x.Id == id);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPatch]
        [Route("{id:int}")]
        public IHttpActionResult Patch(int id, Delta<Message> message)
        {
            if (message == null)
            {
                return BadRequest();
            }

            var messages = storage.Select<Message>();
            var existingMessage = messages.SingleOrDefault(x => x.Id == id);

            if (existingMessage == null)
            {
                return NotFound();
            }
            message.Patch(existingMessage);
            storage.Update(existingMessage);

            return Ok(existingMessage);
        }
    }
}