using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using Messages.Server;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Test.Integration.Messages
{
    [TestFixture]
    public class IntegrationTestBase
    {
        protected readonly MessagesController messagesController;
        protected static ChromeDriver driver;

        protected readonly Storage storage;
        protected readonly Linq2Db host;

        protected IntegrationTestBase()

        {
            host = new Linq2Db();
            storage = new Storage(host);
            messagesController = new MessagesController(storage);
        }

        protected void InitDriver()
        {
            if (driver == null)
            {
                driver = new ChromeDriver();
                Restart();
            }
        }

        public static void Restart()
        {
            driver.Navigate().GoToUrl("http://localhost:5001/");
            Thread.Sleep(1000);
        }

        public RemoteWebElement GetElement(string selector)
        {
            return (RemoteWebElement) driver.FindElementByClassName(selector);
        }

        public ReadOnlyCollection<IWebElement> GetElements(string selector)
        {
            return driver.FindElementsByClassName(selector);
        }

        public static bool Wait(Func<bool> tryFunc, Action<int> onWaitFailed)
        {
            var w = Stopwatch.StartNew();
            var tryResult = false;
            var iteration = 0;
            do
            {
                iteration++;

                tryResult = tryFunc();
                if (tryResult)
                    break;
                Thread.Sleep(100);
            } while (w.ElapsedMilliseconds < 15000);

            if (!tryResult)
                onWaitFailed(iteration);
            return tryResult;
        }
    }
}