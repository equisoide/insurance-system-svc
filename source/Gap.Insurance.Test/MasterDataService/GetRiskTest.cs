using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Core;
using Gap.Insurance.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gap.Insurance.Test
{
    [TestClass]
    public class GetRiskTest : InsuranceBaseTest
    {
        [TestMethod]
        public async Task SingleRecordNullPayload()
        {
            var masterDataSvc = GetService<IMasterDataService>();
            var payload = (GetRiskPayload)null;
            var response = await masterDataSvc.GetRiskAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(GetRiskStatus.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task SingleRecordNegativeRiskId()
        {
            var masterDataSvc = GetService<IMasterDataService>();
            var payload = new GetRiskPayload { RiskId = -1 };
            var response = await masterDataSvc.GetRiskAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(false, response.Success);
            Assert.AreNotEqual(null, response.Message);
            Assert.AreEqual(ApiMessageType.Error, response.MessageType);
            Assert.AreEqual(GetRiskStatus.RiskIdFormat, response.StatusCode);
        }

        [TestMethod]
        public async Task SingleRecordRiskIdDoesntExist()
        {
            var masterDataSvc = GetService<IMasterDataService>();
            var payload = new GetRiskPayload { RiskId = 100 };
            var response = await masterDataSvc.GetRiskAsync(payload);

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(true, response.Success);
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(null, response.MessageType);
            Assert.AreEqual(GetRiskStatus.Ok, response.StatusCode);
        }

        [TestMethod]
        public async Task SingleRecordOk()
        {
            var masterDataSvc = GetService<IMasterDataService>();
            var payload = new GetRiskPayload { RiskId = 1 };
            var response = await masterDataSvc.GetRiskAsync(payload);

            Assert.AreEqual(payload.RiskId, response.Data.RiskId);
            Assert.AreEqual(true, response.Success);
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(null, response.MessageType);
            Assert.AreEqual(GetRiskStatus.Ok, response.StatusCode);
        }

        [TestMethod]
        public async Task AllRecordsOk()
        {
            var masterDataSvc = GetService<IMasterDataService>();
            var response = await masterDataSvc.GetRisksAsync();

            Assert.AreEqual(true, response.Data.Count() > 0);
            Assert.AreEqual(true, response.Success);
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(null, response.MessageType);
            Assert.AreEqual(GetRisksStatus.Ok, response.StatusCode);
        }
    }
}
