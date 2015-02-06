using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Metadata;

namespace CrmLibrary.Metadata
{
    public class AttributeMetadata
    {
        public string SchemName { get; set; }
        public AttributeRequiredLevel RequiredLevel { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}
