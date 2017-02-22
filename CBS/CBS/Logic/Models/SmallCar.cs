namespace CBS.Logic.Models
{
    using System;
    using CBS.DAL.Repositories.Decorators.Interfaces;

    public class SmallCar : Vehicle, IReservable 
    {
        public SmallCar(ISettingRepository settingRepository)
            : base(settingRepository)
        {
        }

        public decimal CalculatePrice(decimal numberOfDays, int kilometers)
        {
            if (numberOfDays <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(numberOfDays),
                    numberOfDays,
                    "Return date should be later than booking date.");
            }

            return this.PricePerDay * numberOfDays;
        }
    }
}
