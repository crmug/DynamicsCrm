using System.Collections.Generic;
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
            Debug.WriteLine("AttributeId:" + response.Results["AttributeId"]);
        }

        [TestMethod]
        public void TestCreateSingleLineOfTextField()
        {
            var metadataHelper = new FieldMetadataHelper();
            var response = metadataHelper.CreateSingleLineOfTextField(
                "new_bankaccount",          // Entity名称
                "new_singlelineoftext",     // Filed名称
                "Single Line of Text",      // Field显示名称
                string.Empty,               // Field描述
                100,                        // Field最大长度
                AttributeRequiredLevel.None // 字段需要级别
            );

            Assert.IsNotNull(response);
            Debug.WriteLine("AttributeId:" + response.Results["AttributeId"]);
        }

        [TestMethod]
        public void TestCreateMultipleLinesOfTextField()
        {
            var metadataHelper = new FieldMetadataHelper();
            var response = metadataHelper.CreateSingleLineOfTextField(
                "new_bankaccount",          // Entity名称
                "new_multiplelinesoftext",  // Filed名称
                "Multiple Lines of Text",   // Field显示名称
                string.Empty,               // Field描述
                4000,                       // Field最大长度
                AttributeRequiredLevel.None // 字段需要级别
            );

            Assert.IsNotNull(response);
            Debug.WriteLine("AttributeId:" + response.Results["AttributeId"]);
        }

        [TestMethod]
        public void TestCreateWholeNumberField()
        {
            var metadataHelper = new FieldMetadataHelper();
            var response = metadataHelper.CreateWholeNumberField(
                "new_bankaccount",          // Entity名称
                "new_wholenumber",          // Filed名称
                "Whole Number",             // Field显示名称
                string.Empty,               // Field描述
                1,                          // Field最小值
                1000,                       // Field最大值
                AttributeRequiredLevel.None // 字段需要级别
            );

            Assert.IsNotNull(response);
            Debug.WriteLine("AttributeId:" + response.Results["AttributeId"]);
        }

        [TestMethod]
        public void TestCreateFloatingPointNumberField()
        {
            var metadataHelper = new FieldMetadataHelper();
            var response = metadataHelper.CreateFloatingPointNumberField(
                "new_bankaccount",          // Entity名称
                "new_floatingpointnumber",  // Filed名称
                "Floating Point Number",    // Field显示名称
                string.Empty,               // Field描述
                -1000000.00,                // Field最小值
                1000000.00,                 // Field最大值
                AttributeRequiredLevel.None // 字段需要级别
            );

            Assert.IsNotNull(response);
            Debug.WriteLine("AttributeId:" + response.Results["AttributeId"]);
        }

        [TestMethod]
        public void TestCreateDecimalField()
        {
            var metadataHelper = new FieldMetadataHelper();
            var response = metadataHelper.CreateDecimalField(
                "new_bankaccount",          // Entity名称
                "new_decimal",              // Filed名称
                "Decimal",                  // Field显示名称
                string.Empty,               // Field描述
                2,                          // Field小数精度
                AttributeRequiredLevel.None // 字段需要级别
            );

            Assert.IsNotNull(response);
            Debug.WriteLine("AttributeId:" + response.Results["AttributeId"]);
        }

        [TestMethod]
        public void TestCreateDateTimeField()
        {
            var metadataHelper = new FieldMetadataHelper();
            var response = metadataHelper.CreateDateTimeField(
                "new_bankaccount",          // Entity名称
                "new_datetime",             // Filed名称
                "DateTime",                 // Field显示名称
                string.Empty,               // Field描述
                AttributeRequiredLevel.None // 字段需要级别
            );

            Assert.IsNotNull(response);
            Debug.WriteLine("AttributeId:" + response.Results["AttributeId"]);
        }

        [TestMethod]
        public void TestCreateTwoOptionsField()
        {
            var metadataHelper = new FieldMetadataHelper();
            var response = metadataHelper.CreateTwoOptionsField(
                "new_bankaccount",                              // Entity名称
                "new_twooptions",                               // Filed名称
                "Two Options",                                  // Field显示名称
                string.Empty,                                   // Field描述
                new Dictionary<string, int>{{"Yes",1},{"No",0}},// Two Options选项 
                AttributeRequiredLevel.None                     // 字段需要级别
            );

            Assert.IsNotNull(response);
            Debug.WriteLine("AttributeId:" + response.Results["AttributeId"]);
        }

        [TestMethod]
        public void TestCreateOptionSetField()
        {
            var metadataHelper = new FieldMetadataHelper();
            var response = metadataHelper.CreateOptionSetField(
                "new_bankaccount",              // Entity名称
                "new_optionset",                // Filed名称
                "OptionSet",                    // Field显示名称
                string.Empty,                   // Field描述
                new Dictionary<string, int>     // OptionSet选项集 
                {
                    {"Green", 1},
                    {"Yellow", 2},
                    {"Red", 3},
                    {"Black", 4}
                }, 
                false,                          // IsGlobal
                AttributeRequiredLevel.None     // 字段需要级别
                );

            Assert.IsNotNull(response);
            Debug.WriteLine("AttributeId:" + response.Results["AttributeId"]);
        }

        [TestMethod]
        public void TestCreateLookupField()
        {
            var metadataHelper = new FieldMetadataHelper();
            var response = metadataHelper.CreateLookupField(
                "new_bankaccount",              // Entity名称
                "new_lookup",                   // Filed名称
                "Lookup",                       // Field显示名称
                string.Empty,                   // Field描述
                "account",                      // 被关联的Entity名称
                "accountid",                    // 被关联的Entity主键字段名称
                "new_account_new_bankaccount",  // Relationship的名称
                AttributeRequiredLevel.None     // 字段需要级别
                );

            Assert.IsNotNull(response);
            Debug.WriteLine("AttributeId:" + response.Results["AttributeId"]);
        }
    }
}
