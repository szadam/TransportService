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
    public class ReserveTransportQueryConsumer: IConsumer<ReserveTransportQuery>
    {
        private readonly ILogger<GetTransportOffersQuery> _logger;
        private ITransportRepository _repository;

        public ReserveTransportQueryConsumer(ILogger<GetTransportOffersQuery> logger, ITransportRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<ReserveTransportQuery> context)
        {
            var command = context.Message;

            var ret = _repository.ReserveTransport(command.DepartueTransportID,command.ReturnTransportID,
                command.Places,command.ReservationId,
                command.ReserveTripOfferParameters.StartDate,
                command.ReserveTripOfferParameters.EndDate);
            var succes = ret.Item3;

            if (succes)
            {
				_ = context.Publish(new ReserveTransportSuccessResponse()
				{
					ReservationId = command.ReservationId
				});
            }
            else
            {
				_ = context.Publish(new ReserveTransportFailureResponse()
				{
					ReservationId = command.ReservationId
				});
            }

            //await context.RespondAsync<ReserveTransportResponse>( new ReserveTransportResponse(){
             //   Created = ret.Item3,
              //  DepartueReservationid = ret.Item1,
               // ReturnReservationid = ret.Item2
        }
    }
}