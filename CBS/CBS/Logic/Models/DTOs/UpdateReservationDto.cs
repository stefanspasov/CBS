namespace CBS.Logic.Models.DTOs
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UpdateReservationDto
    {
        [Required]
        public int ReservationId { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [Required]
        public int ReturnKilometers { get; set; }
    }
}
