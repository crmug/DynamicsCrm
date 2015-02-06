using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommonUtility
{
    public class DataContractHelper
    {
        public void Serialize(string path,Type t, Object o)
        {
            var dcs = new DataContractSerializer(t);

            using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (XmlDictionaryWriter writer =
                    XmlDictionaryWriter.CreateTextWriter(stream, Encoding.UTF8))
                {
                    writer.WriteStartDocument();
                    dcs.WriteObject(writer, o);
                }
            }
        }

        public Object Deserialize(string path, Type t)
        {
            var fs = new FileStream(path, FileMode.Open);
            XmlDictionaryReader reader =
                XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            var dsc = new DataContractSerializer(t);

            // Deserialize the data and read it from the instance.
            return dsc.ReadObject(reader, true);
        }
    }
}
