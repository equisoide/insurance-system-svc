using System;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Core;
using Gap.Insurance.Model;
using Gap.Insurance.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gap.Insurance.Test
{
    [TestClass]
    public class DeleteClientPolicyTest : InsuranceBaseTest
    {
        [TestMethod]
        public async Task NullPayload()
        {
            var clientPolicySvc = GetService<IClientPolicyService>();
            var payload = (CancelClientPolicyPayload)null;
            var response = await clientPolicySvc.CancelClientPolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CancelClientPolicyStatus.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task NegativeClientPolicyId()
        {
            var clientPolicySvc = GetService<IClientPolicyService>();
            var payload = new CancelClientPolicyPayload { ClientPolicyId = -1 };
            var response = await clientPolicySvc.CancelClientPolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CancelClientPolicyStatus.ClientPolicyIdFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task ClientPolicyIdNotFound()
        {
            var clientPolicySvc = GetService<IClientPolicyService>();
            var payload = new CancelClientPolicyPayload { ClientPolicyId = 11 };
            var response = await clientPolicySvc.CancelClientPolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CancelClientPolicyStatus.ClientPolicyIdNotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task ClientPolicyNotActive()
        {
            var clientPolicySvc = GetService<IClientPolicyService>();
            var payload = new CancelClientPolicyPayload { ClientPolicyId = 1 };
            var response = await clientPolicySvc.CancelClientPolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CancelClientPolicyStatus.ClientPolicyNotActive, response.StatusCode);
        }

        [TestMethod]
        public async Task CancelClientPolicyOk()
        {
            var clientPolicySvc = GetService<IClientPolicyService>();
            var payload = new CancelClientPolicyPayload { ClientPolicyId = 3 };
            var response = await clientPolicySvc.CancelClientPolicyAsync(payload);
            var expectedMsg = InsuranceResources.Get("CancelClientPolicyOk");

            Assert.AreEqual(payload.ClientPolicyId, response.Data.ClientPolicyId);
            Assert.AreNotEqual(null, response.Data.PolicyName);
            Assert.AreNotEqual(null, response.Data.PolicyStatusDescription);
            Assert.AreNotEqual(DateTime.MinValue, response.Data.StartDate);
            Assert.AreEqual(expectedMsg, response.Message);
            Assert.AreEqual(ApiMessageType.Success, response.MessageType);
            Assert.AreEqual(CancelClientPolicyStatus.CancelClientPolicyOk, response.StatusCode);
            Assert.AreEqual(true, response.Success);
        }
    }
}
