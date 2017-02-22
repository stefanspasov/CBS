namespace CBS.Logic.Models.DTOs
{
    public class UpdateReservationResponseDto
    {
        public UpdateReservationResponseDto(decimal totalPrice, int numberOfDays, int kilometersTravelled)
        {
            this.TotalPrice = totalPrice;
            this.NumberOfDays = numberOfDays;
            this.KilometersTravelled = kilometersTravelled;
        }

        public decimal TotalPrice { get; set; }

        public int NumberOfDays { get; set; }

        public int KilometersTravelled { get; set; }
    }
}
