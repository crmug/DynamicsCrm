using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace CrmLibrary.Test
{
    [TestClass]
    public class CrmServiceHelperTests
    {
        [TestMethod]
        public void TestGetService()
        {
            var crmServiceHelper = CrmServiceHelper.Instance;
            crmServiceHelper.ConnectionString = "Url=http://192.168.137.138:5555/CrmOrg/XRMServices/2011/Organization.svc; Username=mscrm\\administrator;Password=P@ssw0rd;";
            IOrganizationService service = crmServiceHelper.GetService();

            // 验证service是否为空
            Assert.IsNotNull(service);

            var request = new RetrieveEntityRequest
            {
                EntityFilters = EntityFilters.All,
                LogicalName = "account"
            };
            var response = (RetrieveEntityResponse)service.Execute(request);

            // 验证service对象是否能够成功执行请求
            Assert.IsNotNull(response);
        }
    }
}
