using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using lib.DynamoDBAppSettings.Interfaces;

namespace lib.DynamoDBAppSettings
{
    internal class AppSettingsSetter : IAppSettingsSetter
    {
        public void SetConfiguration(List<AppSettingConfigItem> items)
        {
            if (items == null)
            {
                throw new ArgumentException("Setting items cannot be null");
            }

            SetAppSettings(items.Where(x => x.DomainName == DomainName.ApplicationSpecific));

            SetAppSettings(items.Where(x => x.DomainName == DomainName.Global));
        }

        private void SetAppSettings(IEnumerable<AppSettingConfigItem> items)
        {
            foreach (var appSetting in items.Where(appSetting => ConfigurationManager.AppSettings[appSetting.Key] == null))
            {
                ConfigurationManager.AppSettings.Set(appSetting.Key, appSetting.Value);
            }
        }
    }
}
