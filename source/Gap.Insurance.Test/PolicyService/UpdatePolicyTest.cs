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
    public class UpdatePolicyTest : InsuranceBaseTest
    {
        private UpdatePolicyPayload GetValidPayload() =>
            new UpdatePolicyPayload
            {
                PolicyId = 2,
                Description = "All covered 100%, low risk, 6 months",
                Name = "All Low Risk 6 Vip",
                Periods = 6,
                Price = 300,
                RiskId = 2
            };

        [TestMethod]
        public async Task NullPayload()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = (UpdatePolicyPayload)null;
            var response = await policySvc.UpdatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(UpdatePolicyStatus.BadRequest, response.StatusCode);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(false, response.Success);
        }

        [TestMethod]
        public async Task NegativePolicyId()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.PolicyId = -1;
            var response = await policySvc.UpdatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(UpdatePolicyStatus.PolicyIdFormat, response.StatusCode);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(false, response.Success);
        }

        [TestMethod]
        public async Task NegativeRiskId()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.RiskId = -1;
            var response = await policySvc.UpdatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(UpdatePolicyStatus.RiskIdFormat, response.StatusCode);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(false, response.Success);
        }

        [TestMethod]
        public async Task NullName()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Name = null;
            var response = await policySvc.UpdatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(UpdatePolicyStatus.NameFormat, response.StatusCode);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(false, response.Success);
        }

        [TestMethod]
        public async Task EmptyName()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Name = string.Empty;
            var response = await policySvc.UpdatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(UpdatePolicyStatus.NameFormat, response.StatusCode);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(false, response.Success);
        }

        [TestMethod]
        public async Task ExceedNameLength()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Name = new string('x', 51);
            var response = await policySvc.UpdatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(UpdatePolicyStatus.NameFormat, response.StatusCode);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(false, response.Success);
        }

        [TestMethod]
        public async Task NullDescription()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Description = null;
            var response = await policySvc.UpdatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(UpdatePolicyStatus.DescriptionFormat, response.StatusCode);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(false, response.Success);
        }

        [TestMethod]
        public async Task EmptyDescription()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Description = string.Empty;
            var response = await policySvc.UpdatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(UpdatePolicyStatus.DescriptionFormat, response.StatusCode);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(false, response.Success);
        }

        [TestMethod]
        public async Task ExceedDescriptionLength()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Description = new string('x', 251);
            var response = await policySvc.UpdatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(UpdatePolicyStatus.DescriptionFormat, response.StatusCode);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(false, response.Success);
        }

        [TestMethod]
        public async Task NegativePeriods()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Periods = -1;
            var response = await policySvc.UpdatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(UpdatePolicyStatus.PeriodsFormat, response.StatusCode);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(false, response.Success);
        }

        [TestMethod]
        public async Task NegativePrice()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Price = -1;
            var response = await policySvc.UpdatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(UpdatePolicyStatus.PriceFormat, response.StatusCode);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(false, response.Success);
        }

        [TestMethod]
        public async Task PolicyIdNotFound()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.PolicyId = 11;
            var response = await policySvc.UpdatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(UpdatePolicyStatus.PolicyIdNotFound, response.StatusCode);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(false, response.Success);
        }

        [TestMethod]
        public async Task PolicyInUse()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.PolicyId = 1;
            var response = await policySvc.UpdatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(UpdatePolicyStatus.PolicyInUse, response.StatusCode);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(false, response.Success);
        }

        [TestMethod]
        public async Task RiskIdNotFound()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.RiskId = 11;
            var response = await policySvc.UpdatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(UpdatePolicyStatus.RiskIdNotFound, response.StatusCode);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(false, response.Success);
        }

        [TestMethod]
        public async Task NameAlreadyTaken()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Name = "All High 6 40%";
            var response = await policySvc.UpdatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(UpdatePolicyStatus.NameAlreadyTaken, response.StatusCode);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(false, response.Success);
        }

        [TestMethod]
        public async Task UpdatePolicyOk()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            var response = await policySvc.UpdatePolicyAsync(payload);
            var expectedMsg = InsuranceResources.Get("UpdatePolicyOk");

            Assert.AreEqual(payload.PolicyId, response.Data.PolicyId);
            Assert.AreEqual(payload.Description, response.Data.Description);
            Assert.AreEqual(payload.Name, response.Data.Name);
            Assert.AreEqual(payload.Periods, response.Data.Periods);
            Assert.AreEqual(payload.Price, response.Data.Price);
            Assert.AreEqual(payload.RiskId, response.Data.RiskId);
            Assert.AreNotEqual(null, response.Data.RiskDescripition);
            Assert.AreNotEqual(0, response.Data.Coverages.Count());
            Assert.AreEqual(expectedMsg, response.Message);
            Assert.AreEqual(ApiMessageType.Success, response.MessageType);
            Assert.AreEqual(UpdatePolicyStatus.UpdatePolicyOk, response.StatusCode);
            Assert.AreEqual(true, response.Success);
        }
    }
}
