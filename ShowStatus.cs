using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDbApi
{
    public class ShowStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RecordType { get; set; }
        public bool KeepUpdated { get; set; }
    }
}
