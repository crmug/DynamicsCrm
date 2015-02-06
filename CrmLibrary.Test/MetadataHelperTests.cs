using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk.Metadata;

namespace CrmLibrary.Test
{
    [TestClass]
    public class MetadataHelperTests
    {
        [TestMethod]
        public void TestCreateEntity()
        {
            var metadataHelper = new EntityMetadataHelper();
            var response = metadataHelper.CreateEntity(
                "new_bankaccount",      // Entity名称
                "Bank Account",         // Entity显示名称
                "Bank Accounts",        // Entity复数显示名称
                "new_accountname",      // PrimaryField名称
                "Account Name"          // PrimaryField显示名称
            );

            Assert.IsNotNull(response);
            Debug.WriteLine("EntityId:"     + response.Results["EntityId"]);
            Debug.WriteLine("AttributeId:"  + response.Results["AttributeId"]);
        }

        [TestMethod]
        public void TestCreateSampleField()
        {
            var metadataHelper = new FieldMetadataHelper();
            var response = metadataHelper.CreateSampleField(
                "new_bankaccount",          // Entity名称
                "new_accountcode",          // Filed名称
                "Account Code",             // Field显示名称
                string.Empty,               // Field描述
                AttributeRequiredLevel.None // 字段需要级别
            );

            Assert.IsNotNull(response);
            Debug.WriteLine("EntityId:" + response.Results["AttributeId"]);
        }
    }
}
