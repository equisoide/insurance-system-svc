using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Core;
using Gap.Insurance.Model;
using Gap.Insurance.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gap.Insurance.Test
{
    [TestClass]
    public class DeletePolicyTest : InsuranceBaseTest
    {
        [TestMethod]
        public async Task NullPayload()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = (DeletePolicyPayload)null;
            var response = await policySvc.DeletePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(DeletePolicyStatus.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task NegativePolicyId()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = new DeletePolicyPayload { PolicyId = -1 };
            var response = await policySvc.DeletePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(DeletePolicyStatus.PolicyIdFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task PolicyIdNotFound()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = new DeletePolicyPayload { PolicyId = 11 };
            var response = await policySvc.DeletePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(DeletePolicyStatus.PolicyIdNotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task PolicyInUse()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = new DeletePolicyPayload { PolicyId = 1 };
            var response = await policySvc.DeletePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(DeletePolicyStatus.PolicyInUse, response.StatusCode);
        }

        [TestMethod]
        public async Task DeletePolicyOk()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = new DeletePolicyPayload { PolicyId = 3 };
            var response = await policySvc.DeletePolicyAsync(payload);
            var expectedMsg = InsuranceResources.Get("DeletePolicyOk");

            Assert.AreEqual(payload.PolicyId, response.Data.PolicyId);
            Assert.AreNotEqual(null, response.Data.Description);
            Assert.AreNotEqual(null, response.Data.Name);
            Assert.AreNotEqual(0, response.Data.Periods);
            Assert.AreNotEqual(0, response.Data.Price);
            Assert.AreNotEqual(0, response.Data.RiskId);
            Assert.AreNotEqual(null, response.Data.RiskDescripition);
            Assert.AreNotEqual(0, response.Data.MaxCoverage);
            Assert.AreNotEqual(0, response.Data.Coverages.Count());
            Assert.AreEqual(expectedMsg, response.Message);
            Assert.AreEqual(ApiMessageType.Success, response.MessageType);
            Assert.AreEqual(DeletePolicyStatus.DeletePolicyOk, response.StatusCode);
            Assert.AreEqual(true, response.Success);
        }
    }
}
