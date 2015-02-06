using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace CrmLibrary
{
    public class RecordHelper : BaseHelper
    {

        #region Create

        /// <summary>
        /// 创建Record
        /// </summary>
        /// <param name="entityName">Entity的LogicalName</param>
        /// <param name="attributes">属性键值对</param>
        /// <returns>新创建的Record的Guid</returns>
        public Guid CreateEntityRecord(string entityName, IDictionary<string, string> attributes)
        {
            var entity = new Entity(entityName);
            AttributeMetadata[] metadatas = RetrieveEntityMetadata(entityName,attributes.Keys.ToArray());
            var i = 0;
            foreach (var attr in attributes)
            {
                var metadata = metadatas[i];
                //entity.Attributes[attr.Key] = attr.Value;
                SetAttributeValue(entity,attr,metadata);
                i++;
            }
            Guid id = Service.Create(entity);
            return id;
        }

        #endregion

        #region Update

        /// <summary>
        /// 更新Record
        /// </summary>
        /// <param name="entityName">Entity的LogicalName</param>
        /// <param name="id">要更新的Record的Guid</param>
        /// <param name="attributes">属性键值对</param>
        public void UpdateEntityRecord(string entityName, Guid id, IDictionary<string,string> attributes )
        {
            var columns = attributes.Keys.ToArray();
            Entity entity = Service.Retrieve(entityName, id, new ColumnSet(columns));
            AttributeMetadata[] metadatas = RetrieveEntityMetadata(entityName, columns);
            var i = 0;
            foreach (var attr in attributes)
            {
                var metadata = metadatas[i];
                //entity.Attributes[attr.Key] = attr.Value;
                SetAttributeValue(entity, attr, metadata);
                i++;
            }
            Service.Update(entity);
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// 删除Record
        /// </summary>
        /// <param name="entityName">Entity的LogicalName</param>
        /// <param name="id">要删除的Record的Guid</param>
        public void DeleteEntityRecord(string entityName, Guid id)
        {
            Service.Delete(entityName,id);
        }

        #endregion Delete

        #region Read

        /// <summary>
        /// 读取Record(根据Id)
        /// </summary>
        /// <param name="entityName">Entity的LogicalName</param>
        /// <param name="id">要读取的Record的Guid</param>
        /// <param name="columns">读取的列名称列表</param>
        /// <returns>Entity</returns>
        public Entity ReadEntityRecordById(string entityName,Guid id, string[] columns)
        {
            return Service.Retrieve(entityName,id,new ColumnSet(columns));
        }

        /// <summary>
        /// 读取Record(全部)
        /// </summary>
        /// <param name="entityName">Entity的LogicalName</param>
        /// <param name="columns">读取的列名称列表</param>
        /// <returns>EntityCollection</returns>
        public EntityCollection ReadEntityRecords(string entityName, string[] columns)
        {
            var query = new QueryExpression(entityName) {EntityName = entityName, ColumnSet = new ColumnSet(columns)};
            return Service.RetrieveMultiple(query);
        }

        /// <summary>
        /// 读取Record(根据条件)
        /// </summary>
        /// <param name="entityName">Entity的LogicalName<</param>
        /// <param name="columns">读取的列名称列表</param>
        /// <param name="conditions">Tuple数组：Item1为Attribute名称，Item2为Attribute值，Item3为条件操作符</param>
        /// <returns>EntityCollection</returns>
        public EntityCollection ReadEntityRecords(string entityName, string[] columns,
            Tuple<string, string, ConditionOperator>[] conditions)
        {
            var query = new QueryExpression(entityName) {EntityName = entityName, ColumnSet = new ColumnSet(columns)};
            if (conditions != null)
            {
                var filter = new FilterExpression();
                foreach (var condition in conditions)
                {
                    var con = new ConditionExpression { AttributeName = condition.Item1, Operator = condition.Item3 };
                    con.Values.Add(condition.Item2);
                    filter.Conditions.Add(con);
                }
                query.Criteria.AddFilter(filter);
            }
            return Service.RetrieveMultiple(query);
        }



        /// <summary>
        /// 读取Record(根据条件分页)
        /// </summary>
        /// <param name="entityName">Entity的LogicalName</param>
        /// <param name="columns">读取的列名称列表</param>
        /// <param name="conditions">Tuple数组：Item1为Attribute名称，Item2为Attribute值，Item3为条件操作符</param>
        /// <param name="pageNumber">页数</param>
        /// <param name="count">每页行数</param>
        /// <returns></returns>
        public EntityCollection ReadEntityRecords(string entityName, string[] columns,
            Tuple<string, string, ConditionOperator>[] conditions, int pageNumber, int count)
        {
            var query = new QueryExpression(entityName) { EntityName = entityName, ColumnSet = new ColumnSet(columns) };
            if (conditions != null)
            {
                var filter = new FilterExpression();
                foreach (var condition in conditions)
                {
                    var con = new ConditionExpression { AttributeName = condition.Item1, Operator = condition.Item3 };
                    con.Values.Add(condition.Item2);
                    filter.Conditions.Add(con);
                }
                query.Criteria.AddFilter(filter);
            }
            query.PageInfo = new PagingInfo
            {
                PageNumber = pageNumber,
                Count = count
            };
            return Service.RetrieveMultiple(query);
        }

        #endregion


        /// <summary>
        /// 获取Entity的Metadata数据
        /// </summary>
        /// <param name="entityName">Entity的LogicalName</param>
        /// <param name="columns">要读取Metadata的列名称列表</param>
        /// <returns>Entity的Metadata数据</returns>
        public AttributeMetadata[] RetrieveEntityMetadata(string entityName, string[] columns = null)
        {
            var request = new RetrieveEntityRequest
            {
                EntityFilters = EntityFilters.All,
                LogicalName = entityName
            };
            var response = (RetrieveEntityResponse)Service.Execute(request);

            if (columns != null && columns.Length > 0)
            {
                var all = response.EntityMetadata.Attributes;
                return
                    (from column in columns
                     from metadata in all
                     where metadata.LogicalName == column
                     select metadata)
                        .ToArray();
            }
            return response.EntityMetadata.Attributes;
        }


        /// <summary>
        /// 设置Attribute值
        /// </summary>
        /// <param name="entity">Entity的LogicalName</param>
        /// <param name="attr">Attribute键值对</param>
        /// <param name="metadata">Attribute的Metadata</param>
        public void SetAttributeValue(Entity entity, KeyValuePair<string, string> attr, AttributeMetadata metadata)
        {
            var dataType = metadata.AttributeType.Value;
            switch (dataType)
            {
                case AttributeTypeCode.Picklist:
                    var osv = new OptionSetValue(int.Parse(attr.Value));
                    entity[attr.Key] = osv;
                    break;
                case AttributeTypeCode.Lookup:
                case AttributeTypeCode.Owner:
                    var lookupMetadata = metadata as LookupAttributeMetadata;
                    var referenceEntityName = lookupMetadata.Targets[0];
                    entity[attr.Key] = new EntityReference(referenceEntityName, new Guid(attr.Value));
                    break;
                case AttributeTypeCode.Money:
                    entity[attr.Key] = new Money(decimal.Parse(attr.Value));
                    break;
                default:
                    entity[attr.Key] = attr.Value;
                    break;
            }
        }
    }
}
