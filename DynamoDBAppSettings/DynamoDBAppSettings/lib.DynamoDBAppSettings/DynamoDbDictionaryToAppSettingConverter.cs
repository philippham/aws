using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;
using lib.DynamoDBAppSettings.Interfaces;

namespace lib.DynamoDBAppSettings
{
    internal class DynamoDbDictionaryToAppSettingConverter : IDynamoDbDictionaryToAppSettingConverter
    {
        public const string DomainColumnName = "Domain";
        public const string SettingNameColumnName = "AppSettingName";
        public const string ValueColumnName = "Value";
        public const string GlobalDomainValue = "Global";

        private readonly IAppSettingDomainConverter _appSettingDomainConverter;

        public DynamoDbDictionaryToAppSettingConverter(IAppSettingDomainConverter appSettingDomainConverter)
        {
            _appSettingDomainConverter = appSettingDomainConverter;
        }

        public AppSettingConfigItem Convert(Dictionary<string, AttributeValue> input)
        {
            if (input == null)
            {
                throw new ArgumentException("The input object is null");
            }

            if (!input.ContainsKey(DomainColumnName))
            {
                throw new ArgumentException("The input object does not contain domain");
            }

            if (!input.ContainsKey(SettingNameColumnName))
            {
                throw new ArgumentException("The input object does not contain setting name");
            }

            if (!input.ContainsKey(ValueColumnName))
            {
                throw new ArgumentException("The input object does not contain setting value");
            }

            return new AppSettingConfigItem(_appSettingDomainConverter.Convert(input[DomainColumnName].S), input[SettingNameColumnName].S, input[ValueColumnName].S);
        }
    }
}
