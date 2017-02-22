namespace CBS.DAL.Models
{
    using System;
    using Logic.Models;

    public class Reservation
    {
        public Reservation()
        {
            // Needed for code first db initializer
        }

        public Reservation(string customerNumber, VehicleType vehicleType, DateTime bookingDate, int bookingKilometers)
        {
            this.CustomerNumber = customerNumber;
            this.VehicleType = vehicleType;
            this.BookingDate = bookingDate;
            this.BookingKilometers = bookingKilometers;
        }

        public int ReservationID { get; set; }

        public string CustomerNumber { get; set; }

        public VehicleType VehicleType { get; set; }

        public DateTime BookingDate  { get; set; }

        public int BookingKilometers { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int? ReturnKilometers { get; set; }
    }
}
