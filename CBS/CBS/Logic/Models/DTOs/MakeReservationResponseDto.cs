namespace CBS.Logic.Models.DTOs
{
    public class MakeReservationResponseDto
    {
        public MakeReservationResponseDto(int reservationId)
        {
            this.ReservationId = reservationId;
        }

        public int ReservationId { get; set; } 
    }
}