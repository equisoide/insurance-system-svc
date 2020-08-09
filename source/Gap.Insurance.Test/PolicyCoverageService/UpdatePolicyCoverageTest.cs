using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Core;
using Gap.Insurance.Model;
using Gap.Insurance.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gap.Insurance.Test
{
    [TestClass]
    public class UpdatePolicyCoverageTest : InsuranceBaseTest
    {
        private UpdatePolicyCoveragePayload GetValidPayload() =>
            new UpdatePolicyCoveragePayload
            {
                Percentage = 40,
                PolicyCoverageId = 15
            };

        [TestMethod]
        public async Task NullPayload()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = (UpdatePolicyCoveragePayload)null;
            var response = await PolicyCoverageSvc.UpdatePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(UpdatePolicyCoverageStatus.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task NegativePolicyCoverageId()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = GetValidPayload();
            payload.PolicyCoverageId = -1;
            var response = await PolicyCoverageSvc.UpdatePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(UpdatePolicyCoverageStatus.PolicyCoverageIdFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task NegativePercentage()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = GetValidPayload();
            payload.Percentage = -1;
            var response = await PolicyCoverageSvc.UpdatePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(UpdatePolicyCoverageStatus.PercentageFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task PolicyCoverageIdNotFound()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = GetValidPayload();
            payload.PolicyCoverageId = 11;
            var response = await PolicyCoverageSvc.UpdatePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(UpdatePolicyCoverageStatus.PolicyCoverageIdNotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task MaxCoverageExceeded()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = GetValidPayload();
            payload.Percentage = 60;
            var response = await PolicyCoverageSvc.UpdatePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(UpdatePolicyCoverageStatus.MaxCoverageExceeded, response.StatusCode);
        }

        [TestMethod]
        public async Task PolicyInUse()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = GetValidPayload();
            payload.PolicyCoverageId = 1;
            var response = await PolicyCoverageSvc.UpdatePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(UpdatePolicyCoverageStatus.PolicyInUse, response.StatusCode);
        }

        [TestMethod]
        public async Task UpdatePolicyCoverageOk()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = GetValidPayload();
            var response = await PolicyCoverageSvc.UpdatePolicyCoverageAsync(payload);
            var expectedMsg = InsuranceResources.Get("UpdatePolicyCoverageOk");

            Assert.AreEqual(payload.PolicyCoverageId, response.Data.PolicyCoverageId);
            Assert.AreEqual(payload.Percentage, response.Data.Percentage);
            Assert.AreNotEqual(null, response.Data.CoverageDescription);
            Assert.AreEqual(expectedMsg, response.Message);
            Assert.AreEqual(ApiMessageType.Success, response.MessageType);
            Assert.AreEqual(UpdatePolicyCoverageStatus.UpdatePolicyCoverageOk, response.StatusCode);
            Assert.AreEqual(true, response.Success);
        }
    }
}
