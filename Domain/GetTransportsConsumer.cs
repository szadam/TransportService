using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using CommonComponents;
//using CommonComponents.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransportService.Models;
using TripService.Repository;
//using Transport = CommonComponents.Models.Transport;

namespace TransportService.Consumer
{
    public class GetTransportConsumer: IConsumer<GetTransportQuery>
    {
        private readonly ILogger<GetTransportQuery> _logger;
        private ITransportRepository _repository;

        public GetTransportConsumer(ILogger<GetTransportQuery> logger, ITransportRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<GetTransportQuery> context)
        {
            var command = context.Message;
            var city = command.Destination;

            List<Transport> from_matches = _repository.GetGeneralTransport(command.Departue,command.Destination,
                new DateOnly(command.DepartureDate.Year,command.DepartureDate.Month,command.DepartureDate.Day),command.Places,0);
            List<Transport> to_matches = _repository.GetGeneralTransport(command.Destination,command.Departue,
                new DateOnly(command.ReturnDate.Year,command.ReturnDate.Month,command.ReturnDate.Day),command.Places,1);
            List<Transport> final_list = _repository.MatchTransports(from_matches, to_matches);
            
            await context.RespondAsync<GetTransportResponse>( new GetTransportResponse(){
                Transports = final_list
            });
        }
    }
}