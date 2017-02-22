namespace CBS.Logic.Models
{
    public interface IReservable
    {
        decimal CalculatePrice(decimal numberOfDays, int kilometers);
    }
}
