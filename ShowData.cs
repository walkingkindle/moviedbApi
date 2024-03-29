﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MovieDbApi
{
    public class ShowData
    {

        [JsonProperty("name")]
        public required string  Name { get; set; }

        [JsonProperty("overview")]
        public required string Overview { get; set; }

        [JsonProperty("image")]
        public string? Image { get; set; }

        [JsonProperty("firstAired")]
        public  string? FirstAired { get; set; }

        [JsonProperty("lastAired")]
        public string? LastAired { get; set; }

        [JsonProperty("score")]
        public int? Score { get; set; }

        [JsonProperty("status")]
        public required ShowStatus Status { get; set; }

        [JsonProperty("originalCountry")]
        public required string OriginalCountry { get; set; }

        [JsonProperty("originalLanguage")]
        public required string OriginalLanguage { get; set; }
    }
}
