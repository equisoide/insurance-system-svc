using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Core;
using Gap.Insurance.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gap.Insurance.Test
{
    [TestClass]
    public class GetPolicyTest : InsuranceBaseTest
    {
        [TestMethod]
        public async Task SingleRecordNullPayload()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = (GetPolicyPayload)null;
            var response = await policySvc.GetPolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(GetPolicyStatus.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task SingleRecordNegativePolicyId()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = new GetPolicyPayload { PolicyId = -1 };
            var response = await policySvc.GetPolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(GetPolicyStatus.PolicyIdFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task SingleRecordOk()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = new GetPolicyPayload { PolicyId = 1 };
            var response = await policySvc.GetPolicyAsync(payload);

            Assert.AreNotEqual(null, response.Data);
            Assert.AreEqual(true, response.Success);
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(null, response.MessageType);
            Assert.AreEqual(GetPolicyStatus.Ok, response.StatusCode);
        }

        [TestMethod]
        public async Task AllRecordsOk()
        {
            var policySvc = GetService<IPolicyService>();
            var response = await policySvc.GetPoliciesAsync();

            Assert.AreEqual(true, response.Data.Count() > 0);
            Assert.AreEqual(true, response.Success);
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(null, response.MessageType);
            Assert.AreEqual(GetPoliciesStatus.Ok, response.StatusCode);
        }
    }
}
