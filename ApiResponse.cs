using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDbApi
{
    public class ApiResponse
    {
        public string Status { get; set; }
        public ShowData Data { get; set; }
    }
}
