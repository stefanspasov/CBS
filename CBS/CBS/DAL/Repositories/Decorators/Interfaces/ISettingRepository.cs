namespace CBS.DAL.Repositories.Decorators.Interfaces
{
    public interface ISettingRepository
    {
        T GetSetting<T>(string settingName);
    }
}
