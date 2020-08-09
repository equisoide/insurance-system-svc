using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Core;
using Gap.Insurance.Model;
using Gap.Insurance.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gap.Insurance.Test
{
    [TestClass]
    public class CreatePolicyCoverageTest : InsuranceBaseTest
    {
        private CreatePolicyCoveragePayload GetValidPayload() =>
            new CreatePolicyCoveragePayload
            {
                CoverageId = 4,
                PolicyId = 4,
                Percentage = 50
            };

        [TestMethod]
        public async Task NullPayload()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = (CreatePolicyCoveragePayload)null;
            var response = await PolicyCoverageSvc.CreatePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyCoverageStatus.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task NegativeCoverageId()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = GetValidPayload();
            payload.CoverageId = -1;
            var response = await PolicyCoverageSvc.CreatePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyCoverageStatus.CoverageIdFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task NegativePolicyId()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = GetValidPayload();
            payload.PolicyId = -1;
            var response = await PolicyCoverageSvc.CreatePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyCoverageStatus.PolicyIdFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task NegativePercentage()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = GetValidPayload();
            payload.Percentage = -1;
            var response = await PolicyCoverageSvc.CreatePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyCoverageStatus.PercentageFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task PolicyIdNotFound()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = GetValidPayload();
            payload.PolicyId = 100;
            var response = await PolicyCoverageSvc.CreatePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyCoverageStatus.PolicyIdNotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task CoverageIdNotFound()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = GetValidPayload();
            payload.CoverageId = 100;
            var response = await PolicyCoverageSvc.CreatePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyCoverageStatus.CoverageIdNotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task CoverageAlreadyAdded()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = GetValidPayload();
            payload.CoverageId = 3;
            var response = await PolicyCoverageSvc.CreatePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyCoverageStatus.CoverageAlreadyAdded, response.StatusCode);
        }

        [TestMethod]
        public async Task MaxCoverageExceeded()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = GetValidPayload();
            payload.Percentage = 100;
            var response = await PolicyCoverageSvc.CreatePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(CreatePolicyCoverageStatus.MaxCoverageExceeded, response.StatusCode);
        }

        [TestMethod]
        public async Task CreatePolicyCoverageOk()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = GetValidPayload();
            var response = await PolicyCoverageSvc.CreatePolicyCoverageAsync(payload);
            var expectedMsg = InsuranceResources.Get("CreatePolicyCoverageOk");

            Assert.AreNotEqual(0, response.Data.PolicyCoverageId);
            Assert.AreNotEqual(null, response.Data.CoverageDescription);
            Assert.AreEqual(payload.Percentage, response.Data.Percentage);
            Assert.AreNotEqual(0, response.Data.PolicyCoverageId);
            Assert.AreEqual(expectedMsg, response.Message);
            Assert.AreEqual(ApiMessageType.Success, response.MessageType);
            Assert.AreEqual(CreatePolicyCoverageStatus.CreatePolicyCoverageOk, response.StatusCode);
            Assert.AreEqual(true, response.Success);
        }
    }
}
