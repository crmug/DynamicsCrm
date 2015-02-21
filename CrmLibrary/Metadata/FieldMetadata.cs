using System.Collections.Generic;
using Microsoft.Xrm.Sdk.Metadata;

namespace CrmLibrary.Metadata
{
    /// <summary>
    /// 字段元数据
    /// </summary>
    public class FieldMetadata
    {
        #region 基础属性（必须）

        public string EntityName { get; set; }
        public string SchemName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public AttributeRequiredLevel RequiredLevel { get; set; }
        public FieldType FieldType { get; set; }

        #endregion

        #region 扩展属性（可选）

        /* SingleLineOfText, MultipleLinesOfText */
        public int MaxLength { get; set; }

        /* WholeNumber,FloatingPointNumber */
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }

        /* Decimal */
        public int? Precision { get; set; }

        /* TwoOptions, OptionSet */
        public IDictionary<string, int> Options { get; set; }

        /* OptionSet */
        public bool IsGlobal { get; set; }

        /* Lookup */
        public string ReferencedEntityName { get; set; }
        public string ReferencedAttributeName { get; set; }
        public string RelationshipSchemName { get; set; }

        #endregion
    }

    /// <summary>
    /// 基础属性
    /// </summary>
    public class BasicFieldMetadata
    {
        #region 基础属性（必须）

        public string EntityName { get; set; }
        public string SchemName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public AttributeRequiredLevel RequiredLevel { get; set; }
        public FieldType FieldType { get; set; }

        #endregion
    }

    /// <summary>
    /// 扩展属性
    /// </summary>
    public class ExtendedFieldMetadata
    {
        #region 扩展属性（可选）

        /* SingleLineOfText, MultipleLinesOfText */
        public int MaxLength { get; set; }

        /* WholeNumber,FloatingPointNumber */
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }

        /* Decimal */
        public int? Precision { get; set; }

        /* TwoOptions, OptionSet */
        public IDictionary<string, int> Options { get; set; }

        /* OptionSet */
        public bool IsGlobal { get; set; }

        /* Lookup */
        public string ReferencedEntityName { get; set; }
        public string ReferencedAttributeName { get; set; }
        public string RelationshipSchemName { get; set; }

        #endregion
    }

    /// <summary>
    /// 字段类型
    /// </summary>
    public enum FieldType
    {
        SingleLineOfText,
        MultipleLinesOfText,
        WholeNumber,
        FloatingPointNumber,
        Decimal,
        DateTime,
        TwoOptions,
        OptionSet,
        Lookup
    }
}
