using System;
//using CommonComponents.Models;
using TransportService.Models;

namespace TransportService.Services
{
    public class PriceCalculator
    {
        public static int CalculateTransportOfferPrice(TransportOffer transportOffer)
        {
            int transpotrTypeFactor;
            if (transportOffer.TransportName == "Own")
                return 0;
            else if (transportOffer.TransportName == "Bus")
                transpotrTypeFactor = 1;
            else
                transpotrTypeFactor = 3;
            return transportOffer.Persons * transpotrTypeFactor * (Math.Abs(transportOffer.DestinationCity.GetHashCode())% 200);
        }
	}
}