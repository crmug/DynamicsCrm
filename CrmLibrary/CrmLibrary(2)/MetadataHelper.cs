using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace CrmLibrary
{
    public class MetadataHelper : BaseHelper
    {
        #region Create Entity

        /// <summary>
        /// 创建Entity
        /// </summary>
        /// <param name="entityName">Entity的逻辑名称</param>
        /// <param name="entityDisplayName">Entity的显示名称</param>
        /// <param name="entityPluralName">Entity的复数显示名称</param>
        /// <param name="primaryAttrName">Primary Field的逻辑名称</param>
        /// <param name="primaryAttrDisplayName">Primary Field的显示名称</param>
        public OrganizationResponse CreateEntity(
            string entityName,
            string entityDisplayName,
            string entityPluralName,
            string primaryAttrName,
            string primaryAttrDisplayName)
        {
            if (CheckCreateEntity(entityName, primaryAttrName))
            {
                var request = new CreateEntityRequest()
                {
                    Entity = new EntityMetadata()
                    {
                        SchemaName = entityName,
                        DisplayName = new Label(entityDisplayName, 1033),
                        DisplayCollectionName = new Label(entityPluralName, 1033),
                        OwnershipType = OwnershipTypes.OrganizationOwned,
                    },

                    PrimaryAttribute = new StringAttributeMetadata
                    {
                        SchemaName = primaryAttrName,
                        RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                        MaxLength = 100,
                        Format = StringFormat.Text,
                        DisplayName = new Label(primaryAttrDisplayName, 1033)
                    },
                };
                return Service.Execute(request);
            }
            return null;
        }
        #endregion 

        #region

        /// <summary>
        /// 验证创建Entity的LogicalName和Primary Field的LogicalName
        /// </summary>
        /// <param name="entityName">Entity的LogicalName</param>
        /// <param name="primaryAttrName">Primary Field的LogicalName</param>
        private bool CheckCreateEntity(string entityName, string primaryAttrName)
        {
            // 验证Entity的LogicalName是否以new_开头
            if (!entityName.StartsWith("new_"))
            {
                throw new ArgumentException("The customize entity name should start with \"new_\".");
            }
            // 验证Entity的LogicalName是否都是小写字符
            if (entityName.ToLower() != entityName)
            {
                throw new ArgumentException("The customize entity name should be lower characters.");
            }
            // 验证Entity的LogicalName是否已存在
            try
            {
                var request = new RetrieveEntityRequest
                {
                    EntityFilters = EntityFilters.Entity,
                    LogicalName = entityName
                };
                Service.Execute(request);
                throw new ArgumentException(string.Format("The entity {0} is already exists.", entityName));
            }
            catch // 出现异常时表示该Entity的LogicaName是不存在的，可以创建
            {
            }

            // 验证Primary Field的LogicalName是否以new_开头
            if (!primaryAttrName.StartsWith("new_"))
            {
                throw new ArgumentException("The primary field name should start with \"new_\".");
            }
            // 验证Primary Field的LogicalName是否和Primary Key的LogicalName重复
            if (string.Concat(entityName, "id") == primaryAttrName)
            {
                throw new ArgumentException("The entity primary field name should not equal with primary key name.");
            }
            // 验证Primary Field的LogicalName是否都是小写字符
            if (primaryAttrName.ToLower() != primaryAttrName)
            {
                throw new ArgumentException("The primary field name should be lower characters.");
            }
            return true;
        }

        #endregion
    }
}
