namespace CBS.Logic.Models
{
    using System;
    using CBS.DAL.Repositories.Decorators.Interfaces;

    public class MiniBuss : Vehicle, IReservable
    {
        private const decimal DateMultiplier = 1.7m;
        private const decimal KilometersMultiplier = 1.5m;

        public MiniBuss(ISettingRepository settingRepository)
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
                    "Return date should be after booking date.");
            }

            if (kilometers < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(kilometers), kilometers, "Did you drive backwards?");
            }

            return this.PricePerDay * numberOfDays * DateMultiplier
                   + (this.PricePerKilometer * kilometers * KilometersMultiplier);
        }
    }
}
