namespace CBS.Logic.Models.DTOs
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MakeReservationDto
    {
        [Required]
        public string CustomerNumber { get; set; }

        [Required]
        public VehicleType VehicleType { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public int BookingKilometers { get; set; }
    }
}