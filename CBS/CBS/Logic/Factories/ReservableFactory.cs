namespace CBS.Logic.Factories
{
    using System;
    using CBS.DAL.Repositories.Decorators.Interfaces;
    using CBS.Logic.Models;

    public class ReservableFactory : IReservableFactory
    {
        private readonly ISettingRepository settingRepository;

        public ReservableFactory(ISettingRepository settingRepository)
        {
            if (settingRepository == null)
            {
                throw new ArgumentNullException(nameof(settingRepository));
            }
            this.settingRepository = settingRepository;
        }

        public IReservable Create(VehicleType carType)
        {
            switch (carType)
            {
                case VehicleType.Small:
                    return new SmallCar(this.settingRepository);
                case VehicleType.Van:
                    return new Van(this.settingRepository);
                case VehicleType.MiniBuss:
                    return new MiniBuss(this.settingRepository);
                default:
                    throw new ArgumentOutOfRangeException(nameof(carType), carType, "This car type does not exist!");
            }
        }
    }
}