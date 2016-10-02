using System;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace XamSnap.Tests
{
    [TestFixture]
    public class AzureFunctionsTests
    {
        private AzureFunctionsWebService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new AzureFunctionsWebService();
        }

        [Test]
        public async Task LoginNewUser()
        {
            var user = await _service.Login(Guid.NewGuid().ToString("N"), "Woot");

            Assert.IsNotNull(user);
        }

        [Test]
        public async Task LoginExisting()
        {
            var user = await _service.Login("Azure", "Woot");

            Assert.IsNotNull(user);
        }

        [Test, ExpectedException(typeof(HttpRequestException))]
        public async Task LoginExistingUserWrongPassword()
        {
            var user = await _service.Login("Azure", "aslidfjsalkf");

            Assert.IsNotNull(user);
        }
    }
}
