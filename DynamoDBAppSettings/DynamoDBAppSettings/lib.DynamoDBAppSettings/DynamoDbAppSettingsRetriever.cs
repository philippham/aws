using System;
using System.Collections.Generic;
using lib.DynamoDBAppSettings.Interfaces;

namespace lib.DynamoDBAppSettings
{
    internal class DynamoDbAppSettingsRetriever : IDynamoDbAppSettingsRetriever
    {
        private readonly IDynamoDbAppSettingsRetrieverAdapter _adapter;
        private readonly IDynamoDbDictionaryToAppSettingConverter _converter;

        public DynamoDbAppSettingsRetriever(IDynamoDbAppSettingsRetrieverAdapter adapter, IDynamoDbDictionaryToAppSettingConverter converter)
        {
            _adapter = adapter;
            _converter = converter;
        }

        public List<AppSettingConfigItem> Retrieve(string domain)
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                throw new ArgumentException("Domain is null or empty.");
            }

            var resultAppSettings = new List<AppSettingConfigItem>();

            var appSettings = _adapter.Retrieve(domain);

            if (appSettings == null)
                throw new InvalidOperationException($"There are no app settings for the application {domain}");

            appSettings.ForEach(x => resultAppSettings.Add(_converter.Convert(x)));

            return resultAppSettings;
        }
    }
}
