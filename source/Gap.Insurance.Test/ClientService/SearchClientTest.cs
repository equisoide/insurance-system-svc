using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Core;
using Gap.Insurance.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gap.Insurance.Test
{
    [TestClass]
    public class SearchClientTest : InsuranceBaseTest
    {
        [TestMethod]
        public async Task NullPayload()
        {
            var clientServiceSvc = GetService<IClientService>();
            var payload = (SearchClientPayload)null;
            var response = await clientServiceSvc.SearchClientAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(SearchClientStatus.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task NoCriteria()
        {
            var clientServiceSvc = GetService<IClientService>();
            var payload = new SearchClientPayload { };
            var response = await clientServiceSvc.SearchClientAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(SearchClientStatus.ClientIdFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task EmptyKeyword()
        {
            var clientServiceSvc = GetService<IClientService>();
            var payload = new SearchClientPayload { Keyword = "" };
            var response = await clientServiceSvc.SearchClientAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(SearchClientStatus.KeywordFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task InvalidKeywordLenght()
        {
            var clientServiceSvc = GetService<IClientService>();
            var payload = new SearchClientPayload { Keyword = "a" };
            var response = await clientServiceSvc.SearchClientAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(SearchClientStatus.KeywordFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task OneResultOk()
        {
            var clientServiceSvc = GetService<IClientService>();
            var payload = new SearchClientPayload { Keyword = "CaRtMaN" };
            var response = await clientServiceSvc.SearchClientAsync(payload);

            Assert.AreEqual(1, response.Data.Count());
            Assert.AreEqual(true, response.Success);
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(null, response.MessageType);
            Assert.AreEqual(SearchClientStatus.Ok, response.StatusCode);
        }

        [TestMethod]
        public async Task SeveralResultsOk()
        {
            var clientServiceSvc = GetService<IClientService>();
            var payload = new SearchClientPayload { Keyword = "000" };
            var response = await clientServiceSvc.SearchClientAsync(payload);

            Assert.AreEqual(4, response.Data.Count());
            Assert.AreEqual(true, response.Success);
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(null, response.MessageType);
            Assert.AreEqual(SearchClientStatus.Ok, response.StatusCode);
        }
    }
}
