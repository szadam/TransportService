namespace TransportService.Consumer
{
	public class GetTransportQuery
	{
		public string Departue { get; set; }
		public string Destination { get; set; }
		public int Places { get; set; }
		public DateTime DepartureDate { get; set; }
		public DateTime ReturnDate { get; set; }
	}
}
