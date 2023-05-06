using System;
using System.Collections.Generic;

namespace TransportService.Models
{
    public partial class Place
    {
        public Place()
        {
            TransportDestinationPlaces = new HashSet<Transport>();
            TransportSourcePlaces = new HashSet<Transport>();
        }

        public long Id { get; set; }
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;

        public virtual ICollection<Transport> TransportDestinationPlaces { get; set; }
        public virtual ICollection<Transport> TransportSourcePlaces { get; set; }
    }
}
