using Microsoft.WindowsAzure.MobileServices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamSnap.Tests
{
    [TestFixture]
    public class MobileServiceClientTests
    {
        private MobileServiceClient _client;

        [SetUp]
        public void SetUp()
        {
            _client = new MobileServiceClient("https://xamsnap.azurewebsites.net");
        }

        [Test]
        public async Task Login()
        {
            
        }

        [Test]
        public async Task Values()
        {
            string text = await _client.InvokeApiAsync<string>("values");

            Assert.AreEqual("Hello World!", text);
        }
    }
}
