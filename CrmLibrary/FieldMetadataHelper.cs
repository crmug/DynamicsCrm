using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace CrmLibrary
{
    public class FieldMetadataHelper : BaseHelper
    {
        #region Create Fields

        /// <summary>
        /// 创建示例Field
        /// </summary>
        /// <param name="entityName">Entity的LogicalName</param>
        /// <param name="schemName">Field的SchemName</param>
        /// <param name="displayName">Field的DisplayName</param>
        /// <param name="decription">Field的描述</param>
        /// <param name="requiredLevel">Field的</param>
        public OrganizationResponse CreateSampleField(string entityName, string schemName, string displayName, string decription, AttributeRequiredLevel requiredLevel)
        {
            var request = new CreateAttributeRequest
            {
                EntityName = entityName,
                Attribute = new StringAttributeMetadata
                {
                    SchemaName = schemName,
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(requiredLevel),
                    MaxLength = 100,
                    Format = StringFormat.Text,
                    DisplayName = new Label(displayName, 1033),
                    Description = new Label(decription, 1033)
                }
            };
            return Service.Execute(request);
        }

        /// <summary>
        /// 创建Single Line of Text字段
        /// </summary>
        public OrganizationResponse CreateSingleLienOfTextField(
            string entityName,
            string schemName,
            string displayName,
            string decription,
            int maxLength,
            AttributeRequiredLevel requiredLevel)
        {
            var request = new CreateAttributeRequest
            {
                EntityName = entityName,
                Attribute = new StringAttributeMetadata
                {
                    SchemaName = schemName,
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(requiredLevel),
                    MaxLength = maxLength,
                    Format = StringFormat.Text,
                    DisplayName = new Label(displayName, 1033),
                    Description = new Label(decription, 1033)
                }
            };
            return Service.Execute(request);
        }

        /// <summary>
        /// 创建Multiple Lines of Text字段
        /// </summary>
        public OrganizationResponse CreateMultipleLinesOfTextField(
            string entityName,
            string schemName,
            string displayName,
            string decription,
            int maxLength,
            AttributeRequiredLevel requiredLevel)
        {
            var request = new CreateAttributeRequest
            {
                EntityName = entityName,
                Attribute = new MemoAttributeMetadata()
                {
                    SchemaName = schemName,
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(requiredLevel),
                    MaxLength = maxLength,
                    Format = StringFormat.Text,
                    DisplayName = new Label(displayName, 1033),
                    Description = new Label(decription, 1033)
                }
            };
            return Service.Execute(request);
        }

        /// <summary>
        /// 创建Whole Number字段
        /// </summary>
        public OrganizationResponse CreateWholeNumberField(
            string entityName,
            string schemName,
            string displayName,
            string decription,
            int? minValue,
            int? maxValue,
            AttributeRequiredLevel requiredLevel)
        {
            var request = new CreateAttributeRequest
            {
                EntityName = entityName,
                Attribute = new IntegerAttributeMetadata()
                {
                    SchemaName = schemName,
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(requiredLevel),
                    DisplayName = new Label(displayName, 1033),
                    Description = new Label(decription, 1033),
                    MinValue = minValue,
                    MaxValue = maxValue
                }
            };
            return Service.Execute(request);
        }

        /// <summary>
        /// 创建Floating Point Number字段
        /// </summary>
        public OrganizationResponse CreateFloatingPointNumberField(
            string entityName,
            string schemName,
            string displayName,
            string decription,
            double? minValue,
            double? maxValue,
            AttributeRequiredLevel requiredLevel)
        {
            var request = new CreateAttributeRequest
            {
                EntityName = entityName,
                Attribute = new DoubleAttributeMetadata()
                {
                    SchemaName = schemName,
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(requiredLevel),
                    DisplayName = new Label(displayName, 1033),
                    Description = new Label(decription, 1033),
                    MinValue = minValue,
                    MaxValue = maxValue
                }
            };
            return Service.Execute(request);
        }

        /// <summary>
        /// 创建Decimal字段
        /// </summary>
        public OrganizationResponse CreateDecimalField(
            string entityName,
            string schemName,
            string displayName,
            string decription,
            int? precision,
            AttributeRequiredLevel requiredLevel)
        {
            var request = new CreateAttributeRequest
            {
                EntityName = entityName,
                Attribute = new DecimalAttributeMetadata()
                {
                    SchemaName = schemName,
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(requiredLevel),
                    DisplayName = new Label(displayName, 1033),
                    Description = new Label(decription, 1033),
                    Precision = precision
                }
            };
            return Service.Execute(request);
        }

        /// <summary>
        /// 创建Two Options字段
        /// </summary>
        public OrganizationResponse CreateTwoOptionsField(
            string entityName,
            string schemName,
            string displayName,
            string decription,
            string trueLabel,
            int trueValue,
            string falseLabel,
            int falseValue,
            AttributeRequiredLevel requiredLevel)
        {
            var request = new CreateAttributeRequest
            {
                EntityName = entityName,
                Attribute = new BooleanAttributeMetadata()
                {
                    SchemaName = schemName,
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(requiredLevel),
                    DisplayName = new Label(displayName, 1033),
                    Description = new Label(decription, 1033),
                    OptionSet = new BooleanOptionSetMetadata(
                        new OptionMetadata
                        {
                            Label = new Label(trueLabel, 1033),
                            Value = trueValue
                        }, new OptionMetadata
                        {
                            Label = new Label(falseLabel, 1033),
                            Value = falseValue
                        })
                }
            };
            return Service.Execute(request);
        }

        /// <summary>
        /// 创建OptionSet字段
        /// </summary>
        public OrganizationResponse CreateOptionSetField(
            string entityName,
            string schemName,
            string displayName,
            string decription,
            IDictionary<string,int> options,
            AttributeRequiredLevel requiredLevel)
        {
            OptionMetadataCollection collection = GetOptionMetadataCollection(options);
            var request = new CreateAttributeRequest
            {
                EntityName = entityName,
                Attribute = new PicklistAttributeMetadata()
                {
                    SchemaName = schemName,
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(requiredLevel),
                    DisplayName = new Label(displayName, 1033),
                    Description = new Label(decription, 1033),
                    OptionSet = new OptionSetMetadata(collection)
                }
            };
            return Service.Execute(request);
        }

        #endregion


        #region 私有方法

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        private OptionMetadataCollection GetOptionMetadataCollection(IDictionary<string, int> options)
        {
            IList<OptionMetadata> list = options.Select(opt => new OptionMetadata()
            {
                Label = new Label(opt.Key, 1033),
                Value = opt.Value
            }).ToList();
            var collection = new OptionMetadataCollection(list);
            return collection;
        }

        #endregion 
    }
}
