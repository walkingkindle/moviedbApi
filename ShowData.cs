using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDbApi
{
    public class ShowData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }

        public string Image { get; set; }

        public string FirstAired { get; set; }

        public string LastAired { get; set; }

        public int? Score { get; set; }

        public ShowStatus Status { get; set; }

        public string OriginalCountry { get; set; }

        public string OriginalLanguage { get; set; }
    }
}
