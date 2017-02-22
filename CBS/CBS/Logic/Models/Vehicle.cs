namespace CBS.Logic.Models
{
    using CBS.DAL.Repositories.Decorators.Interfaces;

    public abstract class Vehicle
    {
        private readonly ISettingRepository settingRepository;

        protected Vehicle(ISettingRepository settingRepository)
        {
            this.settingRepository = settingRepository;
        }

        protected decimal PricePerDay => this.settingRepository.GetSetting<decimal>("PricePerDay");

        protected decimal PricePerKilometer => this.settingRepository.GetSetting<decimal>("PricePerKilometer");
    }
}