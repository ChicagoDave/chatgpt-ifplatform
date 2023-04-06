using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldModel
{
    public class GraphProperty
    {
        public string Key { get; set; }
        public object Value { get; set; }

        public GraphProperty(string key, object value)
        {
            Key = key;
            Value = value;
        }

        public static GraphProperty Create(string key, object value)
        {
            return new GraphProperty(key, value);
        }
    }
}
