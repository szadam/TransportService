using TransportService.Models;

namespace TransportService.Consumer
{
	public class ReserveTransportQuery
	{
		public long DepartueTransportID { get; set; }
		public long ReturnTransportID { get; set; }
		public int Places { get; set; }
		public Guid ReservationId { get; set; }
		public ReserveTripOfferParameters ReserveTripOfferParameters { get; set; }
	}
}
