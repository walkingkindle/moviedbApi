using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDbApi
{
    public class Login
    {
        public required string ApiKey { get; set; }
        public string? pin { get; set; }

    }

}
