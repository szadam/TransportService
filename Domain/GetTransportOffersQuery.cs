namespace TransportService.Consumer
{
	public class GetTransportOffersQuery
	{
		public string DepartueCity { get; set; }
		public string DepartueCountry { get; set; }
		public string DestinationCity { get; set; }
		public string DestinationCountry { get; set; }
		public int Places { get; set; }
		public DateTime DepartureDate { get; set; }
		public DateTime ReturnDate { get; set; }
	}
}
