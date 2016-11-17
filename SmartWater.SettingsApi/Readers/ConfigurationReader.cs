using Microsoft.Azure;
using SmartWater.Domain.Core.Contracts.Readers;
using System.Collections.Generic;

namespace SmartWater.SettingsApi.Readers
{
    public class ConfigurationReader : IConfigurationReader
    {
        private Dictionary<string, string> _settings;


        public ConfigurationReader()
        {
            _settings = new Dictionary<string, string>();

            AddSetting("DocumentDb.AuthorizationKey");
            AddSetting("DocumentDb.DatabaseName");
            AddSetting("DocumentDb.DatabaseUri");
        }


        private void AddSetting(string settingName)
        {
            _settings[settingName] = CloudConfigurationManager.GetSetting(settingName);
        }


        public string this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                    return string.Empty;

                if (!_settings.ContainsKey(key))
                    return string.Empty;

                return _settings[key];
            }

            set
            {
                if (_settings.ContainsKey(key)){
                    _settings[key] = value;
                }
                _settings.Add(key, value);
            }
        }
    }
}