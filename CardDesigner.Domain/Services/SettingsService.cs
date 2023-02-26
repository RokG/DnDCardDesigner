using System;
using System.Configuration;

namespace CardDesigner.Domain.Services
{
    public class SettingsStore
    {
        public int PrintCardScale { get => ReadIntSetting("PrintCardScale"); set => AddUpdateAppSettings("PrintCardScale", value.ToString()); }
        public int PrintPageOffsetX { get => ReadIntSetting("PrintPageOffsetX"); set => AddUpdateAppSettings("PrintPageOffsetX", value.ToString()); }

        private static int ReadIntSetting(string key)
        {
            try
            {
                System.Collections.Specialized.NameValueCollection appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key];
                return Convert.ToInt32(result);
            }
            catch (ConfigurationErrorsException)
            {
                return -1;
            }
        }

        private static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationCollection settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
            }
        }
    }
}
