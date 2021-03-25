using System.Collections.Generic;

namespace CV19.Models
{
    class CountryInfo : PlaceInfo
    {
        public IEnumerable<ProviceInfo> ProviceInfo { get; set; }
    }
}
