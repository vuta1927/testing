using System.Collections.Generic;
using Demo.Dependency;

namespace Demo.Settings
{
    public interface ISettingsProvider : ITransientDependency
    {
        bool TryGetSetting<T>(SettingEntryKey key, out T value);
        IEnumerable<SettingEntryInfo> GetAll();
        bool SettingExists(SettingEntryKey key);
        string ProviderName { get; }
    }

    public interface IWritableSettingsProvider : ISettingsProvider
    {
        bool TrySetSetting<T>(SettingEntryKey key, T value);
        void DefineSetting(SettingEntryKey key, object initialValue);
//        void DeleteSettings(string moduleName);
    }
}