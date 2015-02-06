using System.Collections.Generic;
using System.Linq;

namespace CommonUtility
{

    public class Customer
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }

    public static class ObjectHelper
    {
        public static IDictionary<string, object> ToDictionary(this object o)
        {
            return o.GetType().GetProperties().ToDictionary(info => info.Name,info =>(info.GetValue(o, null)));
        }

        public static IDictionary<string, object> ToDictionary2(this object o)
        {
            return o.GetType()
                .GetProperties()
                .Select(pi => new { Name = pi.Name, Value = pi.GetValue(o, null) })
                .ToDictionary(ks => ks.Name, vs => vs.Value);
        }
    }
}
