using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MovieDbApi
{
    public class ShowStatus
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("recordType")]
        public string RecordType { get; set; }

        [JsonProperty("keepUpdated")]
        public bool KeepUpdated { get; set; }
    }
}
