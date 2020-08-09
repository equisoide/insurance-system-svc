using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Core;
using Gap.Insurance.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gap.Insurance.Test
{
    [TestClass]
    public class CheckPolicyUsageTest : InsuranceBaseTest
    {
        [TestMethod]
        public async Task NullPayload()
        {
            var clientPolicyServiceSvc = GetService<IClientPolicyService>();
            var payload = (CheckPolicyUsagePayload)null;
            var response = await clientPolicyServiceSvc.CheckPolicyUsageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CheckPolicyUsageStatus.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task NegativePolicyId()
        {
            var clientPolicyServiceSvc = GetService<IClientPolicyService>();
            var payload = new CheckPolicyUsagePayload { PolicyId = -1 };
            var response = await clientPolicyServiceSvc.CheckPolicyUsageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CheckPolicyUsageStatus.PolicyIdFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task NotInUseOk()
        {
            var clientPolicyServiceSvc = GetService<IClientPolicyService>();
            var payload = new CheckPolicyUsagePayload { PolicyId = 10 };
            var response = await clientPolicyServiceSvc.CheckPolicyUsageAsync(payload);

            Assert.AreEqual(false, response.Data.IsInUse);
            Assert.AreEqual(true, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Success, response.MessageType);
            Assert.AreEqual(CheckPolicyUsageStatus.Ok, response.StatusCode);
        }

        [TestMethod]
        public async Task InUseOk()
        {
            var clientPolicyServiceSvc = GetService<IClientPolicyService>();
            var payload = new CheckPolicyUsagePayload { PolicyId = 1 };
            var response = await clientPolicyServiceSvc.CheckPolicyUsageAsync(payload);

            Assert.AreEqual(true, response.Data.IsInUse);
            Assert.AreEqual(true, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Success, response.MessageType);
            Assert.AreEqual(CheckPolicyUsageStatus.Ok, response.StatusCode);
        }
    }
}
