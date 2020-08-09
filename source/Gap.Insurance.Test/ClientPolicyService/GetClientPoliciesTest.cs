using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Core;
using Gap.Insurance.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gap.Insurance.Test
{
    [TestClass]
    public class GetClientPoliciesTest : InsuranceBaseTest
    {
        [TestMethod]
        public async Task NullPayload()
        {
            var clientPolicyServiceSvc = GetService<IClientPolicyService>();
            var payload = (GetClientPoliciesPayload)null;
            var response = await clientPolicyServiceSvc.GetClientPoliciesAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(GetClientPoliciesStatus.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task NegativeClientId()
        {
            var clientPolicyServiceSvc = GetService<IClientPolicyService>();
            var payload = new GetClientPoliciesPayload { ClientId = -1 };
            var response = await clientPolicyServiceSvc.GetClientPoliciesAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(GetClientPoliciesStatus.ClientIdFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task NoPoliciesFound()
        {
            var clientPolicyServiceSvc = GetService<IClientPolicyService>();
            var payload = new GetClientPoliciesPayload { ClientId = 10 };
            var response = await clientPolicyServiceSvc.GetClientPoliciesAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(GetClientPoliciesStatus.NoPoliciesFound, response.StatusCode);
        }

        [TestMethod]
        public async Task SomePoliciesFound()
        {
            var clientPolicyServiceSvc = GetService<IClientPolicyService>();
            var payload = new GetClientPoliciesPayload { ClientId = 1 };
            var response = await clientPolicyServiceSvc.GetClientPoliciesAsync(payload);

            Assert.AreNotEqual(null, response.Data);
            Assert.AreEqual(true, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Success, response.MessageType);
            Assert.AreEqual(GetClientPoliciesStatus.Ok, response.StatusCode);
        }
    }
}
