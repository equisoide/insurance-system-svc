using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Core;
using Gap.Insurance.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gap.Insurance.Test
{
    [TestClass]
    public class GetCoverageTest : InsuranceBaseTest
    {
        [TestMethod]
        public async Task SingleRecordNullPayload()
        {
            var masterDataSvc = GetService<IMasterDataService>();
            var payload = (GetCoveragePayload)null;
            var response = await masterDataSvc.GetCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(GetCoverageStatus.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task SingleRecordNegativeCoverageId()
        {
            var masterDataSvc = GetService<IMasterDataService>();
            var payload = new GetCoveragePayload { CoverageId = -1 };
            var response = await masterDataSvc.GetCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(GetCoverageStatus.CoverageIdFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task SingleRecordCoverageIdDoesntExist()
        {
            var masterDataSvc = GetService<IMasterDataService>();
            var payload = new GetCoveragePayload { CoverageId = 100 };
            var response = await masterDataSvc.GetCoverageAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(GetCoverageStatus.CoverageIdNotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task SingleRecordOk()
        {
            var masterDataSvc = GetService<IMasterDataService>();
            var payload = new GetCoveragePayload { CoverageId = 1 };
            var response = await masterDataSvc.GetCoverageAsync(payload);

            Assert.AreEqual(payload.CoverageId, response.Data.CoverageId);
            Assert.AreEqual(true, response.Success);
            Assert.AreEqual(GetCoverageStatus.Ok, response.StatusCode);
        }

        [TestMethod]
        public async Task AllRecordsOk()
        {
            var masterDataSvc = GetService<IMasterDataService>();
            var response = await masterDataSvc.GetCoveragesAsync();

            Assert.AreEqual(true, response.Data.Count() > 0);
            Assert.AreEqual(true, response.Success);
            Assert.AreEqual(GetCoveragesStatus.Ok, response.StatusCode);
        }
    }
}
