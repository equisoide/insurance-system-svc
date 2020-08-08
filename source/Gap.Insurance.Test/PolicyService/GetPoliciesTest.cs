using System.Linq;
using System.Threading.Tasks;
using Gap.Insurance.Core;
using Gap.Insurance.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gap.Insurance.Test
{
    [TestClass]
    public class GetPoliciesTest : InsuranceBaseTest
    {
        [TestMethod]
        public async Task AllRecordsOk()
        {
            var policySvc = GetService<IPolicyService>();
            var response = await policySvc.GetPoliciesAsync();

            Assert.AreEqual(true, response.Data.Count() > 0);
            Assert.AreEqual(true, response.Success);
            Assert.AreEqual(GetPoliciesStatus.Ok, response.StatusCode);
        }
    }
}
