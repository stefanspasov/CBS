namespace CBS.Logic.Factories
{
    using CBS.Logic.Models;

    public interface IReservableFactory
    {
        IReservable Create(VehicleType carType);
    }
}