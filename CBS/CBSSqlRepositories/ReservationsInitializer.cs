namespace CBSSqlRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using CBS.DAL.Models;
    using CBS.Logic.Models;

    public class ReservationsInitializer : DropCreateDatabaseIfModelChanges<CbsContext>
    {
        protected override void Seed(CbsContext context)
        {
            var reservations = new List<Reservation>
                                   {
                                       new Reservation("9006231234", VehicleType.Small, DateTime.Now, 0),
                                       new Reservation("9006231234", VehicleType.Van, DateTime.Now, 100),
                                       new Reservation("9006231234", VehicleType.MiniBuss, DateTime.Now, 200)
                                           {
                                               ReturnDate = DateTime.Now.AddDays(1),
                                               ReturnKilometers = 250
                                           },
                                       new Reservation("9006231234", VehicleType.Van, DateTime.Now, 300)
                                           {
                                               ReturnDate = DateTime.Now.AddDays(1), 
                                               ReturnKilometers = 400
                                           }
                                   };

            var settings = new List<Setting>
                               {
                                   new Setting { Name = "PricePerDay", Value = "200" },
                                   new Setting { Name = "PricePerKilometer", Value = "10,5" }
                               };

            context.Reservations.AddRange(reservations);
            context.Settings.AddRange(settings);
            context.SaveChanges();
        }
    }
}
