using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace CrmLibrary
{
    public class FieldHelper
    {
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
