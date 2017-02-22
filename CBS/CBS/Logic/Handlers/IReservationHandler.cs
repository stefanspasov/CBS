namespace CBS.Logic.Handlers
{
    using System.Threading.Tasks;
    using CBS.Logic.Models.DTOs;

    public interface IReservationHandler
    {
        Task<MakeReservationResponseDto> MakeReservation(MakeReservationDto reservationDto);

        Task<UpdateReservationResponseDto> UpdateReservationGetTotalPrice(UpdateReservationDto reservationDto);
    }
}
