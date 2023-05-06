namespace TransportService.Models
{
	public class TransportOffer
	{
		public long TransportIDFrom { get; set; }
		public long TransportIDTo { get; set; }
		public string TransportName { get; set; }
		public string DestinationCountry { get; set; }
		public string DestinationCity { get; set; }
		public string DepartureCountry { get; set; }
		public string DepartureCity { get; set; }
		public int Persons { get; set; }
		public int Price { get; set; }
	}
}
