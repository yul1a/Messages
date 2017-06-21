using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using System.Web.OData;
using Messages.Server;
using NUnit.Framework;

namespace Test.Messages
{
    public class MessagesControllerTest : TestBase
    {
        [Test]
        public void GetEmpty()
        {
            var result = messagesController.Get() as OkNegotiatedContentResult<List<Message>>;
            Assert.That(result.Content, Is.Empty);
        }

        [Test]
        public void GetSimple()
        {
            var message = new Message
            {
                Header = "Request",
                Body = "Text"
            };
            storage.Create(message);

            var result = messagesController.Get() as OkNegotiatedContentResult<List<Message>>;
            Assert.That(result.Content.Count, Is.EqualTo(1));
            var dbMessage = result.Content[0];
            Assert.That(dbMessage.Header, Is.EqualTo("Request"));
            Assert.That(dbMessage.Body, Is.Null);
            Assert.That(dbMessage.Id, Is.Not.Null);
        }

        [Test]
        public void AddMessage()
        {
            var message = new Message
            {
                Header = "Request",
                Body = "Text"
            };
            var result = messagesController.AddMessage(message) as OkNegotiatedContentResult<Message>;
            Assert.That(result.Content.Body, Is.EqualTo("Text"));
            Assert.That(result.Content.Header, Is.EqualTo("Request"));

            var single = storage.Select<Message>().Single();
            Assert.That(single.Body, Is.EqualTo("Text"));
            Assert.That(single.Header, Is.EqualTo("Request"));
            Assert.That(single.Header, Is.Not.Null);
        }

        [Test]
        public void UpdateMessage()
        {
            var message = new Message
            {
                Header = "Request",
                Body = "Text"
            };
            storage.Create(message);


            var single = storage.Select<Message>().Single();

            var deltaMessage = new Delta<Message>();
            deltaMessage.TrySetPropertyValue("Body", "another text");

            messagesController.Patch(single.Id, deltaMessage);
            single = storage.Select<Message>().Single();
            Assert.That(single.Body, Is.EqualTo("another text"));
        }

        [Test]
        public void UpdateMessageWithWrongId()
        {
            var deltaMessage = new Delta<Message>();
            deltaMessage.TrySetPropertyValue("Body", "another text");

            var notFoundResult = messagesController.Patch(int.MaxValue, deltaMessage) as NotFoundResult;

            Assert.That(notFoundResult, Is.Not.Null);
        }

        [Test]
        public void DeleteMessage()
        {
            var message = new Message
            {
                Header = "Request",
                Body = "Text"
            };
            storage.Create(message);


            var single = storage.Select<Message>().Single();
            messagesController.Delete(single.Id);

            Assert.False(storage.Select<Message>().Any());
        }

        [Test]
        public void DeleteMessageWithWrongId()
        {
            var httpActionResult = messagesController.Delete(int.MaxValue) as NotFoundResult;
            Assert.That(httpActionResult, Is.Not.Null);
        }

        [Test]
        public void BodyWithWrongId()
        {
            var httpActionResult = messagesController.Body(int.MaxValue) as NotFoundResult;
            Assert.That(httpActionResult, Is.Not.Null);
        }

        [Test]
        public void Body()
        {
            var message = new Message
            {
                Header = "Request",
                Body = "Text"
            };
            storage.Create(message);

            var single = storage.Select<Message>().Single();
            var httpActionResult = messagesController.Body(single.Id) as OkNegotiatedContentResult<string>;
            Assert.That(httpActionResult.Content, Is.EqualTo("Text"));
        }
    }
}