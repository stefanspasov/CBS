namespace CBS.DAL.Repositories.Decorators
{
    using System;
    using System.Threading.Tasks;
    using CBS.DAL.Models;
    using CBS.DAL.Repositories;
    using CBS.DAL.Repositories.Decorators.Interfaces;
    using CBS.Logic.Loggers;
    using Newtonsoft.Json;

    public class ReservationRepositoryLoggingDecorator : IReservationRepository
    {
        private readonly ILogger logger;
        private readonly IExternalReservationRepository innerRepository;

        public ReservationRepositoryLoggingDecorator(ILogger logger, IExternalReservationRepository innerRepository)
        {
            this.logger = logger;
            this.innerRepository = innerRepository;
        }

        public async Task<int> MakeReservationAsync(Reservation reservation)
        {
            try
            {
                var serialized = JsonConvert.SerializeObject(reservation);
                this.logger.Log($"{nameof(this.MakeReservationAsync)} called with input: {serialized}");
                return await this.innerRepository.MakeReservationAsync(reservation);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(this.MakeReservationAsync)} threw exception. Throwing it furhter.", ex);
                throw;
            }
        }

        public async Task<Reservation> UpdateAndGetReservationAsync(int reservationId, DateTime date, int kilometers)
        {
            try
            {
                this.logger.Log(
                    $"{nameof(this.UpdateAndGetReservationAsync)} called with reservationId: {reservationId}, date: {date}, kilometers: {kilometers}");
                return await this.innerRepository.UpdateAndGetReservationAsync(reservationId, date, kilometers);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(this.UpdateAndGetReservationAsync)} threw exception. Throwing it furhter.", ex);
                throw;
            }
        }
    }
}