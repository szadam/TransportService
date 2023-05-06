using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using CommonComponents;
//using CommonComponents.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransportService.Models;
using TransportService.Services;
using TripService.Repository;
//using Transport = TransportService.Models.Transport;

namespace TransportService.Consumer
{
    public class GetTransportOfferConsumer: IConsumer<GetTransportOffersQuery>
    {
        private readonly ILogger<GetTransportOffersQuery> _logger;
        private ITransportRepository _repository;

        public GetTransportOfferConsumer(ILogger<GetTransportOffersQuery> logger, ITransportRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<GetTransportOffersQuery> context)
        {
            var command = context.Message;

			List<Transport> from_matches = _repository.GetSpecificTransport(command.DepartueCity, command.DepartueCountry, command.DestinationCity, command.DestinationCountry,
                new DateOnly(command.DepartureDate.Year,command.DepartureDate.Month,command.DepartureDate.Day),command.Places);
            List<Transport> to_matches = _repository.GetSpecificTransport(command.DestinationCity,command.DestinationCountry,command.DepartueCity,command.DepartueCountry,
                new DateOnly(command.ReturnDate.Year,command.ReturnDate.Month,command.ReturnDate.Day),command.Places);
            List<TransportOffer> final_list = _repository.MatchSpecyficTransports(from_matches, to_matches,command.Places);
            
            final_list.Insert(0,new TransportOffer()
            {
                DepartureCity = command.DepartueCity,
                DepartureCountry = command.DepartueCountry,
                DestinationCity = command.DestinationCity,
                DestinationCountry = command.DestinationCountry,
                Persons = command.Places,
                TransportIDFrom = -1,
                TransportIDTo = -1,
                TransportName = "Own",
                
            });
            final_list.ForEach( transport => transport.Price = PriceCalculator.CalculateTransportOfferPrice(transport));
            await context.RespondAsync<GetTransportOffersResponse>( new GetTransportOffersResponse(){
                TransportOffer = final_list
            });
        }
    }
}