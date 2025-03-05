using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Test
{
    public class TestConfig
    {
        [JsonIgnore]
        [XmlAttribute("ip")]
        public string IP { get; set; } = "127.0.0.1";

        [JsonProperty("name")]
        [XmlAttribute("name")]
        public string Name { get; set; } = "xiaoming";

        [JsonProperty("number")]
        [XmlAttribute("number")]
        public int Number { get; set; } = 3306;

        [JsonProperty("address")]
        [XmlIgnore]
        public string Address { get; set; } = "www.baidu.com";
    }
}
