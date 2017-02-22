using System;

namespace WpfSampleUI
{
    public class UpdateReservationDto
    {
        public int ReservationId { get; set; }

        public DateTime ReturnDate { get; set; }

        public int ReturnKilometers { get; set; }
    }
}
