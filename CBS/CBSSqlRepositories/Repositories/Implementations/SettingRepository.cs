namespace CBSSqlRepositories.Repositories.Implementations
{
    using System;
    using System.Globalization;
    using CBS.DAL.Repositories;

    public class SettingRepository : IExternalSettingRepository
    {
        public T GetSetting<T>(string settingName)
        {
            using (var ctx = new CbsContext())
            {
                var setting = ctx.Settings.Find(settingName);
                if (setting == null)
                {
                    throw new ArgumentException($"Setting with name: {settingName} not found in the db.");
                }

                return (T)Convert.ChangeType(setting.Value, typeof(T), CultureInfo.CurrentCulture);
            }
        }
    }
}