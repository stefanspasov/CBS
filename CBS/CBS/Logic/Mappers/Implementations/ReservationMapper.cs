namespace CBS.Logic.Mappers.Implementations
{
    using DAL.Models;
    using Models.DTOs;

    public class ReservationMapper : IReservationMapper
    {
        public Reservation Map(MakeReservationDto reservationDto)
        {
            if (reservationDto == null)
            {
                return null;
            }

            return new Reservation(
                reservationDto.CustomerNumber,
                reservationDto.VehicleType,
                reservationDto.BookingDate,
                reservationDto.BookingKilometers);
        }
    }
}