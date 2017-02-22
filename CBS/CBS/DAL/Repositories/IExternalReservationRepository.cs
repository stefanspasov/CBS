namespace CBS.DAL.Repositories
{
    using System;
    using System.Threading.Tasks;

    using CBS.DAL.Models;

    public interface IExternalReservationRepository
    {
        Task<int> MakeReservationAsync(Reservation reservation);

        Task<Reservation> UpdateAndGetReservationAsync(int reservationId, DateTime date, int kilometers);
    }
}