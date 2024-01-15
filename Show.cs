using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDbApi
{
    public class Show
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ImageUrl { get; set; }
        public required DateTime ReleaseDate { get; set; }
        public DateTime? FinalEpisodeAired { get; set; }
        public int? Score { get; set; }
        public required string Status { get; set; }
        public required string OriginalCountry { get; set; }
        public required string OriginalLanguage { get; set; }
    }
}
