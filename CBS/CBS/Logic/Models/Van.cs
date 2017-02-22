using System;

namespace CBS.Logic.Models
{
    using CBS.DAL.Repositories.Decorators.Interfaces;

    public class Van : Vehicle, IReservable
    {
        private const decimal DateMultiplier = 1.2m;

        public Van(ISettingRepository settingRepository)
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

            return this.PricePerDay * numberOfDays * DateMultiplier + (this.PricePerKilometer * kilometers);
        }
    }
}
