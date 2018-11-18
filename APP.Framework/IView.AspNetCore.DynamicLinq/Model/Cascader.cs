using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace IView.AspNetCore.DynamicLinq
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
