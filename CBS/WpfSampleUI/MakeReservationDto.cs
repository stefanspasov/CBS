namespace WpfSampleUI
{
    using System;

    public class MakeReservationDto
    {
        public string CustomerNumber { get; set; }

        public int VehicleType { get; set; }

        public DateTime BookingDate { get; set; }

        public int BookingKilometers { get; set; }
    }
}