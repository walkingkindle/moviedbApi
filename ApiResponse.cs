using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MovieDbApi
{ 
    public class ApiResponse
    {
        [JsonProperty("status")]
        public required string Status { get; set; }

        [JsonProperty("data")]
        public List<ShowData>? Data { get; set; }
    }
}
