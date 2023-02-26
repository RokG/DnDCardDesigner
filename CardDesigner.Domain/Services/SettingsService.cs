using System;
using System.Configuration;

namespace CardDesigner.Domain.Services
{
    public class SettingsStore
    {
        public double PrintCardScale { get => ReadDoubleSetting("PrintCardScale"); set => AddUpdateAppSettings("PrintCardScale", CropDouble(value,1).ToString()); }
        public double PrintPageOffsetX { get => ReadDoubleSetting("PrintPageOffsetX"); set => AddUpdateAppSettings("PrintPageOffsetX", CropDouble(value, 1).ToString()); }

        private double CropDouble(double value, int decimals)
        {
            double factor = Math.Pow(10, decimals);
            return Math.Floor(value * factor) / factor;
        }


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

        private static double ReadDoubleSetting(string key)
        {
            try
            {
                System.Collections.Specialized.NameValueCollection appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key];
                return Convert.ToDouble(result);
            }
            catch (ConfigurationErrorsException)
            {
                return -1.0;
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
