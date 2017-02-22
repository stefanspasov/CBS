using System.Collections.Generic;
using System.Windows;

namespace WpfSampleUI
{
    using System;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CBSClient cbsClient; 

        public MainWindow()
        {
            this.InitializeComponent();
            this.cbsClient = new CBSClient();
        }

        private void btnMakeReservation_Click(object sender, RoutedEventArgs e)
        {
            var newReservation = new MakeReservationDto
                                     {
                                         BookingDate = DateTime.Now,
                                         BookingKilometers = int.Parse(this.tbInitialKilometers.Text),
                                         CustomerNumber = this.tbCustomerId.Text,
                                         VehicleType = int.Parse(this.cbVehicleType.SelectedValue.ToString())
                                     };
            this.lblReservationCreatedNotification.Content = $"Reservation with id: {this.cbsClient.MakeReservation(newReservation)} created!"; 
        }

        private void btnUpdateReservation_Click(object sender, RoutedEventArgs e)
        {
            var reservation = new UpdateReservationDto
                                  {
                                      ReservationId = int.Parse(this.tbReservationId.Text),
                                      ReturnDate = DateTime.Now,
                                      ReturnKilometers = int.Parse(this.tbReturnKilometers.Text)
                                  };
            var response = this.cbsClient.UpdateReservation(reservation);
            this.lblTotalPrice.Content = "Total price: " + response.TotalPrice;
            this.lblKilometersTravelled.Content = "Passed kilometers: " + response.KilometersTravelled;
            this.lblNumberOfDays.Content = "Number of days: " +  response.NumberOfDays;
        }
    }

    public static class Vehicles
    {
        public static Dictionary<int, string> GetVehicles()
        {
            var vehicles = new Dictionary<int, string>
                               {
                                   { 1, "Small Car" },
                                   { 2, "Van" },
                                   { 3, "Minibus" },
                               };
            return vehicles;
        }
    }
}
