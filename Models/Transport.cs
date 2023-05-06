using System;
using System.Collections.Generic;

namespace TransportService.Models
{
    public partial class Transport
    {
        public Transport()
        {
            Transportevents = new HashSet<Transportevent>();
        }

        public long Id { get; set; }
        public long DestinationPlacesId { get; set; }
        public long SourcePlacesId { get; set; }
        public string Transporttype { get; set; } = null!;
        public DateOnly Transportdate { get; set; }
        public int Places { get; set; }

        public virtual Place DestinationPlaces { get; set; } = null!;
        public virtual Place SourcePlaces { get; set; } = null!;
        public virtual ICollection<Transportevent> Transportevents { get; set; }


		public string Name { get; set; }
		public string DestinationCountry { get; set; }
		public string DestinationCity { get; set; }
		public string DepartueCity { get; set; }
		public string DepartueCountry { get; set; }
	}
}
