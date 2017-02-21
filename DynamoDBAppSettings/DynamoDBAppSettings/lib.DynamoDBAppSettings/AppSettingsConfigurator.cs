using System;
using lib.DynamoDBAppSettings.Interfaces;

namespace lib.DynamoDBAppSettings
{
    internal class AppSettingsConfigurator : ISettingsConfigurator
    {
        private readonly IDynamoDbAppSettingsRetriever _appSettingsRetriever;
        private readonly IAppSettingsSetter _appSettingsSetter;

        public AppSettingsConfigurator(IAppSettingsSetter appSettingsSetter, IDynamoDbAppSettingsRetriever appSettingsRetriever)
        {
            _appSettingsSetter = appSettingsSetter;
            _appSettingsRetriever = appSettingsRetriever;
        }

        public void ConfigureSettings(string domain)
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                throw new ArgumentException("The application name is null or empty");
            }

            var settings = _appSettingsRetriever.Retrieve(domain);
            _appSettingsSetter.SetConfiguration(settings);
        }
    }
}
