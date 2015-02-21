using System;
using System.Collections.Generic;
using System.Linq;
using CrmLibrary.Metadata;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace CrmLibrary
{
    public class FieldMetadataHelper : BaseHelper
    {

        #region 示例方法

        /// <summary>
        /// 创建示例字段
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

        #endregion 

        #region 通用的CRM字段创建方法

        /// <summary>
        /// 创建字段
        /// </summary>
        public OrganizationResponse CreateField(FieldMetadata fieldMetadata)
        {

            OrganizationResponse response = null;
            switch (fieldMetadata.FieldType)
            {
                case FieldType.SingleLineOfText:
                    response = CreateSingleLineOfTextField(
                        fieldMetadata.EntityName,
                        fieldMetadata.SchemName,
                        fieldMetadata.DisplayName,
                        fieldMetadata.Description,
                        fieldMetadata.MaxLength,
                        fieldMetadata.RequiredLevel);
                    break;
                case FieldType.MultipleLinesOfText:
                    response = CreateMultipleLinesOfTextField(
                        fieldMetadata.EntityName,
                        fieldMetadata.SchemName,
                        fieldMetadata.DisplayName,
                        fieldMetadata.Description,
                        fieldMetadata.MaxLength,
                        fieldMetadata.RequiredLevel);
                    break;
                case FieldType.DateTime:
                    response = CreateDateTimeField(
                        fieldMetadata.EntityName,
                        fieldMetadata.SchemName,
                        fieldMetadata.DisplayName,
                        fieldMetadata.Description,
                        fieldMetadata.RequiredLevel);
                    break;
                case FieldType.WholeNumber:
                    response = CreateWholeNumberField(
                        fieldMetadata.EntityName,
                        fieldMetadata.SchemName,
                        fieldMetadata.DisplayName,
                        fieldMetadata.Description,
                        fieldMetadata.MinValue,
                        fieldMetadata.MaxValue,
                        fieldMetadata.RequiredLevel);
                    break;
                case FieldType.FloatingPointNumber:
                    response = CreateFloatingPointNumberField(
                        fieldMetadata.EntityName,
                        fieldMetadata.SchemName,
                        fieldMetadata.DisplayName,
                        fieldMetadata.Description,
                        fieldMetadata.MinValue,
                        fieldMetadata.MaxValue,
                        fieldMetadata.RequiredLevel);
                    break;
                case FieldType.Decimal:
                    response = CreateDecimalField(
                        fieldMetadata.EntityName,
                        fieldMetadata.SchemName,
                        fieldMetadata.DisplayName,
                        fieldMetadata.Description,
                        fieldMetadata.Precision,
                        fieldMetadata.RequiredLevel);
                    break;
                case FieldType.TwoOptions:
                    response = CreateTwoOptionsField(
                        fieldMetadata.EntityName,
                        fieldMetadata.SchemName,
                        fieldMetadata.DisplayName,
                        fieldMetadata.Description,
                        fieldMetadata.Options,
                        fieldMetadata.RequiredLevel);
                    break;
                case FieldType.OptionSet:
                    response = CreateOptionSetField(
                        fieldMetadata.EntityName,
                        fieldMetadata.SchemName,
                        fieldMetadata.DisplayName,
                        fieldMetadata.Description,
                        fieldMetadata.Options,
                        fieldMetadata.IsGlobal,
                        fieldMetadata.RequiredLevel);
                    break;
                case FieldType.Lookup:
                    response = CreateLookupField(
                        fieldMetadata.EntityName,
                        fieldMetadata.SchemName,
                        fieldMetadata.DisplayName,
                        fieldMetadata.Description,
                        fieldMetadata.ReferencedEntityName,
                        fieldMetadata.ReferencedAttributeName,
                        fieldMetadata.RelationshipSchemName,
                        fieldMetadata.RequiredLevel);
                    break;

            }
            return response;
        }


        #endregion 

        #region 各类型的CRM字段创建方法

        /// <summary>
        /// 创建Single Line of Text字段
        /// </summary>
        public OrganizationResponse CreateSingleLineOfTextField(
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
        /// 创建DateTime字段
        /// </summary>
        public OrganizationResponse CreateDateTimeField(
            string entityName,
            string schemName,
            string displayName,
            string decription,
            AttributeRequiredLevel requiredLevel)
        {
            var request = new CreateAttributeRequest
            {
                EntityName = entityName,
                Attribute = new DateTimeAttributeMetadata()
                {
                    SchemaName = schemName,
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(requiredLevel),
                    DisplayName = new Label(displayName, 1033),
                    Description = new Label(decription, 1033),
                    // Set extended properties
                    Format = DateTimeFormat.DateOnly,
                    ImeMode = ImeMode.Disabled
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
            IDictionary<string, int> options,
            AttributeRequiredLevel requiredLevel)
        {

            if (options.Count != 2)
            {
                throw new ArgumentException("The options argument should have two options");
            }
            OptionMetadataCollection collection = GetOptionMetadataCollection(options);
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
                        collection[0],  // true option
                        collection[1]   // false option
                    )
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
            bool isGlobal,
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
                    {
                        IsGlobal = isGlobal
                    }
                }
            };
            return Service.Execute(request);
        }

        /// <summary>
        /// 创建Lookup字段
        /// </summary>
        public OrganizationResponse CreateLookupField(
            string entityName,
            string schemName,
            string displayName,
            string decription,
            string referencedEntityName,
            string referencedAttributeName,
            string relationshipSchemName,
            AttributeRequiredLevel requiredLevel)
        {
            CreateOneToManyRequest request = new CreateOneToManyRequest()
            {
                Lookup = new LookupAttributeMetadata()
                {
                    Description = new Label(decription, 1033),
                    DisplayName = new Label(displayName, 1033),
                    SchemaName = schemName,
                    RequiredLevel =
                        new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.ApplicationRequired)
                },
                OneToManyRelationship = new OneToManyRelationshipMetadata()
                {
                    CascadeConfiguration = new CascadeConfiguration()
                    {
                        Assign = CascadeType.Cascade,
                        Delete = CascadeType.Cascade,
                        Merge = CascadeType.Cascade,
                        Reparent = CascadeType.Cascade,
                        Share = CascadeType.Cascade,
                        Unshare = CascadeType.Cascade
                    },
                    ReferencedEntity = referencedEntityName,
                    ReferencedAttribute = referencedAttributeName,
                    ReferencingEntity = entityName,
                    SchemaName = relationshipSchemName
                }
            };
            return Service.Execute(request);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取OptionMetadataCollection
        /// </summary>
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
