using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk.Metadata;

namespace CrmLibrary.Test
{
     [TestClass]
    public class RecordHelperTest
    {
         [TestMethod]
         public void TestGetEntityAttributeMetadata()
         {
             var helper = new RecordHelper();
             AttributeMetadata[] metadatas = helper.RetrieveEntityMetadata("jkh_kasokishudb");
         }
    }
}
