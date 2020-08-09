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
    public class CreatePolicyTest : InsuranceBaseTest
    {
        private CreatePolicyPayload GetValidPayload() =>
            new CreatePolicyPayload
            {
                Description = "All covered 100%, medium risk, 1 year",
                Name = "All Medium 12 Vip",
                Periods = 12,
                Price = 750,
                RiskId = 2
            };

        [TestMethod]
        public async Task NullPayload()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = (CreatePolicyPayload)null;
            var response = await policySvc.CreatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyStatus.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task NegativeRiskId()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.RiskId = -1;
            var response = await policySvc.CreatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyStatus.RiskIdFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task NullName()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Name = null;
            var response = await policySvc.CreatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyStatus.NameFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task EmptyName()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Name = string.Empty;
            var response = await policySvc.CreatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyStatus.NameFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task ExceedNameLength()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Name = new string('x', 51);
            var response = await policySvc.CreatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyStatus.NameFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task NullDescription()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Description = null;
            var response = await policySvc.CreatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyStatus.DescriptionFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task EmptyDescription()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Description = string.Empty;
            var response = await policySvc.CreatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyStatus.DescriptionFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task ExceedDescriptionLength()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Description = new string('x', 251);
            var response = await policySvc.CreatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyStatus.DescriptionFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task NegativePeriods()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Periods = -1;
            var response = await policySvc.CreatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyStatus.PeriodsFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task NegativePrice()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Price = -1;
            var response = await policySvc.CreatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyStatus.PriceFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task RiskIdNotFound()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.RiskId = 11;
            var response = await policySvc.CreatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyStatus.RiskIdNotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task NameAlreadyTaken()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            payload.Name = "All Low 12 Vip";
            var response = await policySvc.CreatePolicyAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyStatus.NameAlreadyTaken, response.StatusCode);
        }

        [TestMethod]
        public async Task CreatePolicyOk()
        {
            var policySvc = GetService<IPolicyService>();
            var payload = GetValidPayload();
            var response = await policySvc.CreatePolicyAsync(payload);
            var expectedMsg = InsuranceResources.Get("CreatePolicyOk");

            Assert.AreNotEqual(0, response.Data.PolicyId);
            Assert.AreEqual(payload.Description, response.Data.Description);
            Assert.AreEqual(payload.Name, response.Data.Name);
            Assert.AreEqual(payload.Periods, response.Data.Periods);
            Assert.AreEqual(payload.Price, response.Data.Price);
            Assert.AreEqual(payload.RiskId, response.Data.RiskId);
            Assert.AreNotEqual(null, response.Data.RiskDescripition);
            Assert.AreEqual(0, response.Data.Coverages.Count());
            Assert.AreEqual(expectedMsg, response.Message);
            Assert.AreEqual(ApiMessageType.Success, response.MessageType);
            Assert.AreEqual(CreatePolicyStatus.CreatePolicyOk, response.StatusCode);
            Assert.AreEqual(true, response.Success);
        }
    }
}
