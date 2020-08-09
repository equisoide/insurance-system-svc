using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Core;
using Gap.Insurance.Model;
using Gap.Insurance.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gap.Insurance.Test
{
    [TestClass]
    public class DeletePolicyCoverageTest : InsuranceBaseTest
    {
        [TestMethod]
        public async Task NullPayload()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = (DeletePolicyCoveragePayload)null;
            var response = await PolicyCoverageSvc.DeletePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(DeletePolicyCoverageStatus.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task NegativePolicyCoverageId()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = new DeletePolicyCoveragePayload { PolicyCoverageId = -1 };
            var response = await PolicyCoverageSvc.DeletePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(DeletePolicyCoverageStatus.PolicyCoverageIdFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task PolicyCoverageIdNotFound()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = new DeletePolicyCoveragePayload { PolicyCoverageId = 11 };
            var response = await PolicyCoverageSvc.DeletePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(DeletePolicyCoverageStatus.PolicyCoverageIdNotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task PolicyInUse()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = new DeletePolicyCoveragePayload { PolicyCoverageId = 1 };
            var response = await PolicyCoverageSvc.DeletePolicyCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(DeletePolicyCoverageStatus.PolicyInUse, response.StatusCode);
        }

        [TestMethod]
        public async Task DeletePolicyCoverageOk()
        {
            var PolicyCoverageSvc = GetService<IPolicyCoverageService>();
            var payload = new DeletePolicyCoveragePayload { PolicyCoverageId = 11 };
            var response = await PolicyCoverageSvc.DeletePolicyCoverageAsync(payload);
            var expectedMsg = InsuranceResources.Get("DeletePolicyCoverageOk");

            Assert.AreEqual(payload.PolicyCoverageId, response.Data.PolicyCoverageId);
            Assert.AreNotEqual(null, response.Data.CoverageDescription);
            Assert.AreNotEqual(0, response.Data.Percentage);
            Assert.AreEqual(expectedMsg, response.Message);
            Assert.AreEqual(ApiMessageType.Success, response.MessageType);
            Assert.AreEqual(DeletePolicyCoverageStatus.DeletePolicyCoverageOk, response.StatusCode);
            Assert.AreEqual(true, response.Success);
        }
    }
}
