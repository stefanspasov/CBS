namespace CBS.DAL.Repositories.Decorators
{
    using System;
    using System.Globalization;
    using System.Runtime.Caching;

    using CBS.DAL.Repositories;
    using CBS.DAL.Repositories.Decorators.Interfaces;

    public class SettingRepositoryCacheDecorator : ISettingRepository
    {
        private const int CacheMinutes = 15;
        private readonly IExternalSettingRepository innerRepository;

        public SettingRepositoryCacheDecorator(IExternalSettingRepository innerRepository)
        {
            this.innerRepository = innerRepository;
        }

        public T GetSetting<T>(string settingName)
        {
            // TODO Extension point: External cache could be used instead
            if (MemoryCache.Default.Contains(settingName))
            {
                return (T)Convert.ChangeType(MemoryCache.Default[settingName], typeof(T), CultureInfo.CurrentCulture);
            }

            var value = this.innerRepository.GetSetting<T>(settingName);

            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMinutes(CacheMinutes))
            };
            MemoryCache.Default.Set(settingName, value, policy);

            return value;
        }
    }
}