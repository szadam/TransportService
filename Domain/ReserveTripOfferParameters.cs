namespace TransportService.Domain
{
    public class ReserveTripOfferParameters
    {
        public string HotelId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RoomTypeId { get; set; }
        public int TransportFromId { get; set; }
        public int TransportToId { get; set; }
        public string Username { get; set; }
        public int Persons { get; set; }
    }
}
