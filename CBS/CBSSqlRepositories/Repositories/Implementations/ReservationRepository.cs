namespace CBSSqlRepositories.Repositories.Implementations
{
    using System;
    using System.Threading.Tasks;
    using CBS.DAL.Models;
    using CBS.DAL.Repositories;

    public class ReservationRepository : IExternalReservationRepository
    {
        public async Task<int> MakeReservationAsync(Reservation reservation)
        {
            using (var ctx = new CbsContext())
            {
                ctx.Reservations.Add(reservation);
                await ctx.SaveChangesAsync();
                return reservation.ReservationID;
            }
        }

        public async Task<Reservation> UpdateAndGetReservationAsync(int reservationId, DateTime date, int kilometers)
        {
            using (var ctx = new CbsContext())
            {
                var reservation = await ctx.Reservations.FindAsync(reservationId);
                if (reservation == null)
                {
                    throw new ArgumentException($"Reservation with id: {reservationId} not found in the db.");
                }
                if (reservation.ReturnDate != null)
                {
                    throw new ArgumentException($"Reservation with id: {reservationId} has already been finalized.");
                }

                reservation.ReturnDate = date;
                reservation.ReturnKilometers = kilometers;
                await ctx.SaveChangesAsync();
                return reservation;
            }
        }
    }
}