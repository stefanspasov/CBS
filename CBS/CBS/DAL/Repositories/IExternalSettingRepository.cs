namespace CBS.DAL.Repositories
{
    public interface IExternalSettingRepository
    {
        T GetSetting<T>(string settingName);
    }
}