namespace CBS.Logic.Mappers
{
    using CBS.DAL.Models;
    using CBS.Logic.Models.DTOs;

    public interface IReservationMapper
    {
        Reservation Map(MakeReservationDto reservationDto);
    }
}