namespace lib.DynamoDBAppSettings
{
    internal class AppSettingConfigItem
    {
        public DomainName DomainName { get; private set; }

        public string Key { get; private set; }

        public string Value { get; private set; }

        public AppSettingConfigItem(DomainName domainName, string key, string value)
        {
            DomainName = domainName;
            Key = key;
            Value = value;
        }
    }
}
