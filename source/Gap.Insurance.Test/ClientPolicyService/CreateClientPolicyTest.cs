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
    public class CreateClientClientPolicyTest : InsuranceBaseTest
    {
        private CreateClientPolicyPayload GetValidPayload() =>
            new CreateClientPolicyPayload
            {
                ClientId = 1,
                PolicyId = 1,
                StartDate = DateTime.Today
            };

        [TestMethod]
        public async Task NullPayload()
        {
            var clientPolicySvc = GetService<IClientPolicyService>();
            var payload = (CreateClientPolicyPayload)null;
            var response = await clientPolicySvc.CreateClientPolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreateClientPolicyStatus.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task NegativeClientId()
        {
            var clientPolicySvc = GetService<IClientPolicyService>();
            var payload = GetValidPayload();
            payload.ClientId = -1;
            var response = await clientPolicySvc.CreateClientPolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreateClientPolicyStatus.ClientIdFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task NegativePolicyId()
        {
            var clientPolicySvc = GetService<IClientPolicyService>();
            var payload = GetValidPayload();
            payload.PolicyId = -1;
            var response = await clientPolicySvc.CreateClientPolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreateClientPolicyStatus.PolicyIdFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task InvalidDate()
        {
            var clientPolicySvc = GetService<IClientPolicyService>();
            var payload = GetValidPayload();
            payload.StartDate = DateTime.MinValue;
            var response = await clientPolicySvc.CreateClientPolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreateClientPolicyStatus.StartDateFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task ClientIdNotFound()
        {
            var clientPolicySvc = GetService<IClientPolicyService>();
            var payload = GetValidPayload();
            payload.ClientId = 11;
            var response = await clientPolicySvc.CreateClientPolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreateClientPolicyStatus.ClientIdNotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task PolicyIdNotFound()
        {
            var clientPolicySvc = GetService<IClientPolicyService>();
            var payload = GetValidPayload();
            payload.PolicyId = 22;
            var response = await clientPolicySvc.CreateClientPolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreateClientPolicyStatus.PolicyIdNotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task CreateClientPolicyOk()
        {
            var clientPolicySvc = GetService<IClientPolicyService>();
            var payload = GetValidPayload();
            var response = await clientPolicySvc.CreateClientPolicyAsync(payload);
            var expectedMsg = InsuranceResources.Get("CreateClientPolicyOk");

            Assert.AreNotEqual(0, response.Data.ClientPolicyId);
            Assert.AreNotEqual(null, response.Data.PolicyName);
            Assert.AreNotEqual(null, response.Data.PolicyStatusDescription);
            Assert.AreEqual(payload.StartDate, response.Data.StartDate);
            Assert.AreEqual(expectedMsg, response.Message);
            Assert.AreEqual(ApiMessageType.Success, response.MessageType);
            Assert.AreEqual(CreateClientPolicyStatus.CreateClientPolicyOk, response.StatusCode);
            Assert.AreEqual(true, response.Success);
        }
    }
}
