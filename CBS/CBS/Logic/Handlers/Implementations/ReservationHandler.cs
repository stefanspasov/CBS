namespace CBS.Logic.Handlers.Implementations
{
    using System;
    using System.Threading.Tasks;
    using CBS.DAL.Repositories.Decorators.Interfaces;
    using Factories;
    using Models.DTOs;
    using Mappers;

    public class ReservationHandler : IReservationHandler
    {
        private readonly IReservationMapper reservationMapper;
        
        private readonly IReservationRepository reservationRepository;

        private readonly IReservableFactory reservableFactory;

        public ReservationHandler(
            IReservationMapper reservationMapper,
            IReservationRepository reservationRepository,
            IReservableFactory reservableFactory)
        {
            if (reservationMapper == null)
            {
                throw new ArgumentException(nameof(reservationMapper));
            }
            if (reservationRepository == null)
            {
                throw new ArgumentException(nameof(reservationRepository));
            }
            if (reservableFactory == null)
            {
                throw new ArgumentException(nameof(reservableFactory));
            }

            this.reservationMapper = reservationMapper;
            this.reservationRepository = reservationRepository;
            this.reservableFactory = reservableFactory;
        }

        public async Task<MakeReservationResponseDto> MakeReservation(MakeReservationDto reservationDto)
        {
            var reservation = this.reservationMapper.Map(reservationDto);
            var reservationId = await this.reservationRepository.MakeReservationAsync(reservation);
            return new MakeReservationResponseDto(reservationId);
        }

        public async Task<UpdateReservationResponseDto> UpdateReservationGetTotalPrice(UpdateReservationDto reservationDto)
        {
            var reservation = await this.reservationRepository.UpdateAndGetReservationAsync(
                reservationDto.ReservationId,
                reservationDto.ReturnDate,
                reservationDto.ReturnKilometers);

            var reservableVehicle = this.reservableFactory.Create(reservation.VehicleType);
            var numberOfDays = DayCalculator.CalculateDateDifferenceCountStartedDay(
                reservation.BookingDate,
                reservation.ReturnDate ?? default(DateTime));
            var kilometers = reservation.ReturnKilometers - reservation.BookingKilometers;
            var totalPrice = reservableVehicle.CalculatePrice(numberOfDays, kilometers ?? 0);
            return new UpdateReservationResponseDto(totalPrice, numberOfDays, kilometers ?? 0);
        } 
    }
}