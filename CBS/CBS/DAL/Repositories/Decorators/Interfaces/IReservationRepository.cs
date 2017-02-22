namespace CBS.DAL.Repositories.Decorators.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using CBS.DAL.Models;

    public interface IReservationRepository
    {
        Task<int> MakeReservationAsync(Reservation reservation);

        Task<Reservation> UpdateAndGetReservationAsync(int reservationId, DateTime date, int kilometers);
    }
}
