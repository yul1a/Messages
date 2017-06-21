using System.Linq;
using Messages.Server;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Test.Integration.Messages
{
    public class MessagesControllerIntegrationTest : IntegrationTestBase
    {
        [SetUp]
        public void ActualSetUp()
        {
            SetUp();
        }

        public virtual void SetUp()
        {
            storage.Remove<Message>(x => true);
            InitDriver();
            Restart();
        }

        [Test]
        public void Simple()
        {
            Assert.That(driver, Is.Not.Null);

            var message = new Message
            {
                Header = "Request",
                Body = "Text"
            };
            storage.Create(message);
            var single = storage.Select<Message>().Single();
            WaitMessages(1);
            var messages = GetElements("message");
            var webElement = messages[0];
            Assert.That(webElement.FindElement(By.ClassName("messageId")).Text, Is.EqualTo(single.Id.ToString()));
            Assert.That(webElement.FindElement(By.ClassName("messageHeader")).Text, Is.EqualTo("Request"));
            Assert.That(webElement.FindElement(By.ClassName("messageBody")).Text, Is.EqualTo(""));
        }

        [Test]
        public void Delete()
        {
            var message = new Message
            {
                Header = "Request",
                Body = "Text"
            };
            storage.Create(message);

            var message2 = new Message
            {
                Header = "Request2",
                Body = "Text2"
            };
            storage.Create(message2);

            WaitMessages(2);
            var messages = GetElements("message");
            var webElement = messages[0];
            Assert.That(webElement.FindElement(By.ClassName("messageHeader")).Text, Is.EqualTo("Request"));
            webElement.FindElement(By.ClassName("delete")).Click();

            WaitMessages(1);
            messages = GetElements("message");
            webElement = messages[0];
            Assert.That(webElement.FindElement(By.ClassName("messageHeader")).Text, Is.EqualTo("Request2"));
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

            WaitMessages(1);
            var messages = GetElements("message");
            var webElement = messages[0];
            Assert.That(webElement.FindElement(By.ClassName("messageBody")).Text, Is.EqualTo(""));
            GetMessageBodyButton(webElement).Click();

            Wait(() => webElement.FindElement(By.ClassName("messageBody")).GetAttribute("value") == "Text",
                i => { Assert.Fail("Не удалось получить текст сообщения"); });
        }

        [Test]
        public void Update()
        {
            var message = new Message
            {
                Header = "Request",
                Body = "Text"
            };
            storage.Create(message);
            WaitMessages(1);

            var messages = GetElements("message");
            var webElement = messages[0];
            Assert.That(webElement.FindElement(By.ClassName("messageBody")).Text, Is.EqualTo(""));
            GetMessageBodyButton(webElement).Click();

            var findElement = webElement.FindElement(By.ClassName("messageBody"));
            Wait(() =>
                    findElement.GetAttribute("value") == "Text"
                , i => { Assert.Fail("text not found"); });


            findElement.SendKeys("plus");
            webElement.FindElement(By.ClassName("update")).Click();

            Restart();
            WaitMessages(1);

            webElement = GetElements("message")[0];
            webElement.FindElement(By.ClassName("getbody")).Click();
            Wait(() => webElement.FindElement(By.ClassName("messageBody")).GetAttribute("value") == "Textplus",
                i => { Assert.Fail("text not found"); });
        }

        private static IWebElement GetMessageBodyButton(IWebElement webElement)
        {
            return webElement.FindElement(By.ClassName("getbody"));
        }


        [Test]
        public void CreateNew()
        {
            driver.FindElement(By.Id("newMessageBody")).SendKeys("Text");
            driver.FindElement(By.Id("newMessageHeader")).SendKeys("Request");
            driver.FindElement(By.ClassName("submit")).Click();

            WaitMessages(1);

            var message = storage.Select<Message>().Single();
            Assert.That(message.Body, Is.EqualTo("Text"));
            Assert.That(message.Header, Is.EqualTo("Request"));
            Assert.That(message.Id, Is.Not.Null);
        }

        private void WaitMessages(int expectedCount)
        {
            Wait(() => { 
                Restart(); 
                return GetElements("message").Count == expectedCount;
            },
                i => { Assert.Fail("messages count was  " + GetElements("message").Count); });
        }
    }
}