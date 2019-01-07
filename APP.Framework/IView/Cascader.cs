using System.Collections.Generic;
using Newtonsoft.Json;

namespace APP.Framework.IView
{
    public class Cascader
    {
        public string Value { get; set; }
        public string Label { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Cascader> Children { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Loading { get; set; }
    }
}
